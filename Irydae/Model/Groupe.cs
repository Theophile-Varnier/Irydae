using System.ComponentModel;

namespace Irydae.Model
{
    public enum Groupe
    {
        [Description("daenar")]
        [AmbientValue("Daënar")]
        Daenar,
        [Description("mytran")]
        [AmbientValue("My'trän")]
        Mytran,
        [Description("peregrin")]
        [AmbientValue("Pérégrin")]
        Peregrin,
        [Description("anomalie")]
        [AmbientValue("Anomalie")]
        Anomalie,
        [Description("regisseur")]
        [AmbientValue("Régisseur")]
        Regisseur,
        [Description("architecte")]
        [AmbientValue("Architecte")]
        Architecte,
        [Description("pnj")]
        [AmbientValue("PNJ")]
        Pnj
    }
}