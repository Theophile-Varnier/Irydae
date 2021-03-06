﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Irydae.Helpers;
using Irydae.Model;
using Irydae.Services;
using Irydae.Views;

namespace Irydae.ViewModels
{
    public class MainWindowViewModel : AbstractPropertyChanged
    {
        private double currentZoom;

        public double CurrentZoom
        {
            get { return currentZoom; }
            set
            {
                currentZoom = value;
                OnPropertyChanged("CurrentZoom");
            }
        }

        private double currentPanX;

        public double CurrentPanX
        {
            get { return currentPanX; }
            set
            {
                currentPanX = value;
                OnPropertyChanged("CurrentPanX");
            }
        }

        private double currentPanY;

        public double CurrentPanY
        {
            get { return currentPanY; }
            set
            {
                currentPanY = value;
                OnPropertyChanged("CurrentPanY");
            }
        }

        private DisplayMode displayMode;

        public DisplayMode DisplayMode
        {
            get { return displayMode; }
            set
            {
                displayMode = value;
                OnPropertyChanged("DisplayMode");
            }
        }

        private readonly JournalService journalService;

        private const string CurrentProfilePropertyName = "CurrentProfile";

        private ProfilViewModel currentProfile;

        public PersonnageInfoViewModel PersonnageInfo { get; private set; }

        public ICommand CreateProfilCommand { get; private set; }

        public ICommand SaveDataCommand { get; private set; }

        public ICommand GenerateHtmlCommand { get; private set; }

        public ICommand DisplayResultCommand { get; private set; }

        public ICommand DisplayOptionDialogCommand { get; private set; }

        public ProfilViewModel CurrentProfile
        {
            get { return currentProfile; }
            set
            {
                currentProfile = value;
                OnPropertyChanged(CurrentProfilePropertyName);
            }
        }

        public OptionsViewModel OptionsViewModel { get; private set; }

        private void SelectProfil(ProfilViewModel profil)
        {
            if (profil != CurrentProfile && CheckModifications(true))
            {
                foreach (ProfilViewModel otherProfile in Profils)
                {
                    otherProfile.Selected = false;
                }
                if (profil != null)
                {
                    profil.Selected = true;
                    CurrentProfile = profil;
                }
            }
        }

        private void CreateProfil()
        {
            var newProfil = new ProfilViewModel();
            AddProfileDialog dialog = new AddProfileDialog
            {
                DataContext = newProfil,
                Owner = Application.Current.MainWindow
            };
            var resDialog = dialog.ShowDialog();
            if (resDialog.HasValue && resDialog.Value)
            {
                newProfil.Command = new RelayCommand(o => SelectProfil((ProfilViewModel)o));
                Profils.Add(newProfil);
                SelectProfil(newProfil);
            }
        }

        public ObservableCollection<ProfilViewModel> Profils { get; private set; }

        public MainWindowViewModel(JournalService service, OptionsService optionService, OptionsViewModel options)
        {
            Profils = new ObservableCollection<ProfilViewModel>();
            journalService = service;
            OptionsViewModel = options;
            PersonnageInfo = new PersonnageInfoViewModel(optionService);
            CreateProfilCommand = new RelayCommand(CreateProfil);
            SaveDataCommand = new RelayCommand(SaveDatas);
            GenerateHtmlCommand = new RelayCommand(GenerateAndPreview);
            DisplayResultCommand = new RelayCommand(GenerateAndOpen);
            DisplayOptionDialogCommand = new RelayCommand(OpenOptionDialog);
            PropertyChanged += OnPropertyChanged;
            CurrentZoom = 1;
            CurrentPanX = 0;
            CurrentPanY = 0;
        }

        private void OpenOptionDialog()
        {
            OptionDialog dialog = new OptionDialog
            {
                Owner = Application.Current.MainWindow,
                DataContext = OptionsViewModel
            };
            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                OptionsViewModel.Save(CurrentProfile.Header);
            }
            else
            {
                OptionsViewModel.CancelDialog(CurrentProfile.Header);
            }
        }

