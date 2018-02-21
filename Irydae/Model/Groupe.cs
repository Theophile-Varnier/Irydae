using System.ComponentModel;

namespace Irydae.Model
{
    public enum Groupe
    {
        [Description("cd")]
        [AmbientValue("Daënar")]
        Daenar,
        [Description("cm")]
        [AmbientValue("My'trän")]
        Mytran,
        [Description("cp")]
        [AmbientValue("Pérégrin")]
        Peregrin,
        [Description("ca")]
        [AmbientValue("Anomalie")]
        Anomalie,
        [Description("cr")]
        [AmbientValue("Régisseur")]
        Regisseur,
        [Description("car")]
        [AmbientValue("Architecte")]
        Architecte,
        [Description("pnj")]
        [AmbientValue("PNJ")]
        Pnj
    }
}