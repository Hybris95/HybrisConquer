using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConquerServer_Basic.Main_Classes
{
    public class NewMath
    {
        static public void ToLocal(byte[] Data, ushort X, ushort Y, int Map, int MapInstance, int ForbidID)
        {
            try
            {
                foreach (KeyValuePair<uint, GameClient> Tests in Kernel.GamePool)
                {
                    GameClient C = Tests.Value;
                    //if (Calculation.CanSee(X, Y, C.Client.Loc.X, C.Client.Loc.Y) && C.Client.ID != ForbidID && (int)C.Client.Loc.Map == Map)
                    if (Kernel.CanSee(X, Y, C.Entity.X, C.Entity.Y) && C.Entity.UID != ForbidID && C.Entity.MapID == Map)
                    {
                        C.Send(Data);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        static public void Vitals(GameClient Client)
        {
            //HP
            double HP = 0;
            double HpFactor = 24;

            // Vit
            #region Trojan
            if ((int)Client.Job == 11)
            {
                switch (Client.Vitality)
                {
                    #region Diff Vit Count
                    case 1:
                        HP += 25; break;
                    case 2:
                        HP += 50; break;
                    case 3:
                        HP += 75; break;
                    case 4:
                        HP += 100; break;
                    case 5:
                        HP += 126; break;
                    case 6:
                        HP += 151; break;
                    case 7:
                        HP += 176; break;
                    case 8:
                        HP += 201; break;
                    case 9:
                        HP += 226; break;
                    case 10:
                        HP += 252; break;
                    case 11:
                        HP += 277; break;
                    case 12:
                        HP += 302; break;
                    case 13:
                        HP += 327; break;
                    case 14:
                        HP += 352; break;
                    case 15:
                        HP += 378; break;
                    case 16:
                        HP += 403; break;
                    case 17:
                        HP += 428; break;
                    case 18:
                        HP += 453; break;
                    case 19:
                        HP += 478; break;
                    case 20:
                        HP += 504; break;
                    case 21:
                        HP += 529; break;
                    case 22:
                        HP += 554; break;
                    case 23:
                        HP += 579; break;
                    case 24:
                        HP += 604; break;
                    case 25:
                        HP += 630; break;
                    case 26:
                        HP += 655; break;
                    case 27:
                        HP += 680; break;
                    case 28:
                        HP += 705; break;
                    case 29:
                        HP += 730; break;
                    case 30:
                        HP += 756; break;
                    case 31:
                        HP += 781; break;
                    case 32:
                        HP += 806; break;
                    case 33:
                        HP += 831; break;
                    case 34:
                        HP += 856; break;
                    case 35:
                        HP += 882; break;
                    case 36:
                        HP += 907; break;
                    case 37:
                        HP += 932; break;
                    case 38:
                        HP += 957; break;
                    case 39:
                        HP += 982; break;
                    case 40:
                        HP += 1008; break;
                    case 41:
                        HP += 1033; break;
                    case 42:
                        HP += 1058; break;
                    case 43:
                        HP += 1083; break;
                    case 44:
                        HP += 1108; break;
                    case 45:
                        HP += 1134; break;
                    case 46:
                        HP += 1159; break;
                    case 47:
                        HP += 1184; break;
                    case 48:
                        HP += 1209; break;
                    case 49:
                        HP += 1234; break;
                    case 50:
                        HP += 1260; break;
                    case 51:
                        HP += 1285; break;
                    case 52:
                        HP += 1310; break;
                    case 53:
                        HP += 1335; break;
                    case 54:
                        HP += 1360; break;
                    case 55:
                        HP += 1386; break;
                    case 56:
                        HP += 1411; break;
                    case 57:
                        HP += 1436; break;
                    case 58:
                        HP += 1461; break;
                    case 59:
                        HP += 1486; break;
                    case 60:
                        HP += 1512; break;
                    case 61:
                        HP += 1537; break;
                    case 62:
                        HP += 1562; break;
                    case 63:
                        HP += 1587; break;
                    case 64:
                        HP += 1612; break;
                    case 65:
                        HP += 1638; break;
                    case 66:
                        HP += 1663; break;
                    case 67:
                        HP += 1688; break;
                    case 68:
                        HP += 1713; break;
                    case 69:
                        HP += 1738; break;
                    case 70:
                        HP += 1764; break;
                    case 71:
                        HP += 1789; break;
                    case 72:
                        HP += 1814; break;
                    case 73:
                        HP += 1839; break;
                    case 74:
                        HP += 1864; break;
                    case 75:
                        HP += 1890; break;
                    case 76:
                        HP += 1915; break;
                    case 77:
                        HP += 1940; break;
                    case 78:
                        HP += 1965; break;
                    case 79:
                        HP += 1990; break;
                    case 80:
                        HP += 2016; break;
                    case 81:
                        HP += 2041; break;
                    case 82:
                        HP += 2066; break;
                    case 83:
                        HP += 2091; break;
                    case 84:
                        HP += 2116; break;
                    case 85:
                        HP += 2142; break;
                    case 86:
                        HP += 2167; break;
                    case 87:
                        HP += 2192; break;
                    case 88:
                        HP += 2217; break;
                    case 89:
                        HP += 2242; break;
                    case 90:
                        HP += 2268; break;
                    case 91:
                        HP += 2293; break;
                    case 92:
                        HP += 2318; break;
                    case 93:
                        HP += 2343; break;
                    case 94:
                        HP += 2368; break;
                    case 95:
                        HP += 2394; break;
                    case 96:
                        HP += 2419; break;
                    case 97:
                        HP += 2444; break;
                    case 98:
                        HP += 2469; break;
                    case 99:
                        HP += 2494; break;
                    case 100:
                        HP += 2520; break;
                    case 101:
                        HP += 2545; break;
                    case 102:
                        HP += 2570; break;
                    case 103:
                        HP += 2595; break;
                    case 104:
                        HP += 2620; break;
                    case 105:
                        HP += 2646; break;
                    case 106:
                        HP += 2671; break;
                    case 107:
                        HP += 2696; break;
                    case 108:
                        HP += 2721; break;
                    case 109:
                        HP += 2746; break;
                    case 110:
                        HP += 2772; break;
                    case 111:
                        HP += 2797; break;
                    case 112:
                        HP += 2822; break;
                    case 113:
                        HP += 2847; break;
                    case 114:
                        HP += 2872; break;
                    case 115:
                        HP += 2898; break;
                    case 116:
                        HP += 2923; break;
                    case 117:
                        HP += 2948; break;
                    case 118:
                        HP += 2973; break;
                    case 119:
                        HP += 2998; break;
                    case 120:
                        HP += 3024; break;
                    case 121:
                        HP += 3049; break;
                    case 122:
                        HP += 3074; break;
                    case 123:
                        HP += 3099; break;
                    case 124:
                        HP += 3124; break;
                    case 125:
                        HP += 3150; break;
                    case 126:
                        HP += 3175; break;
                    case 127:
                        HP += 3200; break;
                    case 128:
                        HP += 3225; break;
                    case 129:
                        HP += 3250; break;
                    case 130:
                        HP += 3276; break;
                    case 131:
                        HP += 3301; break;
                    case 132:
                        HP += 3326; break;
                    case 133:
                        HP += 3351; break;
                    case 134:
                        HP += 3376; break;
                    case 135:
                        HP += 3402; break;
                    case 136:
                        HP += 3427; break;
                    case 137:
                        HP += 3452; break;
                    case 138:
                        HP += 3477; break;
                    case 139:
                        HP += 3502; break;
                    case 140:
                        HP += 3528; break;
                    case 141:
                        HP += 3553; break;
                    case 142:
                        HP += 3578; break;
                    case 143:
                        HP += 3603; break;
                    case 144:
                        HP += 3628; break;
                    case 145:
                        HP += 3654; break;
                    case 146:
                        HP += 3679; break;
                    case 147:
                        HP += 3704; break;
                    case 148:
                        HP += 3729; break;
                    case 149:
                        HP += 3754; break;
                    case 150:
                        HP += 3780; break;
                    case 151:
                        HP += 3805; break;
                    case 152:
                        HP += 3830; break;
                    case 153:
                        HP += 3855; break;
                    case 154:
                        HP += 3880; break;
                    case 155:
                        HP += 3906; break;
                    case 156:
                        HP += 3931; break;
                    case 157:
                        HP += 3956; break;
                    case 158:
                        HP += 3981; break;
                    case 159:
                        HP += 4006; break;
                    case 160:
                        HP += 4032; break;
                    case 161:
                        HP += 4057; break;
                    case 162:
                        HP += 4082; break;
                    case 163:
                        HP += 4107; break;
                    case 164:
                        HP += 4132; break;
                    case 165:
                        HP += 4158; break;
                    case 166:
                        HP += 4183; break;
                    case 167:
                        HP += 4208; break;
                    case 168:
                        HP += 4233; break;
                    case 169:
                        HP += 4258; break;
                    case 170:
                        HP += 4284; break;
                    case 171:
                        HP += 4309; break;
                    case 172:
                        HP += 4334; break;
                    case 173:
                        HP += 4359; break;
                    case 174:
                        HP += 4384; break;
                    case 175:
                        HP += 4410; break;
                    case 176:
                        HP += 4435; break;
                    case 177:
                        HP += 4460; break;
                    case 178:
                        HP += 4485; break;
                    case 179:
                        HP += 4510; break;
                    case 180:
                        HP += 4536; break;
                    case 181:
                        HP += 4561; break;
                    case 182:
                        HP += 4586; break;
                    case 183:
                        HP += 4611; break;
                    case 184:
                        HP += 4636; break;
                    case 185:
                        HP += 4662; break;
                    case 186:
                        HP += 4687; break;
                    case 187:
                        HP += 4712; break;
                    case 188:
                        HP += 4737; break;
                    case 189:
                        HP += 4762; break;
                    case 190:
                        HP += 4788; break;
                    case 191:
                        HP += 4813; break;
                    case 192:
                        HP += 4838; break;
                    case 193:
                        HP += 4863; break;
                    case 194:
                        HP += 4888; break;
                    case 195:
                        HP += 4914; break;
                    case 196:
                        HP += 4939; break;
                    case 197:
                        HP += 4964; break;
                    case 198:
                        HP += 4989; break;
                    case 199:
                        HP += 5014; break;
                    case 200:
                        HP += 5040; break;
                    #endregion
                }
            }
            #endregion
            #region VeteranTrojan
            else if ((int)Client.Job == 12)
            {
                switch (Client.Vitality)
                {
                    #region Diff Vit Count
                    case 1:
                        HP += 25; break;
                    case 2:
                        HP += 51; break;
                    case 3:
                        HP += 77; break;
                    case 4:
                        HP += 103; break;
                    case 5:
                        HP += 129; break;
                    case 6:
                        HP += 155; break;
                    case 7:
                        HP += 181; break;
                    case 8:
                        HP += 207; break;
                    case 9:
                        HP += 233; break;
                    case 10:
                        HP += 259; break;
                    case 11:
                        HP += 285; break;
                    case 12:
                        HP += 311; break;
                    case 13:
                        HP += 336; break;
                    case 14:
                        HP += 362; break;
                    case 15:
                        HP += 388; break;
                    case 16:
                        HP += 414; break;
                    case 17:
                        HP += 440; break;
                    case 18:
                        HP += 466; break;
                    case 19:
                        HP += 492; break;
                    case 20:
                        HP += 518; break;
                    case 21:
                        HP += 544; break;
                    case 22:
                        HP += 570; break;
                    case 23:
                        HP += 596; break;
                    case 24:
                        HP += 622; break;
                    case 25:
                        HP += 648; break;
                    case 26:
                        HP += 673; break;
                    case 27:
                        HP += 699; break;
                    case 28:
                        HP += 725; break;
                    case 29:
                        HP += 751; break;
                    case 30:
                        HP += 777; break;
                    case 31:
                        HP += 803; break;
                    case 32:
                        HP += 829; break;
                    case 33:
                        HP += 855; break;
                    case 34:
                        HP += 881; break;
                    case 35:
                        HP += 907; break;
                    case 36:
                        HP += 933; break;
                    case 37:
                        HP += 959; break;
                    case 38:
                        HP += 984; break;
                    case 39:
                        HP += 1010; break;
                    case 40:
                        HP += 1036; break;
                    case 41:
                        HP += 1062; break;
                    case 42:
                        HP += 1088; break;
                    case 43:
                        HP += 1114; break;
                    case 44:
                        HP += 1140; break;
                    case 45:
                        HP += 1166; break;
                    case 46:
                        HP += 1192; break;
                    case 47:
                        HP += 1218; break;
                    case 48:
                        HP += 1244; break;
                    case 49:
                        HP += 1270; break;
                    case 50:
                        HP += 1296; break;
                    case 51:
                        HP += 1321; break;
                    case 52:
                        HP += 1347; break;
                    case 53:
                        HP += 1373; break;
                    case 54:
                        HP += 1399; break;
                    case 55:
                        HP += 1425; break;
                    case 56:
                        HP += 1451; break;
                    case 57:
                        HP += 1477; break;
                    case 58:
                        HP += 1503; break;
                    case 59:
                        HP += 1529; break;
                    case 60:
                        HP += 1555; break;
                    case 61:
                        HP += 1581; break;
                    case 62:
                        HP += 1607; break;
                    case 63:
                        HP += 1632; break;
                    case 64:
                        HP += 1658; break;
                    case 65:
                        HP += 1684; break;
                    case 66:
                        HP += 1710; break;
                    case 67:
                        HP += 1736; break;
                    case 68:
                        HP += 1762; break;
                    case 69:
                        HP += 1788; break;
                    case 70:
                        HP += 1814; break;
                    case 71:
                        HP += 1840; break;
                    case 72:
                        HP += 1866; break;
                    case 73:
                        HP += 1892; break;
                    case 74:
                        HP += 1918; break;
                    case 75:
                        HP += 1944; break;
                    case 76:
                        HP += 1969; break;
                    case 77:
                        HP += 1995; break;
                    case 78:
                        HP += 2021; break;
                    case 79:
                        HP += 2047; break;
                    case 80:
                        HP += 2073; break;
                    case 81:
                        HP += 2099; break;
                    case 82:
                        HP += 2125; break;
                    case 83:
                        HP += 2151; break;
                    case 84:
                        HP += 2177; break;
                    case 85:
                        HP += 2203; break;
                    case 86:
                        HP += 2229; break;
                    case 87:
                        HP += 2255; break;
                    case 88:
                        HP += 2280; break;
                    case 89:
                        HP += 2306; break;
                    case 90:
                        HP += 2332; break;
                    case 91:
                        HP += 2358; break;
                    case 92:
                        HP += 2384; break;
                    case 93:
                        HP += 2410; break;
                    case 94:
                        HP += 2436; break;
                    case 95:
                        HP += 2462; break;
                    case 96:
                        HP += 2488; break;
                    case 97:
                        HP += 2514; break;
                    case 98:
                        HP += 2540; break;
                    case 99:
                        HP += 2566; break;
                    case 100:
                        HP += 2592; break;
                    case 101:
                        HP += 2617; break;
                    case 102:
                        HP += 2643; break;
                    case 103:
                        HP += 2669; break;
                    case 104:
                        HP += 2695; break;
                    case 105:
                        HP += 2721; break;
                    case 106:
                        HP += 2747; break;
                    case 107:
                        HP += 2773; break;
                    case 108:
                        HP += 2799; break;
                    case 109:
                        HP += 2825; break;
                    case 110:
                        HP += 2851; break;
                    case 111:
                        HP += 2877; break;
                    case 112:
                        HP += 2903; break;
                    case 113:
                        HP += 2928; break;
                    case 114:
                        HP += 2954; break;
                    case 115:
                        HP += 2980; break;
                    case 116:
                        HP += 3006; break;
                    case 117:
                        HP += 3032; break;
                    case 118:
                        HP += 3058; break;
                    case 119:
                        HP += 3084; break;
                    case 120:
                        HP += 3110; break;
                    case 121:
                        HP += 3136; break;
                    case 122:
                        HP += 3162; break;
                    case 123:
                        HP += 3188; break;
                    case 124:
                        HP += 3214; break;
                    case 125:
                        HP += 3240; break;
                    case 126:
                        HP += 3265; break;
                    case 127:
                        HP += 3291; break;
                    case 128:
                        HP += 3317; break;
                    case 129:
                        HP += 3343; break;
                    case 130:
                        HP += 3369; break;
                    case 131:
                        HP += 3395; break;
                    case 132:
                        HP += 3421; break;
                    case 133:
                        HP += 3447; break;
                    case 134:
                        HP += 3473; break;
                    case 135:
                        HP += 3499; break;
                    case 136:
                        HP += 3525; break;
                    case 137:
                        HP += 3551; break;
                    case 138:
                        HP += 3576; break;
                    case 139:
                        HP += 3602; break;
                    case 140:
                        HP += 3628; break;
                    case 141:
                        HP += 3654; break;
                    case 142:
                        HP += 3680; break;
                    case 143:
                        HP += 3706; break;
                    case 144:
                        HP += 3732; break;
                    case 145:
                        HP += 3758; break;
                    case 146:
                        HP += 3784; break;
                    case 147:
                        HP += 3810; break;
                    case 148:
                        HP += 3836; break;
                    case 149:
                        HP += 3862; break;
                    case 150:
                        HP += 3888; break;
                    case 151:
                        HP += 3913; break;
                    case 152:
                        HP += 3939; break;
                    case 153:
                        HP += 3965; break;
                    case 154:
                        HP += 3991; break;
                    case 155:
                        HP += 4017; break;
                    case 156:
                        HP += 4043; break;
                    case 157:
                        HP += 4069; break;
                    case 158:
                        HP += 4095; break;
                    case 159:
                        HP += 4121; break;
                    case 160:
                        HP += 4147; break;
                    case 161:
                        HP += 4173; break;
                    case 162:
                        HP += 4199; break;
                    case 163:
                        HP += 4224; break;
                    case 164:
                        HP += 4250; break;
                    case 165:
                        HP += 4276; break;
                    case 166:
                        HP += 4302; break;
                    case 167:
                        HP += 4328; break;
                    case 168:
                        HP += 4354; break;
                    case 169:
                        HP += 4380; break;
                    case 170:
                        HP += 4406; break;
                    case 171:
                        HP += 4432; break;
                    case 172:
                        HP += 4458; break;
                    case 173:
                        HP += 4484; break;
                    case 174:
                        HP += 4510; break;
                    case 175:
                        HP += 4536; break;
                    case 176:
                        HP += 4561; break;
                    case 177:
                        HP += 4587; break;
                    case 178:
                        HP += 4613; break;
                    case 179:
                        HP += 4639; break;
                    case 180:
                        HP += 4665; break;
                    case 181:
                        HP += 4691; break;
                    case 182:
                        HP += 4717; break;
                    case 183:
                        HP += 4743; break;
                    case 184:
                        HP += 4769; break;
                    case 185:
                        HP += 4795; break;
                    case 186:
                        HP += 4821; break;
                    case 187:
                        HP += 4847; break;
                    case 188:
                        HP += 4872; break;
                    case 189:
                        HP += 4898; break;
                    case 190:
                        HP += 4924; break;
                    case 191:
                        HP += 4950; break;
                    case 192:
                        HP += 4976; break;
                    case 193:
                        HP += 5002; break;
                    case 194:
                        HP += 5028; break;
                    case 195:
                        HP += 5054; break;
                    case 196:
                        HP += 5080; break;
                    case 197:
                        HP += 5106; break;
                    case 198:
                        HP += 5132; break;
                    case 199:
                        HP += 5158; break;
                    case 200:
                        HP += 5184; break;
                    #endregion
                }
            }
            #endregion
            #region TigerTrojan
            else if ((int)Client.Job == 13)
            {
                switch (Client.Vitality)
                {
                    #region Diff Vit Count
                    case 1:
                        HP += 26; break;
                    case 2:
                        HP += 52; break;
                    case 3:
                        HP += 79; break;
                    case 4:
                        HP += 105; break;
                    case 5:
                        HP += 132; break;
                    case 6:
                        HP += 158; break;
                    case 7:
                        HP += 184; break;
                    case 8:
                        HP += 211; break;
                    case 9:
                        HP += 237; break;
                    case 10:
                        HP += 264; break;
                    case 11:
                        HP += 290; break;
                    case 12:
                        HP += 316; break;
                    case 13:
                        HP += 343; break;
                    case 14:
                        HP += 369; break;
                    case 15:
                        HP += 396; break;
                    case 16:
                        HP += 442; break;
                    case 17:
                        HP += 448; break;
                    case 18:
                        HP += 475; break;
                    case 19:
                        HP += 501; break;
                    case 20:
                        HP += 528; break;
                    case 21:
                        HP += 554; break;
                    case 22:
                        HP += 580; break;
                    case 23:
                        HP += 607; break;
                    case 24:
                        HP += 633; break;
                    case 25:
                        HP += 660; break;
                    case 26:
                        HP += 686; break;
                    case 27:
                        HP += 712; break;
                    case 28:
                        HP += 739; break;
                    case 29:
                        HP += 765; break;
                    case 30:
                        HP += 792; break;
                    case 31:
                        HP += 818; break;
                    case 32:
                        HP += 844; break;
                    case 33:
                        HP += 871; break;
                    case 34:
                        HP += 897; break;
                    case 35:
                        HP += 924; break;
                    case 36:
                        HP += 950; break;
                    case 37:
                        HP += 976; break;
                    case 38:
                        HP += 1003; break;
                    case 39:
                        HP += 1029; break;
                    case 40:
                        HP += 1056; break;
                    case 41:
                        HP += 1082; break;
                    case 42:
                        HP += 1108; break;
                    case 43:
                        HP += 1135; break;
                    case 44:
                        HP += 1161; break;
                    case 45:
                        HP += 1188; break;
                    case 46:
                        HP += 1214; break;
                    case 47:
                        HP += 1240; break;
                    case 48:
                        HP += 1267; break;
                    case 49:
                        HP += 1293; break;
                    case 50:
                        HP += 1320; break;
                    case 51:
                        HP += 1346; break;
                    case 52:
                        HP += 1372; break;
                    case 53:
                        HP += 1399; break;
                    case 54:
                        HP += 1425; break;
                    case 55:
                        HP += 1452; break;
                    case 56:
                        HP += 1478; break;
                    case 57:
                        HP += 1504; break;
                    case 58:
                        HP += 1531; break;
                    case 59:
                        HP += 1557; break;
                    case 60:
                        HP += 1584; break;
                    case 61:
                        HP += 1610; break;
                    case 62:
                        HP += 1636; break;
                    case 63:
                        HP += 1663; break;
                    case 64:
                        HP += 1689; break;
                    case 65:
                        HP += 1716; break;
                    case 66:
                        HP += 1742; break;
                    case 67:
                        HP += 1768; break;
                    case 68:
                        HP += 1795; break;
                    case 69:
                        HP += 1821; break;
                    case 70:
                        HP += 1848; break;
                    case 71:
                        HP += 1874; break;
                    case 72:
                        HP += 1900; break;
                    case 73:
                        HP += 1927; break;
                    case 74:
                        HP += 1953; break;
                    case 75:
                        HP += 1980; break;
                    case 76:
                        HP += 2006; break;
                    case 77:
                        HP += 2032; break;
                    case 78:
                        HP += 2059; break;
                    case 79:
                        HP += 2085; break;
                    case 80:
                        HP += 2112; break;
                    case 81:
                        HP += 2138; break;
                    case 82:
                        HP += 2164; break;
                    case 83:
                        HP += 2191; break;
                    case 84:
                        HP += 2217; break;
                    case 85:
                        HP += 2244; break;
                    case 86:
                        HP += 2270; break;
                    case 87:
                        HP += 2296; break;
                    case 88:
                        HP += 2323; break;
                    case 89:
                        HP += 2349; break;
                    case 90:
                        HP += 2376; break;
                    case 91:
                        HP += 2402; break;
                    case 92:
                        HP += 2428; break;
                    case 93:
                        HP += 2455; break;
                    case 94:
                        HP += 2481; break;
                    case 95:
                        HP += 2508; break;
                    case 96:
                        HP += 2534; break;
                    case 97:
                        HP += 2560; break;
                    case 98:
                        HP += 2587; break;
                    case 99:
                        HP += 2613; break;
                    case 100:
                        HP += 2640; break;
                    case 101:
                        HP += 2666; break;
                    case 102:
                        HP += 2692; break;
                    case 103:
                        HP += 2719; break;
                    case 104:
                        HP += 2745; break;
                    case 105:
                        HP += 2772; break;
                    case 106:
                        HP += 2798; break;
                    case 107:
                        HP += 2824; break;
                    case 108:
                        HP += 2851; break;
                    case 109:
                        HP += 2877; break;
                    case 110:
                        HP += 2904; break;
                    case 111:
                        HP += 2930; break;
                    case 112:
                        HP += 2956; break;
                    case 113:
                        HP += 2983; break;
                    case 114:
                        HP += 3009; break;
                    case 115:
                        HP += 3036; break;
                    case 116:
                        HP += 3062; break;
                    case 117:
                        HP += 3088; break;
                    case 118:
                        HP += 3115; break;
                    case 119:
                        HP += 3141; break;
                    case 120:
                        HP += 3168; break;
                    case 121:
                        HP += 3194; break;
                    case 122:
                        HP += 3220; break;
                    case 123:
                        HP += 3247; break;
                    case 124:
                        HP += 3273; break;
                    case 125:
                        HP += 3300; break;
                    case 126:
                        HP += 3326; break;
                    case 127:
                        HP += 3352; break;
                    case 128:
                        HP += 3379; break;
                    case 129:
                        HP += 3405; break;
                    case 130:
                        HP += 3432; break;
                    case 131:
                        HP += 3458; break;
                    case 132:
                        HP += 3484; break;
                    case 133:
                        HP += 3511; break;
                    case 134:
                        HP += 3537; break;
                    case 135:
                        HP += 3564; break;
                    case 136:
                        HP += 3590; break;
                    case 137:
                        HP += 3616; break;
                    case 138:
                        HP += 3643; break;
                    case 139:
                        HP += 3669; break;
                    case 140:
                        HP += 3696; break;
                    case 141:
                        HP += 3722; break;
                    case 142:
                        HP += 3748; break;
                    case 143:
                        HP += 3775; break;
                    case 144:
                        HP += 3801; break;
                    case 145:
                        HP += 3828; break;
                    case 146:
                        HP += 3854; break;
                    case 147:
                        HP += 3880; break;
                    case 148:
                        HP += 3907; break;
                    case 149:
                        HP += 3933; break;
                    case 150:
                        HP += 3960; break;
                    case 151:
                        HP += 3986; break;
                    case 152:
                        HP += 4012; break;
                    case 153:
                        HP += 4039; break;
                    case 154:
                        HP += 4065; break;
                    case 155:
                        HP += 4092; break;
                    case 156:
                        HP += 4118; break;
                    case 157:
                        HP += 4144; break;
                    case 158:
                        HP += 4171; break;
                    case 159:
                        HP += 4197; break;
                    case 160:
                        HP += 4224; break;
                    case 161:
                        HP += 4250; break;
                    case 162:
                        HP += 4276; break;
                    case 163:
                        HP += 4303; break;
                    case 164:
                        HP += 4329; break;
                    case 165:
                        HP += 4356; break;
                    case 166:
                        HP += 4382; break;
                    case 167:
                        HP += 4408; break;
                    case 168:
                        HP += 4435; break;
                    case 169:
                        HP += 4461; break;
                    case 170:
                        HP += 4488; break;
                    case 171:
                        HP += 4514; break;
                    case 172:
                        HP += 4540; break;
                    case 173:
                        HP += 4567; break;
                    case 174:
                        HP += 4593; break;
                    case 175:
                        HP += 4620; break;
                    case 176:
                        HP += 4646; break;
                    case 177:
                        HP += 4672; break;
                    case 178:
                        HP += 4699; break;
                    case 179:
                        HP += 4725; break;
                    case 180:
                        HP += 4752; break;
                    case 181:
                        HP += 4778; break;
                    case 182:
                        HP += 4804; break;
                    case 183:
                        HP += 4831; break;
                    case 184:
                        HP += 4857; break;
                    case 185:
                        HP += 4884; break;
                    case 186:
                        HP += 4910; break;
                    case 187:
                        HP += 4936; break;
                    case 188:
                        HP += 4963; break;
                    case 189:
                        HP += 4989; break;
                    case 190:
                        HP += 5016; break;
                    case 191:
                        HP += 5042; break;
                    case 192:
                        HP += 5068; break;
                    case 193:
                        HP += 5095; break;
                    case 194:
                        HP += 5121; break;
                    case 195:
                        HP += 5148; break;
                    case 196:
                        HP += 5174; break;
                    case 197:
                        HP += 5200; break;
                    case 198:
                        HP += 5227; break;
                    case 199:
                        HP += 5253; break;
                    case 200:
                        HP += 5280; break;
                    #endregion
                }
            }
            #endregion
            #region DragonTrojan
            else if ((int)Client.Job == 14)
            {
                switch (Client.Vitality)
                {
                    #region Diff Vit Count
                    case 1:
                        HP += 26; break;
                    case 2:
                        HP += 53; break;
                    case 3:
                        HP += 80; break;
                    case 4:
                        HP += 107; break;
                    case 5:
                        HP += 134; break;
                    case 6:
                        HP += 161; break;
                    case 7:
                        HP += 188; break;
                    case 8:
                        HP += 215; break;
                    case 9:
                        HP += 241; break;
                    case 10:
                        HP += 268; break;
                    case 11:
                        HP += 295; break;
                    case 12:
                        HP += 322; break;
                    case 13:
                        HP += 349; break;
                    case 14:
                        HP += 376; break;
                    case 15:
                        HP += 403; break;
                    case 16:
                        HP += 430; break;
                    case 17:
                        HP += 456; break;
                    case 18:
                        HP += 483; break;
                    case 19:
                        HP += 510; break;
                    case 20:
                        HP += 537; break;
                    case 21:
                        HP += 564; break;
                    case 22:
                        HP += 591; break;
                    case 23:
                        HP += 618; break;
                    case 24:
                        HP += 645; break;
                    case 25:
                        HP += 672; break;
                    case 26:
                        HP += 698; break;
                    case 27:
                        HP += 725; break;
                    case 28:
                        HP += 752; break;
                    case 29:
                        HP += 779; break;
                    case 30:
                        HP += 806; break;
                    case 31:
                        HP += 833; break;
                    case 32:
                        HP += 860; break;
                    case 33:
                        HP += 887; break;
                    case 34:
                        HP += 913; break;
                    case 35:
                        HP += 940; break;
                    case 36:
                        HP += 967; break;
                    case 37:
                        HP += 994; break;
                    case 38:
                        HP += 1021; break;
                    case 39:
                        HP += 1048; break;
                    case 40:
                        HP += 1075; break;
                    case 41:
                        HP += 1102; break;
                    case 42:
                        HP += 1128; break;
                    case 43:
                        HP += 1155; break;
                    case 44:
                        HP += 1182; break;
                    case 45:
                        HP += 1209; break;
                    case 46:
                        HP += 1236; break;
                    case 47:
                        HP += 1263; break;
                    case 48:
                        HP += 1290; break;
                    case 49:
                        HP += 1317; break;
                    case 50:
                        HP += 1344; break;
                    case 51:
                        HP += 1370; break;
                    case 52:
                        HP += 1397; break;
                    case 53:
                        HP += 1424; break;
                    case 54:
                        HP += 1451; break;
                    case 55:
                        HP += 1478; break;
                    case 56:
                        HP += 1505; break;
                    case 57:
                        HP += 1532; break;
                    case 58:
                        HP += 1559; break;
                    case 59:
                        HP += 1585; break;
                    case 60:
                        HP += 1612; break;
                    case 61:
                        HP += 1639; break;
                    case 62:
                        HP += 1666; break;
                    case 63:
                        HP += 1693; break;
                    case 64:
                        HP += 1720; break;
                    case 65:
                        HP += 1747; break;
                    case 66:
                        HP += 1774; break;
                    case 67:
                        HP += 1800; break;
                    case 68:
                        HP += 1827; break;
                    case 69:
                        HP += 1854; break;
                    case 70:
                        HP += 1881; break;
                    case 71:
                        HP += 1908; break;
                    case 72:
                        HP += 1935; break;
                    case 73:
                        HP += 1962; break;
                    case 74:
                        HP += 1989; break;
                    case 75:
                        HP += 2016; break;
                    case 76:
                        HP += 2042; break;
                    case 77:
                        HP += 2069; break;
                    case 78:
                        HP += 2096; break;
                    case 79:
                        HP += 2123; break;
                    case 80:
                        HP += 2150; break;
                    case 81:
                        HP += 2177; break;
                    case 82:
                        HP += 2204; break;
                    case 83:
                        HP += 2231; break;
                    case 84:
                        HP += 2257; break;
                    case 85:
                        HP += 2284; break;
                    case 86:
                        HP += 2311; break;
                    case 87:
                        HP += 2338; break;
                    case 88:
                        HP += 2365; break;
                    case 89:
                        HP += 2392; break;
                    case 90:
                        HP += 2419; break;
                    case 91:
                        HP += 2446; break;
                    case 92:
                        HP += 2472; break;
                    case 93:
                        HP += 2499; break;
                    case 94:
                        HP += 2526; break;
                    case 95:
                        HP += 2553; break;
                    case 96:
                        HP += 2580; break;
                    case 97:
                        HP += 2607; break;
                    case 98:
                        HP += 2634; break;
                    case 99:
                        HP += 2661; break;
                    case 100:
                        HP += 2688; break;
                    case 101:
                        HP += 2714; break;
                    case 102:
                        HP += 2741; break;
                    case 103:
                        HP += 2768; break;
                    case 104:
                        HP += 2795; break;
                    case 105:
                        HP += 2822; break;
                    case 106:
                        HP += 2849; break;
                    case 107:
                        HP += 2876; break;
                    case 108:
                        HP += 2903; break;
                    case 109:
                        HP += 2929; break;
                    case 110:
                        HP += 2956; break;
                    case 111:
                        HP += 2983; break;
                    case 112:
                        HP += 3010; break;
                    case 113:
                        HP += 3037; break;
                    case 114:
                        HP += 3064; break;
                    case 115:
                        HP += 3091; break;
                    case 116:
                        HP += 3118; break;
                    case 117:
                        HP += 3144; break;
                    case 118:
                        HP += 3171; break;
                    case 119:
                        HP += 3198; break;
                    case 120:
                        HP += 3225; break;
                    case 121:
                        HP += 3252; break;
                    case 122:
                        HP += 3279; break;
                    case 123:
                        HP += 3306; break;
                    case 124:
                        HP += 3333; break;
                    case 125:
                        HP += 3360; break;
                    case 126:
                        HP += 3386; break;
                    case 127:
                        HP += 3413; break;
                    case 128:
                        HP += 3440; break;
                    case 129:
                        HP += 3467; break;
                    case 130:
                        HP += 3494; break;
                    case 131:
                        HP += 3521; break;
                    case 132:
                        HP += 3548; break;
                    case 133:
                        HP += 3575; break;
                    case 134:
                        HP += 3601; break;
                    case 135:
                        HP += 3628; break;
                    case 136:
                        HP += 3655; break;
                    case 137:
                        HP += 3682; break;
                    case 138:
                        HP += 3709; break;
                    case 139:
                        HP += 3736; break;
                    case 140:
                        HP += 3763; break;
                    case 141:
                        HP += 3790; break;
                    case 142:
                        HP += 3816; break;
                    case 143:
                        HP += 3843; break;
                    case 144:
                        HP += 3870; break;
                    case 145:
                        HP += 3897; break;
                    case 146:
                        HP += 3924; break;
                    case 147:
                        HP += 3951; break;
                    case 148:
                        HP += 3978; break;
                    case 149:
                        HP += 4005; break;
                    case 150:
                        HP += 4032; break;
                    case 151:
                        HP += 4058; break;
                    case 152:
                        HP += 4085; break;
                    case 153:
                        HP += 4112; break;
                    case 154:
                        HP += 4139; break;
                    case 155:
                        HP += 4166; break;
                    case 156:
                        HP += 4193; break;
                    case 157:
                        HP += 4220; break;
                    case 158:
                        HP += 4247; break;
                    case 159:
                        HP += 4273; break;
                    case 160:
                        HP += 4300; break;
                    case 161:
                        HP += 4327; break;
                    case 162:
                        HP += 4354; break;
                    case 163:
                        HP += 4381; break;
                    case 164:
                        HP += 4408; break;
                    case 165:
                        HP += 4435; break;
                    case 166:
                        HP += 4462; break;
                    case 167:
                        HP += 4488; break;
                    case 168:
                        HP += 4515; break;
                    case 169:
                        HP += 4542; break;
                    case 170:
                        HP += 4569; break;
                    case 171:
                        HP += 4596; break;
                    case 172:
                        HP += 4623; break;
                    case 173:
                        HP += 4650; break;
                    case 174:
                        HP += 4677; break;
                    case 175:
                        HP += 4704; break;
                    case 176:
                        HP += 4730; break;
                    case 177:
                        HP += 4757; break;
                    case 178:
                        HP += 4784; break;
                    case 179:
                        HP += 4811; break;
                    case 180:
                        HP += 4838; break;
                    case 181:
                        HP += 4865; break;
                    case 182:
                        HP += 4892; break;
                    case 183:
                        HP += 4919; break;
                    case 184:
                        HP += 4945; break;
                    case 185:
                        HP += 4972; break;
                    case 186:
                        HP += 4999; break;
                    case 187:
                        HP += 5026; break;
                    case 188:
                        HP += 5053; break;
                    case 189:
                        HP += 5080; break;
                    case 190:
                        HP += 5107; break;
                    case 191:
                        HP += 5134; break;
                    case 192:
                        HP += 5160; break;
                    case 193:
                        HP += 5187; break;
                    case 194:
                        HP += 5214; break;
                    case 195:
                        HP += 5241; break;
                    case 196:
                        HP += 5268; break;
                    case 197:
                        HP += 5295; break;
                    case 198:
                        HP += 5322; break;
                    case 199:
                        HP += 5349; break;
                    case 200:
                        HP += 5376; break;
                    #endregion
                }
            }
            #endregion
            #region TrojanMaster
            else if ((int)Client.Job == 15)
            {
                switch (Client.Vitality)
                {
                    #region Diff Vit Count
                    case 1:
                        HP += 27; break;
                    case 2:
                        HP += 55; break;
                    case 3:
                        HP += 82; break;
                    case 4:
                        HP += 110; break;
                    case 5:
                        HP += 138; break;
                    case 6:
                        HP += 165; break;
                    case 7:
                        HP += 193; break;
                    case 8:
                        HP += 220; break;
                    case 9:
                        HP += 248; break;
                    case 10:
                        HP += 276; break;
                    case 11:
                        HP += 303; break;
                    case 12:
                        HP += 331; break;
                    case 13:
                        HP += 358; break;
                    case 14:
                        HP += 386; break;
                    case 15:
                        HP += 414; break;
                    case 16:
                        HP += 441; break;
                    case 17:
                        HP += 469; break;
                    case 18:
                        HP += 496; break;
                    case 19:
                        HP += 524; break;
                    case 20:
                        HP += 552; break;
                    case 21:
                        HP += 579; break;
                    case 22:
                        HP += 607; break;
                    case 23:
                        HP += 634; break;
                    case 24:
                        HP += 662; break;
                    case 25:
                        HP += 690; break;
                    case 26:
                        HP += 717; break;
                    case 27:
                        HP += 745; break;
                    case 28:
                        HP += 772; break;
                    case 29:
                        HP += 800; break;
                    case 30:
                        HP += 828; break;
                    case 31:
                        HP += 855; break;
                    case 32:
                        HP += 883; break;
                    case 33:
                        HP += 910; break;
                    case 34:
                        HP += 938; break;
                    case 35:
                        HP += 966; break;
                    case 36:
                        HP += 993; break;
                    case 37:
                        HP += 1021; break;
                    case 38:
                        HP += 1048; break;
                    case 39:
                        HP += 1076; break;
                    case 40:
                        HP += 1104; break;
                    case 41:
                        HP += 1131; break;
                    case 42:
                        HP += 1159; break;
                    case 43:
                        HP += 1186; break;
                    case 44:
                        HP += 1214; break;
                    case 45:
                        HP += 1242; break;
                    case 46:
                        HP += 1269; break;
                    case 47:
                        HP += 1297; break;
                    case 48:
                        HP += 1324; break;
                    case 49:
                        HP += 1352; break;
                    case 50:
                        HP += 1380; break;
                    case 51:
                        HP += 1407; break;
                    case 52:
                        HP += 1435; break;
                    case 53:
                        HP += 1462; break;
                    case 54:
                        HP += 1490; break;
                    case 55:
                        HP += 1518; break;
                    case 56:
                        HP += 1545; break;
                    case 57:
                        HP += 1573; break;
                    case 58:
                        HP += 1600; break;
                    case 59:
                        HP += 1628; break;
                    case 60:
                        HP += 1656; break;
                    case 61:
                        HP += 1683; break;
                    case 62:
                        HP += 1711; break;
                    case 63:
                        HP += 1738; break;
                    case 64:
                        HP += 1766; break;
                    case 65:
                        HP += 1794; break;
                    case 66:
                        HP += 1821; break;
                    case 67:
                        HP += 1849; break;
                    case 68:
                        HP += 1876; break;
                    case 69:
                        HP += 1904; break;
                    case 70:
                        HP += 1932; break;
                    case 71:
                        HP += 1959; break;
                    case 72:
                        HP += 1987; break;
                    case 73:
                        HP += 2014; break;
                    case 74:
                        HP += 2042; break;
                    case 75:
                        HP += 2070; break;
                    case 76:
                        HP += 2097; break;
                    case 77:
                        HP += 2125; break;
                    case 78:
                        HP += 2152; break;
                    case 79:
                        HP += 2180; break;
                    case 80:
                        HP += 2208; break;
                    case 81:
                        HP += 2235; break;
                    case 82:
                        HP += 2263; break;
                    case 83:
                        HP += 2290; break;
                    case 84:
                        HP += 2318; break;
                    case 85:
                        HP += 2346; break;
                    case 86:
                        HP += 2373; break;
                    case 87:
                        HP += 2401; break;
                    case 88:
                        HP += 2428; break;
                    case 89:
                        HP += 2456; break;
                    case 90:
                        HP += 2484; break;
                    case 91:
                        HP += 2511; break;
                    case 92:
                        HP += 2539; break;
                    case 93:
                        HP += 2566; break;
                    case 94:
                        HP += 2594; break;
                    case 95:
                        HP += 2622; break;
                    case 96:
                        HP += 2649; break;
                    case 97:
                        HP += 2677; break;
                    case 98:
                        HP += 2704; break;
                    case 99:
                        HP += 2732; break;
                    case 100:
                        HP += 2760; break;
                    case 101:
                        HP += 2787; break;
                    case 102:
                        HP += 2815; break;
                    case 103:
                        HP += 2842; break;
                    case 104:
                        HP += 2870; break;
                    case 105:
                        HP += 2898; break;
                    case 106:
                        HP += 2925; break;
                    case 107:
                        HP += 2953; break;
                    case 108:
                        HP += 2980; break;
                    case 109:
                        HP += 3008; break;
                    case 110:
                        HP += 3036; break;
                    case 111:
                        HP += 3063; break;
                    case 112:
                        HP += 3091; break;
                    case 113:
                        HP += 3118; break;
                    case 114:
                        HP += 3146; break;
                    case 115:
                        HP += 3174; break;
                    case 116:
                        HP += 3201; break;
                    case 117:
                        HP += 3229; break;
                    case 118:
                        HP += 3256; break;
                    case 119:
                        HP += 3284; break;
                    case 120:
                        HP += 3312; break;
                    case 121:
                        HP += 3339; break;
                    case 122:
                        HP += 3367; break;
                    case 123:
                        HP += 3394; break;
                    case 124:
                        HP += 3422; break;
                    case 125:
                        HP += 3450; break;
                    case 126:
                        HP += 3477; break;
                    case 127:
                        HP += 3505; break;
                    case 128:
                        HP += 3532; break;
                    case 129:
                        HP += 3560; break;
                    case 130:
                        HP += 3588; break;
                    case 131:
                        HP += 3615; break;
                    case 132:
                        HP += 3643; break;
                    case 133:
                        HP += 3670; break;
                    case 134:
                        HP += 3698; break;
                    case 135:
                        HP += 3726; break;
                    case 136:
                        HP += 3753; break;
                    case 137:
                        HP += 3781; break;
                    case 138:
                        HP += 3808; break;
                    case 139:
                        HP += 3836; break;
                    case 140:
                        HP += 3864; break;
                    case 141:
                        HP += 3891; break;
                    case 142:
                        HP += 3919; break;
                    case 143:
                        HP += 3946; break;
                    case 144:
                        HP += 3974; break;
                    case 145:
                        HP += 4002; break;
                    case 146:
                        HP += 4029; break;
                    case 147:
                        HP += 4057; break;
                    case 148:
                        HP += 4084; break;
                    case 149:
                        HP += 4112; break;
                    case 150:
                        HP += 4140; break;
                    case 151:
                        HP += 4167; break;
                    case 152:
                        HP += 4195; break;
                    case 153:
                        HP += 4222; break;
                    case 154:
                        HP += 4250; break;
                    case 155:
                        HP += 4278; break;
                    case 156:
                        HP += 4305; break;
                    case 157:
                        HP += 4333; break;
                    case 158:
                        HP += 4360; break;
                    case 159:
                        HP += 4388; break;
                    case 160:
                        HP += 4416; break;
                    case 161:
                        HP += 4443; break;
                    case 162:
                        HP += 4471; break;
                    case 163:
                        HP += 4498; break;
                    case 164:
                        HP += 4526; break;
                    case 165:
                        HP += 4554; break;
                    case 166:
                        HP += 4581; break;
                    case 167:
                        HP += 4609; break;
                    case 168:
                        HP += 4636; break;
                    case 169:
                        HP += 4664; break;
                    case 170:
                        HP += 4692; break;
                    case 171:
                        HP += 4719; break;
                    case 172:
                        HP += 4747; break;
                    case 173:
                        HP += 4774; break;
                    case 174:
                        HP += 4802; break;
                    case 175:
                        HP += 4830; break;
                    case 176:
                        HP += 4857; break;
                    case 177:
                        HP += 4885; break;
                    case 178:
                        HP += 4912; break;
                    case 179:
                        HP += 4940; break;
                    case 180:
                        HP += 4968; break;
                    case 181:
                        HP += 4995; break;
                    case 182:
                        HP += 5023; break;
                    case 183:
                        HP += 5050; break;
                    case 184:
                        HP += 5078; break;
                    case 185:
                        HP += 5106; break;
                    case 186:
                        HP += 5133; break;
                    case 187:
                        HP += 5161; break;
                    case 188:
                        HP += 5188; break;
                    case 189:
                        HP += 5216; break;
                    case 190:
                        HP += 5244; break;
                    case 191:
                        HP += 5271; break;
                    case 192:
                        HP += 5299; break;
                    case 193:
                        HP += 5326; break;
                    case 194:
                        HP += 5354; break;
                    case 195:
                        HP += 5382; break;
                    case 196:
                        HP += 5409; break;
                    case 197:
                        HP += 5437; break;
                    case 198:
                        HP += 5464; break;
                    case 199:
                        HP += 5492; break;
                    case 200:
                        HP += 5520; break;
                    case 201:
                        HP += 5547; break;
                    case 202:
                        HP += 5575; break;
                    case 203:
                        HP += 5602; break;
                    case 204:
                        HP += 5630; break;
                    case 205:
                        HP += 5658; break;
                    case 206:
                        HP += 5685; break;
                    case 207:
                        HP += 5713; break;
                    case 208:
                        HP += 5740; break;
                    case 209:
                        HP += 5768; break;
                    case 210:
                        HP += 5796; break;
                    case 211:
                        HP += 5823; break;
                    case 212:
                        HP += 5851; break;
                    case 213:
                        HP += 5878; break;
                    case 214:
                        HP += 5906; break;
                    case 215:
                        HP += 5934; break;
                    case 216:
                        HP += 5961; break;
                    case 217:
                        HP += 5989; break;
                    case 218:
                        HP += 6016; break;
                    case 219:
                        HP += 6044; break;
                    case 220:
                        HP += 6072; break;
                    case 221:
                        HP += 6099; break;
                    case 222:
                        HP += 6127; break;
                    case 223:
                        HP += 6154; break;
                    case 224:
                        HP += 6182; break;
                    case 225:
                        HP += 6210; break;
                    case 226:
                        HP += 6237; break;
                    case 227:
                        HP += 6265; break;
                    case 228:
                        HP += 6292; break;
                    case 229:
                        HP += 6320; break;
                    case 230:
                        HP += 6348; break;
                    case 231:
                        HP += 6375; break;
                    case 232:
                        HP += 6403; break;
                    case 233:
                        HP += 6430; break;
                    case 234:
                        HP += 6458; break;
                    case 235:
                        HP += 6486; break;
                    case 236:
                        HP += 6513; break;
                    case 237:
                        HP += 6541; break;
                    case 238:
                        HP += 6568; break;
                    case 239:
                        HP += 6596; break;
                    case 240:
                        HP += 6624; break;
                    case 241:
                        HP += 6651; break;
                    case 242:
                        HP += 6679; break;
                    case 243:
                        HP += 6706; break;
                    case 244:
                        HP += 6734; break;
                    case 245:
                        HP += 6762; break;
                    case 246:
                        HP += 6789; break;
                    case 247:
                        HP += 6817; break;
                    case 248:
                        HP += 6844; break;
                    case 249:
                        HP += 6872; break;
                    case 250:
                        HP += 6900; break;
                    case 251:
                        HP += 6927; break;
                    case 252:
                        HP += 6955; break;
                    case 253:
                        HP += 6982; break;
                    case 254:
                        HP += 7010; break;
                    case 255:
                        HP += 7038; break;
                    case 256:
                        HP += 7065; break;
                    case 257:
                        HP += 7093; break;
                    case 258:
                        HP += 7120; break;
                    case 259:
                        HP += 7148; break;
                    case 260:
                        HP += 7176; break;
                    case 261:
                        HP += 7203; break;
                    case 262:
                        HP += 7231; break;
                    case 263:
                        HP += 7258; break;
                    case 264:
                        HP += 7286; break;
                    case 265:
                        HP += 7314; break;
                    case 266:
                        HP += 7341; break;
                    case 267:
                        HP += 4369; break;
                    case 268:
                        HP += 4396; break;
                    case 269:
                        HP += 7424; break;
                    case 270:
                        HP += 7452; break;
                    case 271:
                        HP += 7479; break;
                    case 272:
                        HP += 7507; break;
                    case 273:
                        HP += 7534; break;
                    case 274:
                        HP += 7562; break;
                    case 275:
                        HP += 7590; break;
                    case 276:
                        HP += 7617; break;
                    case 277:
                        HP += 7645; break;
                    case 278:
                        HP += 7672; break;
                    case 279:
                        HP += 7700; break;
                    case 280:
                        HP += 7728; break;
                    case 281:
                        HP += 7755; break;
                    case 282:
                        HP += 7783; break;
                    case 283:
                        HP += 7810; break;
                    case 284:
                        HP += 7838; break;
                    case 285:
                        HP += 7866; break;
                    case 286:
                        HP += 7893; break;
                    case 287:
                        HP += 7921; break;
                    case 288:
                        HP += 7948; break;
                    case 289:
                        HP += 7976; break;
                    case 290:
                        HP += 8004; break;
                    case 291:
                        HP += 8031; break;
                    case 292:
                        HP += 8059; break;
                    case 293:
                        HP += 8086; break;
                    case 294:
                        HP += 8114; break;
                    case 295:
                        HP += 8142; break;
                    case 296:
                        HP += 8169; break;
                    case 297:
                        HP += 8197; break;
                    case 298:
                        HP += 8224; break;
                    case 299:
                        HP += 8252; break;
                    case 300:
                        HP += 8280; break;
                    #endregion
                }
            }
            #endregion
            else
            {
                HP += (Client.Vitality * HpFactor);
            }

            //Strength
            #region Trojan
            if ((int)Client.Job == 11)
            {
                switch (Client.Strength)
                {
                    #region Diff Str Count
                    case 1:
                        HP += 3; break;
                    case 2:
                        HP += 6; break;
                    case 3:
                        HP += 9; break;
                    case 4:
                        HP += 12; break;
                    case 5:
                        HP += 15; break;
                    case 6:
                        HP += 18; break;
                    case 7:
                        HP += 22; break;
                    case 8:
                        HP += 25; break;
                    case 9:
                        HP += 28; break;
                    case 10:
                        HP += 31; break;
                    case 11:
                        HP += 34; break;
                    case 12:
                        HP += 37; break;
                    case 13:
                        HP += 40; break;
                    case 14:
                        HP += 44; break;
                    case 15:
                        HP += 47; break;
                    case 16:
                        HP += 50; break;
                    case 17:
                        HP += 53; break;
                    case 18:
                        HP += 56; break;
                    case 19:
                        HP += 59; break;
                    case 20:
                        HP += 63; break;
                    case 21:
                        HP += 66; break;
                    case 22:
                        HP += 69; break;
                    case 23:
                        HP += 72; break;
                    case 24:
                        HP += 75; break;
                    case 25:
                        HP += 78; break;
                    case 26:
                        HP += 81; break;
                    case 27:
                        HP += 85; break;
                    case 28:
                        HP += 88; break;
                    case 29:
                        HP += 91; break;
                    case 30:
                        HP += 94; break;
                    case 31:
                        HP += 97; break;
                    case 32:
                        HP += 100; break;
                    case 33:
                        HP += 103; break;
                    case 34:
                        HP += 107; break;
                    case 35:
                        HP += 110; break;
                    case 36:
                        HP += 113; break;
                    case 37:
                        HP += 116; break;
                    case 38:
                        HP += 119; break;
                    case 39:
                        HP += 122; break;
                    case 40:
                        HP += 126; break;
                    case 41:
                        HP += 129; break;
                    case 42:
                        HP += 132; break;
                    case 43:
                        HP += 135; break;
                    case 44:
                        HP += 138; break;
                    case 45:
                        HP += 141; break;
                    case 46:
                        HP += 144; break;
                    case 47:
                        HP += 148; break;
                    case 48:
                        HP += 151; break;
                    case 49:
                        HP += 154; break;
                    case 50:
                        HP += 157; break;
                    case 51:
                        HP += 160; break;
                    case 52:
                        HP += 163; break;
                    case 53:
                        HP += 166; break;
                    case 54:
                        HP += 170; break;
                    case 55:
                        HP += 173; break;
                    case 56:
                        HP += 176; break;
                    case 57:
                        HP += 179; break;
                    case 58:
                        HP += 182; break;
                    case 59:
                        HP += 185; break;
                    case 60:
                        HP += 189; break;
                    case 61:
                        HP += 192; break;
                    case 62:
                        HP += 195; break;
                    case 63:
                        HP += 198; break;
                    case 64:
                        HP += 201; break;
                    case 65:
                        HP += 204; break;
                    case 66:
                        HP += 207; break;
                    case 67:
                        HP += 211; break;
                    case 68:
                        HP += 214; break;
                    case 69:
                        HP += 217; break;
                    case 70:
                        HP += 220; break;
                    case 71:
                        HP += 223; break;
                    case 72:
                        HP += 226; break;
                    case 73:
                        HP += 229; break;
                    case 74:
                        HP += 233; break;
                    case 75:
                        HP += 236; break;
                    case 76:
                        HP += 239; break;
                    case 77:
                        HP += 242; break;
                    case 78:
                        HP += 245; break;
                    case 79:
                        HP += 248; break;
                    case 80:
                        HP += 252; break;
                    case 81:
                        HP += 255; break;
                    case 82:
                        HP += 258; break;
                    case 83://
                        HP += 261; break;//
                    case 84:
                        HP += 264; break;
                    case 85:
                        HP += 267; break;
                    case 86:
                        HP += 270; break;
                    case 87:
                        HP += 274; break;
                    case 88:
                        HP += 277; break;
                    case 89:
                        HP += 280; break;
                    case 90:
                        HP += 283; break;
                    case 91:
                        HP += 286; break;
                    case 92:
                        HP += 289; break;
                    case 93:
                        HP += 292; break;
                    case 94:
                        HP += 296; break;
                    case 95:
                        HP += 299; break;
                    case 96:
                        HP += 302; break;
                    case 97:
                        HP += 305; break;
                    case 98:
                        HP += 308; break;
                    case 99:
                        HP += 311; break;
                    case 100:
                        HP += 315; break;
                    case 101:
                        HP += 318; break;
                    case 102:
                        HP += 321; break;
                    case 103:
                        HP += 324; break;
                    case 104:
                        HP += 327; break;
                    case 105:
                        HP += 330; break;
                    case 106:
                        HP += 333; break;
                    case 107:
                        HP += 337; break;
                    case 108:
                        HP += 340; break;
                    case 109:
                        HP += 343; break;
                    case 110:
                        HP += 346; break;
                    case 111:
                        HP += 349; break;
                    case 112:
                        HP += 352; break;
                    case 113:
                        HP += 355; break;
                    case 114:
                        HP += 359; break;
                    case 115:
                        HP += 362; break;
                    case 116:
                        HP += 365; break;
                    case 117:
                        HP += 368; break;
                    case 118:
                        HP += 371; break;
                    case 119:
                        HP += 374; break;
                    case 120:
                        HP += 378; break;
                    case 121:
                        HP += 381; break;
                    case 122:
                        HP += 384; break;
                    case 123:
                        HP += 387; break;
                    case 124:
                        HP += 390; break;
                    case 125:
                        HP += 393; break;
                    case 126:
                        HP += 396; break;
                    case 127:
                        HP += 400; break;
                    case 128:
                        HP += 403; break;
                    case 129:
                        HP += 406; break;
                    case 130:
                        HP += 409; break;
                    case 131:
                        HP += 412; break;
                    case 132:
                        HP += 415; break;
                    case 133:
                        HP += 419; break;
                    case 134:
                        HP += 422; break;
                    case 135:
                        HP += 425; break;
                    case 136:
                        HP += 428; break;
                    case 137:
                        HP += 431; break;
                    case 138:
                        HP += 434; break;
                    case 139:
                        HP += 437; break;
                    case 140:
                        HP += 441; break;
                    case 141:
                        HP += 444; break;
                    case 142:
                        HP += 447; break;
                    case 143:
                        HP += 450; break;
                    case 144:
                        HP += 453; break;
                    case 145:
                        HP += 456; break;
                    case 146:
                        HP += 459; break;
                    case 147:
                        HP += 463; break;
                    case 148:
                        HP += 466; break;
                    case 149:
                        HP += 469; break;
                    case 150:
                        HP += 472; break;
                    case 151:
                        HP += 475; break;
                    case 152:
                        HP += 478; break;
                    case 153:
                        HP += 481; break;
                    case 154:
                        HP += 485; break;
                    case 155:
                        HP += 488; break;
                    case 156:
                        HP += 491; break;
                    case 157:
                        HP += 494; break;
                    case 158:
                        HP += 497; break;
                    case 159:
                        HP += 500; break;
                    case 160:
                        HP += 504; break;
                    case 161:
                        HP += 507; break;
                    case 162:
                        HP += 510; break;
                    case 163:
                        HP += 513; break;
                    case 164:
                        HP += 516; break;
                    case 165:
                        HP += 519; break;
                    case 166:
                        HP += 522; break;
                    case 167:
                        HP += 526; break;
                    case 168:
                        HP += 529; break;
                    case 169:
                        HP += 532; break;
                    case 170:
                        HP += 535; break;
                    case 171:
                        HP += 538; break;
                    case 172:
                        HP += 541; break;
                    case 173:
                        HP += 544; break;
                    case 174:
                        HP += 548; break;
                    case 175:
                        HP += 551; break;
                    case 176:
                        HP += 554; break;
                    case 177:
                        HP += 557; break;
                    case 178:
                        HP += 560; break;
                    case 179:
                        HP += 563; break;
                    case 180:
                        HP += 567; break;
                    case 181:
                        HP += 570; break;
                    case 182:
                        HP += 573; break;
                    case 183:
                        HP += 576; break;
                    case 184:
                        HP += 579; break;
                    case 185:
                        HP += 582; break;
                    case 186:
                        HP += 585; break;
                    case 187:
                        HP += 589; break;
                    case 188:
                        HP += 592; break;
                    case 189:
                        HP += 595; break;
                    case 190:
                        HP += 598; break;
                    case 191:
                        HP += 601; break;
                    case 192:
                        HP += 604; break;
                    case 193:
                        HP += 607; break;
                    case 194:
                        HP += 611; break;
                    case 195:
                        HP += 614; break;
                    case 196:
                        HP += 617; break;
                    case 197:
                        HP += 620; break;
                    case 198:
                        HP += 623; break;
                    case 199:
                        HP += 626; break;
                    case 200:
                        HP += 630; break;
                    #endregion
                }
            }
            #endregion
            #region VeteranTrojan
            else if ((int)Client.Job == 12)
            {
                switch (Client.Strength)
                {
                    #region Diff Str Count
                    case 1:
                        HP += 3; break;
                    case 2:
                        HP += 6; break;
                    case 3:
                        HP += 9; break;
                    case 4:
                        HP += 12; break;
                    case 5:
                        HP += 16; break;
                    case 6:
                        HP += 19; break;
                    case 7:
                        HP += 22; break;
                    case 8:
                        HP += 25; break;
                    case 9:
                        HP += 29; break;
                    case 10:
                        HP += 32; break;
                    case 11:
                        HP += 35; break;
                    case 12:
                        HP += 38; break;
                    case 13:
                        HP += 42; break;
                    case 14:
                        HP += 45; break;
                    case 15:
                        HP += 48; break;
                    case 16:
                        HP += 51; break;
                    case 17:
                        HP += 55; break;
                    case 18:
                        HP += 58; break;
                    case 19:
                        HP += 61; break;
                    case 20:
                        HP += 64; break;
                    case 21:
                        HP += 68; break;
                    case 22:
                        HP += 71; break;
                    case 23:
                        HP += 74; break;
                    case 24:
                        HP += 77; break;
                    case 25:
                        HP += 81; break;
                    case 26:
                        HP += 84; break;
                    case 27:
                        HP += 87; break;
                    case 28:
                        HP += 90; break;
                    case 29:
                        HP += 93; break;
                    case 30:
                        HP += 97; break;
                    case 31:
                        HP += 100; break;
                    case 32:
                        HP += 103; break;
                    case 33:
                        HP += 106; break;
                    case 34:
                        HP += 110; break;
                    case 35:
                        HP += 113; break;
                    case 36:
                        HP += 116; break;
                    case 37:
                        HP += 119; break;
                    case 38:
                        HP += 123; break;
                    case 39:
                        HP += 126; break;
                    case 40:
                        HP += 129; break;
                    case 41:
                        HP += 132; break;
                    case 42:
                        HP += 136; break;
                    case 43:
                        HP += 139; break;
                    case 44:
                        HP += 142; break;
                    case 45:
                        HP += 145; break;
                    case 46:
                        HP += 149; break;
                    case 47:
                        HP += 152; break;
                    case 48:
                        HP += 155; break;
                    case 49:
                        HP += 158; break;
                    case 50:
                        HP += 162; break;
                    case 51:
                        HP += 165; break;
                    case 52:
                        HP += 168; break;
                    case 53:
                        HP += 171; break;
                    case 54:
                        HP += 174; break;
                    case 55:
                        HP += 178; break;
                    case 56:
                        HP += 181; break;
                    case 57:
                        HP += 184; break;
                    case 58:
                        HP += 187; break;
                    case 59:
                        HP += 191; break;
                    case 60:
                        HP += 194; break;
                    case 61:
                        HP += 197; break;
                    case 62:
                        HP += 200; break;
                    case 63:
                        HP += 204; break;
                    case 64:
                        HP += 207; break;
                    case 65:
                        HP += 210; break;
                    case 66:
                        HP += 213; break;
                    case 67:
                        HP += 217; break;
                    case 68:
                        HP += 220; break;
                    case 69:
                        HP += 223; break;
                    case 70:
                        HP += 226; break;
                    case 71:
                        HP += 230; break;
                    case 72:
                        HP += 233; break;
                    case 73:
                        HP += 236; break;
                    case 74:
                        HP += 239; break;
                    case 75:
                        HP += 243; break;
                    case 76:
                        HP += 246; break;
                    case 77:
                        HP += 249; break;
                    case 78:
                        HP += 252; break;
                    case 79:
                        HP += 255; break;
                    case 80:
                        HP += 259; break;
                    case 81:
                        HP += 262; break;
                    case 82:
                        HP += 265; break;
                    case 83://
                        HP += 268; break;//
                    case 84:
                        HP += 272; break;
                    case 85:
                        HP += 275; break;
                    case 86:
                        HP += 278; break;
                    case 87:
                        HP += 281; break;
                    case 88:
                        HP += 285; break;
                    case 89:
                        HP += 288; break;
                    case 90:
                        HP += 291; break;
                    case 91:
                        HP += 294; break;
                    case 92:
                        HP += 298; break;
                    case 93:
                        HP += 301; break;
                    case 94:
                        HP += 304; break;
                    case 95:
                        HP += 307; break;
                    case 96:
                        HP += 311; break;
                    case 97:
                        HP += 314; break;
                    case 98:
                        HP += 317; break;
                    case 99:
                        HP += 320; break;
                    case 100:
                        HP += 324; break;
                    case 101:
                        HP += 327; break;
                    case 102:
                        HP += 330; break;
                    case 103:
                        HP += 333; break;
                    case 104:
                        HP += 336; break;
                    case 105:
                        HP += 340; break;
                    case 106:
                        HP += 343; break;
                    case 107:
                        HP += 346; break;
                    case 108:
                        HP += 349; break;
                    case 109:
                        HP += 353; break;
                    case 110:
                        HP += 356; break;
                    case 111:
                        HP += 359; break;
                    case 112:
                        HP += 362; break;
                    case 113:
                        HP += 366; break;
                    case 114:
                        HP += 369; break;
                    case 115:
                        HP += 372; break;
                    case 116:
                        HP += 375; break;
                    case 117:
                        HP += 379; break;
                    case 118:
                        HP += 382; break;
                    case 119:
                        HP += 385; break;
                    case 120:
                        HP += 388; break;
                    case 121:
                        HP += 392; break;
                    case 122:
                        HP += 395; break;
                    case 123:
                        HP += 398; break;
                    case 124:
                        HP += 401; break;
                    case 125:
                        HP += 405; break;
                    case 126:
                        HP += 408; break;
                    case 127:
                        HP += 411; break;
                    case 128:
                        HP += 414; break;
                    case 129:
                        HP += 417; break;
                    case 130:
                        HP += 421; break;
                    case 131:
                        HP += 424; break;
                    case 132:
                        HP += 427; break;
                    case 133:
                        HP += 430; break;
                    case 134:
                        HP += 434; break;
                    case 135:
                        HP += 437; break;
                    case 136:
                        HP += 440; break;
                    case 137:
                        HP += 443; break;
                    case 138:
                        HP += 447; break;
                    case 139:
                        HP += 450; break;
                    case 140:
                        HP += 453; break;
                    case 141:
                        HP += 456; break;
                    case 142:
                        HP += 460; break;
                    case 143:
                        HP += 463; break;
                    case 144:
                        HP += 466; break;
                    case 145:
                        HP += 469; break;
                    case 146:
                        HP += 473; break;
                    case 147:
                        HP += 476; break;
                    case 148:
                        HP += 479; break;
                    case 149:
                        HP += 482; break;
                    case 150:
                        HP += 486; break;
                    case 151:
                        HP += 489; break;
                    case 152:
                        HP += 492; break;
                    case 153:
                        HP += 495; break;
                    case 154:
                        HP += 498; break;
                    case 155:
                        HP += 502; break;
                    case 156:
                        HP += 505; break;
                    case 157:
                        HP += 508; break;
                    case 158:
                        HP += 511; break;
                    case 159:
                        HP += 515; break;
                    case 160:
                        HP += 518; break;
                    case 161:
                        HP += 521; break;
                    case 162:
                        HP += 524; break;
                    case 163:
                        HP += 528; break;
                    case 164:
                        HP += 531; break;
                    case 165:
                        HP += 534; break;
                    case 166:
                        HP += 537; break;
                    case 167:
                        HP += 541; break;
                    case 168:
                        HP += 544; break;
                    case 169:
                        HP += 547; break;
                    case 170:
                        HP += 550; break;
                    case 171:
                        HP += 554; break;
                    case 172:
                        HP += 557; break;
                    case 173:
                        HP += 560; break;
                    case 174:
                        HP += 563; break;
                    case 175:
                        HP += 567; break;
                    case 176:
                        HP += 570; break;
                    case 177:
                        HP += 573; break;
                    case 178:
                        HP += 576; break;
                    case 179:
                        HP += 579; break;
                    case 180:
                        HP += 583; break;
                    case 181:
                        HP += 586; break;
                    case 182:
                        HP += 589; break;
                    case 183:
                        HP += 592; break;
                    case 184:
                        HP += 596; break;
                    case 185:
                        HP += 599; break;
                    case 186:
                        HP += 602; break;
                    case 187:
                        HP += 605; break;
                    case 188:
                        HP += 609; break;
                    case 189:
                        HP += 612; break;
                    case 190:
                        HP += 615; break;
                    case 191:
                        HP += 618; break;
                    case 192:
                        HP += 622; break;
                    case 193:
                        HP += 625; break;
                    case 194:
                        HP += 628; break;
                    case 195:
                        HP += 631; break;
                    case 196:
                        HP += 635; break;
                    case 197:
                        HP += 638; break;
                    case 198:
                        HP += 641; break;
                    case 199:
                        HP += 644; break;
                    case 200:
                        HP += 648; break;
                    #endregion
                }
            }
            #endregion
            #region TigerTrojan
            else if ((int)Client.Job == 13)
            {
                switch (Client.Strength)
                {
                    #region Diff Str Count
                    case 1:
                        HP += 3; break;
                    case 2:
                        HP += 6; break;
                    case 3:
                        HP += 9; break;
                    case 4:
                        HP += 13; break;
                    case 5:
                        HP += 16; break;
                    case 6:
                        HP += 19; break;
                    case 7:
                        HP += 23; break;
                    case 8:
                        HP += 26; break;
                    case 9:
                        HP += 29; break;
                    case 10:
                        HP += 33; break;
                    case 11:
                        HP += 36; break;
                    case 12:
                        HP += 39; break;
                    case 13:
                        HP += 42; break;
                    case 14:
                        HP += 46; break;
                    case 15:
                        HP += 49; break;
                    case 16:
                        HP += 52; break;
                    case 17:
                        HP += 56; break;
                    case 18:
                        HP += 59; break;
                    case 19:
                        HP += 62; break;
                    case 20:
                        HP += 66; break;
                    case 21:
                        HP += 69; break;
                    case 22:
                        HP += 72; break;
                    case 23:
                        HP += 75; break;
                    case 24:
                        HP += 79; break;
                    case 25:
                        HP += 82; break;
                    case 26:
                        HP += 85; break;
                    case 27:
                        HP += 89; break;
                    case 28:
                        HP += 92; break;
                    case 29:
                        HP += 95; break;
                    case 30:
                        HP += 99; break;
                    case 31:
                        HP += 102; break;
                    case 32:
                        HP += 105; break;
                    case 33:
                        HP += 108; break;
                    case 34:
                        HP += 112; break;
                    case 35:
                        HP += 115; break;
                    case 36:
                        HP += 118; break;
                    case 37:
                        HP += 122; break;
                    case 38:
                        HP += 125; break;
                    case 39:
                        HP += 128; break;
                    case 40:
                        HP += 132; break;
                    case 41:
                        HP += 135; break;
                    case 42:
                        HP += 138; break;
                    case 43:
                        HP += 141; break;
                    case 44:
                        HP += 145; break;
                    case 45:
                        HP += 148; break;
                    case 46:
                        HP += 151; break;
                    case 47:
                        HP += 155; break;
                    case 48:
                        HP += 158; break;
                    case 49:
                        HP += 161; break;
                    case 50:
                        HP += 165; break;
                    case 51:
                        HP += 168; break;
                    case 52:
                        HP += 171; break;
                    case 53:
                        HP += 174; break;
                    case 54:
                        HP += 178; break;
                    case 55:
                        HP += 181; break;
                    case 56:
                        HP += 184; break;
                    case 57:
                        HP += 188; break;
                    case 58:
                        HP += 191; break;
                    case 59:
                        HP += 194; break;
                    case 60:
                        HP += 198; break;
                    case 61:
                        HP += 201; break;
                    case 62:
                        HP += 204; break;
                    case 63:
                        HP += 207; break;
                    case 64:
                        HP += 211; break;
                    case 65:
                        HP += 214; break;
                    case 66:
                        HP += 217; break;
                    case 67:
                        HP += 221; break;
                    case 68:
                        HP += 224; break;
                    case 69:
                        HP += 227; break;
                    case 70:
                        HP += 231; break;
                    case 71:
                        HP += 234; break;
                    case 72:
                        HP += 237; break;
                    case 73:
                        HP += 240; break;
                    case 74:
                        HP += 244; break;
                    case 75:
                        HP += 247; break;
                    case 76:
                        HP += 250; break;
                    case 77:
                        HP += 254; break;
                    case 78:
                        HP += 257; break;
                    case 79:
                        HP += 260; break;
                    case 80:
                        HP += 264; break;
                    case 81:
                        HP += 267; break;
                    case 82:
                        HP += 270; break;
                    case 83://
                        HP += 273; break;//
                    case 84:
                        HP += 277; break;
                    case 85:
                        HP += 280; break;
                    case 86:
                        HP += 283; break;
                    case 87:
                        HP += 287; break;
                    case 88:
                        HP += 290; break;
                    case 89:
                        HP += 293; break;
                    case 90:
                        HP += 297; break;
                    case 91:
                        HP += 300; break;
                    case 92:
                        HP += 303; break;
                    case 93:
                        HP += 306; break;
                    case 94:
                        HP += 310; break;
                    case 95:
                        HP += 313; break;
                    case 96:
                        HP += 316; break;
                    case 97:
                        HP += 320; break;
                    case 98:
                        HP += 323; break;
                    case 99:
                        HP += 326; break;
                    case 100:
                        HP += 330; break;
                    case 101:
                        HP += 333; break;
                    case 102:
                        HP += 336; break;
                    case 103:
                        HP += 339; break;
                    case 104:
                        HP += 343; break;
                    case 105:
                        HP += 346; break;
                    case 106:
                        HP += 349; break;
                    case 107:
                        HP += 353; break;
                    case 108:
                        HP += 356; break;
                    case 109:
                        HP += 359; break;
                    case 110:
                        HP += 363; break;
                    case 111:
                        HP += 366; break;
                    case 112:
                        HP += 369; break;
                    case 113:
                        HP += 372; break;
                    case 114:
                        HP += 376; break;
                    case 115:
                        HP += 379; break;
                    case 116:
                        HP += 382; break;
                    case 117:
                        HP += 386; break;
                    case 118:
                        HP += 389; break;
                    case 119:
                        HP += 392; break;
                    case 120:
                        HP += 396; break;
                    case 121:
                        HP += 399; break;
                    case 122:
                        HP += 402; break;
                    case 123:
                        HP += 405; break;
                    case 124:
                        HP += 409; break;
                    case 125:
                        HP += 412; break;
                    case 126:
                        HP += 415; break;
                    case 127:
                        HP += 419; break;
                    case 128:
                        HP += 422; break;
                    case 129:
                        HP += 425; break;
                    case 130:
                        HP += 429; break;
                    case 131:
                        HP += 432; break;
                    case 132:
                        HP += 435; break;
                    case 133:
                        HP += 438; break;
                    case 134:
                        HP += 442; break;
                    case 135:
                        HP += 445; break;
                    case 136:
                        HP += 448; break;
                    case 137:
                        HP += 452; break;
                    case 138:
                        HP += 455; break;
                    case 139:
                        HP += 458; break;
                    case 140:
                        HP += 462; break;
                    case 141:
                        HP += 465; break;
                    case 142:
                        HP += 468; break;
                    case 143:
                        HP += 471; break;
                    case 144:
                        HP += 475; break;
                    case 145:
                        HP += 478; break;
                    case 146:
                        HP += 481; break;
                    case 147:
                        HP += 485; break;
                    case 148:
                        HP += 488; break;
                    case 149:
                        HP += 491; break;
                    case 150:
                        HP += 495; break;
                    case 151:
                        HP += 498; break;
                    case 152:
                        HP += 501; break;
                    case 153:
                        HP += 504; break;
                    case 154:
                        HP += 508; break;
                    case 155:
                        HP += 511; break;
                    case 156:
                        HP += 514; break;
                    case 157:
                        HP += 518; break;
                    case 158:
                        HP += 521; break;
                    case 159:
                        HP += 524; break;
                    case 160:
                        HP += 528; break;
                    case 161:
                        HP += 531; break;
                    case 162:
                        HP += 534; break;
                    case 163:
                        HP += 537; break;
                    case 164:
                        HP += 541; break;
                    case 165:
                        HP += 544; break;
                    case 166:
                        HP += 547; break;
                    case 167:
                        HP += 551; break;
                    case 168:
                        HP += 554; break;
                    case 169:
                        HP += 557; break;
                    case 170:
                        HP += 561; break;
                    case 171:
                        HP += 564; break;
                    case 172:
                        HP += 567; break;
                    case 173:
                        HP += 570; break;
                    case 174:
                        HP += 574; break;
                    case 175:
                        HP += 577; break;
                    case 176:
                        HP += 580; break;
                    case 177:
                        HP += 584; break;
                    case 178:
                        HP += 587; break;
                    case 179:
                        HP += 590; break;
                    case 180:
                        HP += 594; break;
                    case 181:
                        HP += 597; break;
                    case 182:
                        HP += 600; break;
                    case 183:
                        HP += 603; break;
                    case 184:
                        HP += 607; break;
                    case 185:
                        HP += 610; break;
                    case 186:
                        HP += 613; break;
                    case 187:
                        HP += 617; break;
                    case 188:
                        HP += 620; break;
                    case 189:
                        HP += 623; break;
                    case 190:
                        HP += 627; break;
                    case 191:
                        HP += 630; break;
                    case 192:
                        HP += 633; break;
                    case 193:
                        HP += 636; break;
                    case 194:
                        HP += 640; break;
                    case 195:
                        HP += 643; break;
                    case 196:
                        HP += 646; break;
                    case 197:
                        HP += 650; break;
                    case 198:
                        HP += 653; break;
                    case 199:
                        HP += 656; break;
                    case 200:
                        HP += 660; break;
                    #endregion
                }
            }
            #endregion
            #region DragonTrojan
            else if ((int)Client.Job == 14)
            {
                switch (Client.Strength)
                {
                    #region Diff Str Count
                    case 1:
                        HP += 3; break;
                    case 2:
                        HP += 6; break;
                    case 3:
                        HP += 10; break;
                    case 4:
                        HP += 13; break;
                    case 5:
                        HP += 16; break;
                    case 6:
                        HP += 20; break;
                    case 7:
                        HP += 23; break;
                    case 8:
                        HP += 26; break;
                    case 9:
                        HP += 30; break;
                    case 10:
                        HP += 33; break;
                    case 11:
                        HP += 36; break;
                    case 12:
                        HP += 40; break;
                    case 13:
                        HP += 43; break;
                    case 14:
                        HP += 47; break;
                    case 15:
                        HP += 50; break;
                    case 16:
                        HP += 53; break;
                    case 17:
                        HP += 57; break;
                    case 18:
                        HP += 60; break;
                    case 19:
                        HP += 63; break;
                    case 20:
                        HP += 67; break;
                    case 21:
                        HP += 70; break;
                    case 22:
                        HP += 73; break;
                    case 23:
                        HP += 77; break;
                    case 24:
                        HP += 80; break;
                    case 25:
                        HP += 84; break;
                    case 26:
                        HP += 87; break;
                    case 27:
                        HP += 90; break;
                    case 28:
                        HP += 94; break;
                    case 29:
                        HP += 97; break;
                    case 30:
                        HP += 100; break;
                    case 31:
                        HP += 104; break;
                    case 32:
                        HP += 107; break;
                    case 33:
                        HP += 110; break;
                    case 34:
                        HP += 114; break;
                    case 35:
                        HP += 117; break;
                    case 36:
                        HP += 120; break;
                    case 37:
                        HP += 124; break;
                    case 38:
                        HP += 127; break;
                    case 39:
                        HP += 131; break;
                    case 40:
                        HP += 134; break;
                    case 41:
                        HP += 137; break;
                    case 42:
                        HP += 141; break;
                    case 43:
                        HP += 144; break;
                    case 44:
                        HP += 147; break;
                    case 45:
                        HP += 151; break;
                    case 46:
                        HP += 154; break;
                    case 47:
                        HP += 157; break;
                    case 48:
                        HP += 161; break;
                    case 49:
                        HP += 164; break;
                    case 50:
                        HP += 168; break;
                    case 51:
                        HP += 171; break;
                    case 52:
                        HP += 174; break;
                    case 53:
                        HP += 178; break;
                    case 54:
                        HP += 181; break;
                    case 55:
                        HP += 184; break;
                    case 56:
                        HP += 188; break;
                    case 57:
                        HP += 191; break;
                    case 58:
                        HP += 194; break;
                    case 59:
                        HP += 198; break;
                    case 60:
                        HP += 201; break;
                    case 61:
                        HP += 204; break;
                    case 62:
                        HP += 208; break;
                    case 63:
                        HP += 211; break;
                    case 64:
                        HP += 215; break;
                    case 65:
                        HP += 218; break;
                    case 66:
                        HP += 221; break;
                    case 67:
                        HP += 225; break;
                    case 68:
                        HP += 228; break;
                    case 69:
                        HP += 231; break;
                    case 70:
                        HP += 235; break;
                    case 71:
                        HP += 238; break;
                    case 72:
                        HP += 241; break;
                    case 73:
                        HP += 245; break;
                    case 74:
                        HP += 248; break;
                    case 75:
                        HP += 252; break;
                    case 76:
                        HP += 255; break;
                    case 77:
                        HP += 258; break;
                    case 78:
                        HP += 262; break;
                    case 79:
                        HP += 265; break;
                    case 80:
                        HP += 268; break;
                    case 81:
                        HP += 272; break;
                    case 82:
                        HP += 275; break;
                    case 83://
                        HP += 278; break;//
                    case 84:
                        HP += 282; break;
                    case 85:
                        HP += 285; break;
                    case 86:
                        HP += 288; break;
                    case 87:
                        HP += 292; break;
                    case 88:
                        HP += 295; break;
                    case 89:
                        HP += 299; break;
                    case 90:
                        HP += 302; break;
                    case 91:
                        HP += 305; break;
                    case 92:
                        HP += 309; break;
                    case 93:
                        HP += 312; break;
                    case 94:
                        HP += 315; break;
                    case 95:
                        HP += 319; break;
                    case 96:
                        HP += 322; break;
                    case 97:
                        HP += 325; break;
                    case 98:
                        HP += 329; break;
                    case 99:
                        HP += 332; break;
                    case 100:
                        HP += 336; break;
                    case 101:
                        HP += 339; break;
                    case 102:
                        HP += 342; break;
                    case 103:
                        HP += 346; break;
                    case 104:
                        HP += 349; break;
                    case 105:
                        HP += 352; break;
                    case 106:
                        HP += 356; break;
                    case 107:
                        HP += 359; break;
                    case 108:
                        HP += 362; break;
                    case 109:
                        HP += 366; break;
                    case 110:
                        HP += 369; break;
                    case 111:
                        HP += 372; break;
                    case 112:
                        HP += 376; break;
                    case 113:
                        HP += 379; break;
                    case 114:
                        HP += 383; break;
                    case 115:
                        HP += 386; break;
                    case 116:
                        HP += 389; break;
                    case 117:
                        HP += 393; break;
                    case 118:
                        HP += 396; break;
                    case 119:
                        HP += 399; break;
                    case 120:
                        HP += 403; break;
                    case 121:
                        HP += 406; break;
                    case 122:
                        HP += 409; break;
                    case 123:
                        HP += 413; break;
                    case 124:
                        HP += 416; break;
                    case 125:
                        HP += 420; break;
                    case 126:
                        HP += 423; break;
                    case 127:
                        HP += 426; break;
                    case 128:
                        HP += 430; break;
                    case 129:
                        HP += 433; break;
                    case 130:
                        HP += 436; break;
                    case 131:
                        HP += 440; break;
                    case 132:
                        HP += 443; break;
                    case 133:
                        HP += 446; break;
                    case 134:
                        HP += 450; break;
                    case 135:
                        HP += 453; break;
                    case 136:
                        HP += 456; break;
                    case 137:
                        HP += 460; break;
                    case 138:
                        HP += 463; break;
                    case 139:
                        HP += 467; break;
                    case 140:
                        HP += 470; break;
                    case 141:
                        HP += 473; break;
                    case 142:
                        HP += 477; break;
                    case 143:
                        HP += 480; break;
                    case 144:
                        HP += 483; break;
                    case 145:
                        HP += 487; break;
                    case 146:
                        HP += 490; break;
                    case 147:
                        HP += 493; break;
                    case 148:
                        HP += 497; break;
                    case 149:
                        HP += 500; break;
                    case 150:
                        HP += 504; break;
                    case 151:
                        HP += 507; break;
                    case 152:
                        HP += 510; break;
                    case 153:
                        HP += 514; break;
                    case 154:
                        HP += 517; break;
                    case 155:
                        HP += 520; break;
                    case 156:
                        HP += 524; break;
                    case 157:
                        HP += 527; break;
                    case 158:
                        HP += 530; break;
                    case 159:
                        HP += 534; break;
                    case 160:
                        HP += 537; break;
                    case 161:
                        HP += 540; break;
                    case 162:
                        HP += 544; break;
                    case 163:
                        HP += 547; break;
                    case 164:
                        HP += 551; break;
                    case 165:
                        HP += 554; break;
                    case 166:
                        HP += 557; break;
                    case 167:
                        HP += 561; break;
                    case 168:
                        HP += 564; break;
                    case 169:
                        HP += 567; break;
                    case 170:
                        HP += 571; break;
                    case 171:
                        HP += 574; break;
                    case 172:
                        HP += 577; break;
                    case 173:
                        HP += 581; break;
                    case 174:
                        HP += 584; break;
                    case 175:
                        HP += 588; break;
                    case 176:
                        HP += 591; break;
                    case 177:
                        HP += 594; break;
                    case 178:
                        HP += 598; break;
                    case 179:
                        HP += 601; break;
                    case 180:
                        HP += 604; break;
                    case 181:
                        HP += 608; break;
                    case 182:
                        HP += 611; break;
                    case 183:
                        HP += 614; break;
                    case 184:
                        HP += 618; break;
                    case 185:
                        HP += 621; break;
                    case 186:
                        HP += 624; break;
                    case 187:
                        HP += 628; break;
                    case 188:
                        HP += 631; break;
                    case 189:
                        HP += 635; break;
                    case 190:
                        HP += 638; break;
                    case 191:
                        HP += 641; break;
                    case 192:
                        HP += 645; break;
                    case 193:
                        HP += 648; break;
                    case 194:
                        HP += 651; break;
                    case 195:
                        HP += 655; break;
                    case 196:
                        HP += 658; break;
                    case 197:
                        HP += 661; break;
                    case 198:
                        HP += 665; break;
                    case 199:
                        HP += 668; break;
                    case 200:
                        HP += 672; break;
                    #endregion
                }
            }
            #endregion
            #region TrojanMaster
            else if ((int)Client.Job == 15)
            {
                switch (Client.Strength)// not done
                {
                    #region Diff Str Count
                    case 1:
                        HP += 3; break;
                    case 2:
                        HP += 6; break;
                    case 3:
                        HP += 10; break;
                    case 4:
                        HP += 13; break;
                    case 5:
                        HP += 17; break;
                    case 6:
                        HP += 20; break;
                    case 7:
                        HP += 24; break;
                    case 8:
                        HP += 27; break;
                    case 9:
                        HP += 31; break;
                    case 10:
                        HP += 34; break;
                    case 11:
                        HP += 37; break;
                    case 12:
                        HP += 41; break;
                    case 13:
                        HP += 44; break;
                    case 14:
                        HP += 48; break;
                    case 15:
                        HP += 51; break;
                    case 16:
                        HP += 55; break;
                    case 17:
                        HP += 58; break;
                    case 18:
                        HP += 62; break;
                    case 19:
                        HP += 65; break;
                    case 20:
                        HP += 69; break;
                    case 21:
                        HP += 72; break;
                    case 22:
                        HP += 75; break;
                    case 23:
                        HP += 79; break;
                    case 24:
                        HP += 82; break;
                    case 25:
                        HP += 86; break;
                    case 26:
                        HP += 89; break;
                    case 27:
                        HP += 93; break;
                    case 28:
                        HP += 96; break;
                    case 29:
                        HP += 100; break;
                    case 30:
                        HP += 103; break;
                    case 31:
                        HP += 106; break;
                    case 32:
                        HP += 110; break;
                    case 33:
                        HP += 113; break;
                    case 34:
                        HP += 117; break;
                    case 35:
                        HP += 120; break;
                    case 36:
                        HP += 124; break;
                    case 37:
                        HP += 127; break;
                    case 38:
                        HP += 131; break;
                    case 39:
                        HP += 134; break;
                    case 40:
                        HP += 138; break;
                    case 41:
                        HP += 141; break;
                    case 42:
                        HP += 144; break;
                    case 43:
                        HP += 148; break;
                    case 44:
                        HP += 151; break;
                    case 45:
                        HP += 155; break;
                    case 46:
                        HP += 158; break;
                    case 47:
                        HP += 162; break;
                    case 48:
                        HP += 165; break;
                    case 49:
                        HP += 169; break;
                    case 50:
                        HP += 172; break;
                    case 51:
                        HP += 175; break;
                    case 52:
                        HP += 179; break;
                    case 53:
                        HP += 182; break;
                    case 54:
                        HP += 186; break;
                    case 55:
                        HP += 189; break;
                    case 56:
                        HP += 193; break;
                    case 57:
                        HP += 196; break;
                    case 58:
                        HP += 200; break;
                    case 59:
                        HP += 203; break;
                    case 60:
                        HP += 207; break;
                    case 61:
                        HP += 210; break;
                    case 62:
                        HP += 213; break;
                    case 63:
                        HP += 217; break;
                    case 64:
                        HP += 220; break;
                    case 65:
                        HP += 224; break;
                    case 66:
                        HP += 227; break;
                    case 67:
                        HP += 231; break;
                    case 68:
                        HP += 234; break;
                    case 69:
                        HP += 238; break;
                    case 70:
                        HP += 241; break;
                    case 71:
                        HP += 244; break;
                    case 72:
                        HP += 248; break;
                    case 73:
                        HP += 251; break;
                    case 74:
                        HP += 255; break;
                    case 75:
                        HP += 258; break;
                    case 76:
                        HP += 262; break;
                    case 77:
                        HP += 265; break;
                    case 78:
                        HP += 269; break;
                    case 79:
                        HP += 272; break;
                    case 80:
                        HP += 276; break;
                    case 81:
                        HP += 279; break;
                    case 82:
                        HP += 282; break;
                    case 83://
                        HP += 286; break;//
                    case 84:
                        HP += 289; break;
                    case 85:
                        HP += 293; break;
                    case 86:
                        HP += 296; break;
                    case 87:
                        HP += 300; break;
                    case 88:
                        HP += 303; break;
                    case 89:
                        HP += 307; break;
                    case 90:
                        HP += 310; break;
                    case 91:
                        HP += 313; break;
                    case 92:
                        HP += 317; break;
                    case 93:
                        HP += 320; break;
                    case 94:
                        HP += 324; break;
                    case 95:
                        HP += 327; break;
                    case 96:
                        HP += 331; break;
                    case 97:
                        HP += 334; break;
                    case 98:
                        HP += 338; break;
                    case 99:
                        HP += 341; break;
                    case 100:
                        HP += 345; break;
                    case 101:
                        HP += 348; break;
                    case 102:
                        HP += 351; break;
                    case 103:
                        HP += 355; break;
                    case 104:
                        HP += 358; break;
                    case 105:
                        HP += 362; break;
                    case 106:
                        HP += 365; break;
                    case 107:
                        HP += 369; break;
                    case 108:
                        HP += 372; break;
                    case 109:
                        HP += 376; break;
                    case 110:
                        HP += 379; break;
                    case 111:
                        HP += 382; break;
                    case 112:
                        HP += 386; break;
                    case 113:
                        HP += 389; break;
                    case 114:
                        HP += 393; break;
                    case 115:
                        HP += 396; break;
                    case 116:
                        HP += 400; break;
                    case 117:
                        HP += 403; break;
                    case 118:
                        HP += 407; break;
                    case 119:
                        HP += 410; break;
                    case 120:
                        HP += 414; break;
                    case 121:
                        HP += 417; break;
                    case 122:
                        HP += 420; break;
                    case 123:
                        HP += 424; break;
                    case 124:
                        HP += 427; break;
                    case 125:
                        HP += 431; break;
                    case 126:
                        HP += 434; break;
                    case 127:
                        HP += 438; break;
                    case 128:
                        HP += 441; break;
                    case 129:
                        HP += 445; break;
                    case 130:
                        HP += 448; break;
                    case 131:
                        HP += 451; break;
                    case 132:
                        HP += 455; break;
                    case 133:
                        HP += 458; break;
                    case 134:
                        HP += 462; break;
                    case 135:
                        HP += 465; break;
                    case 136:
                        HP += 469; break;
                    case 137:
                        HP += 472; break;
                    case 138:
                        HP += 476; break;
                    case 139:
                        HP += 479; break;
                    case 140:
                        HP += 483; break;
                    case 141:
                        HP += 486; break;
                    case 142:
                        HP += 489; break;
                    case 143:
                        HP += 493; break;
                    case 144:
                        HP += 496; break;
                    case 145:
                        HP += 500; break;
                    case 146:
                        HP += 503; break;
                    case 147:
                        HP += 507; break;
                    case 148:
                        HP += 510; break;
                    case 149:
                        HP += 514; break;
                    case 150:
                        HP += 517; break;
                    case 151:
                        HP += 520; break;
                    case 152:
                        HP += 524; break;
                    case 153:
                        HP += 527; break;
                    case 154:
                        HP += 531; break;
                    case 155:
                        HP += 534; break;
                    case 156:
                        HP += 538; break;
                    case 157:
                        HP += 541; break;
                    case 158:
                        HP += 545; break;
                    case 159:
                        HP += 548; break;
                    case 160:
                        HP += 552; break;
                    case 161:
                        HP += 555; break;
                    case 162:
                        HP += 558; break;
                    case 163:
                        HP += 562; break;
                    case 164:
                        HP += 565; break;
                    case 165:
                        HP += 569; break;
                    case 166:
                        HP += 572; break;
                    case 167:
                        HP += 576; break;
                    case 168:
                        HP += 579; break;
                    case 169:
                        HP += 583; break;
                    case 170:
                        HP += 586; break;
                    case 171:
                        HP += 589; break;
                    case 172:
                        HP += 593; break;
                    case 173:
                        HP += 596; break;
                    case 174:
                        HP += 600; break;
                    case 175:
                        HP += 603; break;
                    case 176:
                        HP += 607; break;
                    case 177:
                        HP += 610; break;
                    case 178:
                        HP += 614; break;
                    case 179:
                        HP += 617; break;
                    case 180:
                        HP += 621; break;
                    case 181:
                        HP += 624; break;
                    case 182:
                        HP += 627; break;
                    case 183:
                        HP += 631; break;
                    case 184:
                        HP += 634; break;
                    case 185:
                        HP += 638; break;
                    case 186:
                        HP += 641; break;
                    case 187:
                        HP += 645; break;
                    case 188:
                        HP += 648; break;
                    case 189:
                        HP += 652; break;
                    case 190:
                        HP += 655; break;
                    case 191:
                        HP += 658; break;
                    case 192:
                        HP += 662; break;
                    case 193:
                        HP += 665; break;
                    case 194:
                        HP += 669; break;
                    case 195:
                        HP += 672; break;
                    case 196:
                        HP += 676; break;
                    case 197:
                        HP += 679; break;
                    case 198:
                        HP += 683; break;
                    case 199:
                        HP += 686; break;
                    case 200:
                        HP += 690; break;
                    #endregion
                }
            }
            #endregion
            else
            {
                HP += (Client.Strength * 3.2);
            }
            HP += (Client.Spirit * 3.2); //+ ( Client.Dexterity * 3.2) + ( Client.Strengthength * 3.2);

            Client.Entity.MaxHitpoints = (ushort)HP;

            //Mana
            int Mana = 0;
            int multiplier = 5;
            if ((int)Client.Job == 132 || (int)Client.Job == 142)
            {
                multiplier = 15;
            }
            else if ((int)Client.Job == 133 || (int)Client.Job == 143)
            {
                multiplier = 20;
            }
            else if ((int)Client.Job == 134 || (int)Client.Job == 144)
            {
                multiplier = 25;
            }
            else if ((int)Client.Job == 135 || (int)Client.Job == 145)
            {
                multiplier = 30;

            }
            Mana = Client.Spirit * multiplier;
            Client.MaxMana += Client.BaseMP;
            Sync.MaxHP(Client); Sync.HP(Client);
        }

    }
}