        public void Init()
        {
            var profiles = journalService.GetExistingProfils();
            foreach (var profil in profiles)
            {
                Profils.Add(new ProfilViewModel
                {
                    Header = profil,
                    Command = new RelayCommand(o => SelectProfil((ProfilViewModel)o))
                });
            }
            if (!Profils.Any())
            {
                CreateProfil();
            }
            SelectProfil(Profils.FirstOrDefault());
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case CurrentProfilePropertyName:
                    PersonnageInfo.Personnage.Periodes.Clear();
                    PersonnageInfo.Personnage.Relations.Clear();
                    if (CurrentProfile != null)
                    {
                        OptionsViewModel.Load(CurrentProfile.Header);
                        Personnage personnage = journalService.ParseDatas(CurrentProfile.Header);
                        if (personnage != null && personnage.Periodes != null)
                        {
                            foreach (var relation in personnage.Relations)
                            {
                                relation.Type = relation.Type == null ? null : OptionsViewModel.Options.TypesRelation.FirstOrDefault(tr => tr.Nom == relation.Type.Nom);
                                PersonnageInfo.Personnage.Relations.Add(relation);
                            }
                            bool update = false;
                            foreach (var periode in personnage.Periodes)
                            {
                                if (!update && !OptionsViewModel.Options.HideUpdate && !PersonnageInfoViewModel.VerifierPositionPeriode(periode))
                                {
                                    update = YesNoCancelDialogViewModel.ShowDialog("Des différences de position ont été trouvées entre ce profil et notre super base de données de ouf.\nVeux-tu mettre à jour tes données en conséquences ?", "Mise à jour, bonjour !", "Oui", "Non", null) == MessageBoxResult.Yes;
                                    if (!update)
                                    {
                                        OptionsViewModel.Options.HideUpdate = true;
                                        OptionsViewModel.Save(CurrentProfile.Header);
                                        YesNoCancelDialogViewModel.ShowDialog("C'était bien la peine d'ajouter la configuration des lieux.", "Ca fait plaisir.", "Ok TG.", null, null);
                                        YesNoCancelDialogViewModel.ShowDialog("Heureusement que je me casse pas le cul pour rien.", "Ca fait plaisir.", "Nan mais vraiment tais-toi.", null, null);
                                    }
                                }
                                PersonnageInfo.AddPeriode(periode, update);
                            }
                        }
                    }
                    break;
            }
        }

        public bool CheckModifications(bool soft)
        {
            if (ModificationStatusService.Instance.Dirty)
            {
                ModificationStatusService.Instance.Dirty = false;
                MessageBoxResult messageResult = YesNoCancelDialogViewModel.ShowDialog("Si tu quittes sans avoir sauvegardé ces épuisantes modifications (Ctrl + S), le monde risque de s'effondrer et les anomalies régneront sans partage sur Irydaë. Et aussi va falloir recommencer.\n\nTu veux que je sauvegarde pour toi (ça fera 5€) ?", "Attention malheureux !", "C'est fort aimable.", "5 balles ?! Crève.", "Tout bien réfléchi...");
                switch (messageResult)
                {
                    case MessageBoxResult.Yes:
                        SaveDatas();
                        return true;
                    case MessageBoxResult.No:
                        if (soft)
                        {
                            return true;
                        }
                        MessageBoxResult innerResult = YesNoCancelDialogViewModel.ShowDialog("Et pour 2€ ?", "Allez s'teup !", "Ok, ok, sauvegarde.", "Non mais vraiment.", "Attends, j'ai oublié un truc.");
                        switch (innerResult)
                        {
                            case MessageBoxResult.Yes:
                                SaveDatas();
                                return true;
                            case MessageBoxResult.No:
                                MessageBoxResult innerInnerResult = YesNoCancelDialogViewModel.ShowDialog("Allez, comme c'est toi je le fais gratuitement.", "Comme ça radine !", "Ah bah quand même.", "En fait je voulais vraiment pas sauvegarder.", "Ca m'a fait penser à un truc.");
                                switch (innerInnerResult)
                                {
                                    case MessageBoxResult.Yes:
                                        SaveDatas();
                                        return true;
                                    case MessageBoxResult.No:
                                        return true;
                                    default:
                                        ModificationStatusService.Instance.Dirty = true;
                                        return false;
                                }
                            default:
                                ModificationStatusService.Instance.Dirty = true;
                                return false;
                        }
                    default:
                        ModificationStatusService.Instance.Dirty = true;
                        return false;
                }
            }
            return true;
        }

        public string GenerateHtml()
        {
            SaveDatas();
            switch (DisplayMode)
            {
                case DisplayMode.Relations:
                    var relWritter = new TreeWriter();
                    return relWritter.GenerateHtml(PersonnageInfo.Personnage.Relations, OptionsViewModel.Options);
                case DisplayMode.Rps:
                    var htmlWriter = new HtmlWriterService();
                    return htmlWriter.GenerateHtml(PersonnageInfo.Personnage.Periodes, OptionsViewModel.Options);
                default:
                    return string.Empty;
            }
        }

        private void GenerateAndOpen()
        {
            var html = GenerateHtml();
            ResultDisplayDialog dialog = new ResultDisplayDialog
            {
                DataContext = html,
                Owner = Application.Current.MainWindow
            };
            dialog.ShowDialog();
        }

        private void GenerateAndPreview()
        {
            //new HtmlWriterService().ReadMapDatas();
            GenerateHtml();
            OpenBrowser();
        }

        private void OpenBrowser()
        {
            var uri = Path.Combine(JournalService.DataPath, "Web", "result.html");
            System.Diagnostics.Process.Start(uri);
        }

        public void SaveDatas()
        {
            journalService.UpdateDatas(CurrentProfile.Header, PersonnageInfo.Personnage);
            ModificationStatusService.Instance.Dirty = false;
        }
    }
}