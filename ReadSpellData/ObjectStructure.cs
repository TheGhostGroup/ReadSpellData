using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadSpellData
{
    class ObjectStructure
    {
        public struct SMSG_SPELL_GO
        {
            public string Time;
            public string ObjectID;
            public string ObjectType;
            public string CasterGUID;
            public string CasterUnit;
            public string SpellID;
            public string CastFlags;
            public string CastFlagsEx;
            public string CasterTarget;
            public string CasterTargetID;
        };
    }
}
