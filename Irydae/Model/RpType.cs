using System.ComponentModel;

namespace Irydae.Model
{
    public enum RpType
    {
        [Description("balance")]
        [AmbientValue("Pesée")]
        Justice,

        [Description("besace")]
        [AmbientValue("Pique-nique")]
        Gouter,

        [Description("bourse")]
        [AmbientValue("Corruption")]
        Corruption,

        [Description("carte")]
        [AmbientValue("Gribouillage")]
        Gribouillage,

        [Description("chapeau")]
        [AmbientValue("Bal costumé")]
        Deguisement,

        [Description("gun")]
        [AmbientValue("Bagarre")]
        Bagarre,

        [Description("livre")]
        [AmbientValue("Rites sataniques")]
        Rite,

        [Description("quete")]
        [AmbientValue("Quayte")]
        Quete,

        [Description("social")]
        [AmbientValue("Commérage")]
        Commerage,

        [Description("roue")]
        [AmbientValue("Bidouillage")]
        Engrenage
    }
}