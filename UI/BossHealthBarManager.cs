using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReLogic.Graphics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Events;

namespace CalamityModClassicPreTrailer.UI
{
    /*
    Heyo! Here's some things you might need to know about this class and where to change things:

    In the "Load" method is where the "OneToMany" dictionary is updated. 
    If an NPC is made up of multiple segments (such as EoW, etc.) that all have separate health but would be considered a single boss, you can update the dictionary. 
    The boss health bar will automatically keep track of all segments of that type and add up the health.

    The "SetupExclusionList" method allows you to check for if certain mods are loaded and setup the exclusion list based on what mods are loaded.
    For now I've had to load calamity separately, you might want to change that by just passing in Calamity itself etc.

    If you need to add a special kind of health bar, like the EoW one which counts the number of segments left, let me know.
    I may need to dive into the spaghetti to add it.
    
    There's a few readonly fields at the top of both classes that you can edit should you deem it fit to. Change the animation lengths, etc.
    The bar uses those and shouldn't have any problems dealing with the new values 
    (although the flickering in the start animation will always happen in the first 20 frames or so)

    As for toggling, in the Mod File, you can simply add a boolean check and not draw or update the health bar manager.
    There is a "SHOULD_DRAW_SMALLTEXT_HEALTH" field below here which is public, if that's false then the smalltext won't draw.

    That should be it -- ask if you have any questions!
    */

    internal static class BossHealthBarManager
    {
        private static readonly int MAX_BARS = 4; //4

        public static bool SHOULD_DRAW_SMALLTEXT_HEALTH = true;

        private static List<BossHPUI> Bars;

        private static DynamicSpriteFont HPBarFont;
        private static Texture2D BossMainHPBar;
        private static Texture2D BossComboHPBar;
        private static Texture2D BossSeperatorBar;

        private static Dictionary<int, int[]> OneToMany;
        private static List<int> ExclusionList;
        private static List<int> MinibossHPBarList;

        public static void Load(Mod mod)
        {
            Bars = new List<BossHPUI>();

            if (!Main.dedServ)
            {
                BossMainHPBar = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/UI/BossHPMainBar", AssetRequestMode.ImmediateLoad).Value;
                BossComboHPBar = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/UI/BossHPComboBar", AssetRequestMode.ImmediateLoad).Value;
                BossSeperatorBar = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/UI/BossHPSeperatorBar", AssetRequestMode.ImmediateLoad).Value;

                PlatformID id = Environment.OSVersion.Platform;
                if (id == PlatformID.Win32NT)
                {
                    HPBarFont = ModContent.Request<DynamicSpriteFont>("CalamityModClassicPreTrailer/Fonts/HPBarFont", AssetRequestMode.ImmediateLoad).Value;
                }
                else
                {
                    HPBarFont = FontAssets.MouseText.Value;
                }
            }

            OneToMany = new Dictionary<int, int[]>();
            int[] EoW = new int[] { NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail };
            OneToMany[NPCID.EaterofWorldsHead] = EoW;
            OneToMany[NPCID.EaterofWorldsBody] = EoW;
            OneToMany[NPCID.EaterofWorldsTail] = EoW;

            int[] BoC = new int[] { NPCID.BrainofCthulhu, NPCID.Creeper };
            OneToMany[NPCID.BrainofCthulhu] = BoC;
            OneToMany[NPCID.Creeper] = BoC;

            int[] Skele = new int[] { NPCID.SkeletronHead, NPCID.SkeletronHand };
            OneToMany[NPCID.SkeletronHead] = Skele;
            OneToMany[NPCID.SkeletronHand] = Skele;

            int[] SkelePrime = new int[] { NPCID.SkeletronPrime, NPCID.PrimeSaw, NPCID.PrimeVice, NPCID.PrimeCannon, NPCID.PrimeLaser };
            OneToMany[NPCID.SkeletronPrime] = SkelePrime;
            OneToMany[NPCID.PrimeSaw] = SkelePrime;
            OneToMany[NPCID.PrimeVice] = SkelePrime;
            OneToMany[NPCID.PrimeCannon] = SkelePrime;
            OneToMany[NPCID.PrimeLaser] = SkelePrime;

            int[] Saucer = new int[] { NPCID.MartianSaucerCore, NPCID.MartianSaucerTurret, NPCID.MartianSaucerCannon };
            OneToMany[NPCID.MartianSaucerCore] = Saucer;
            OneToMany[NPCID.MartianSaucerTurret] = Saucer;
            OneToMany[NPCID.MartianSaucerCannon] = Saucer;

            int[] Ship = new int[] { NPCID.PirateShip, NPCID.PirateShipCannon };
            OneToMany[NPCID.PirateShip] = Ship;
            OneToMany[NPCID.PirateShipCannon] = Ship;

            int[] MoonLord = new int[] { NPCID.MoonLordHead, NPCID.MoonLordHand, NPCID.MoonLordCore };
            OneToMany[NPCID.MoonLordHead] = MoonLord;
            OneToMany[NPCID.MoonLordHand] = MoonLord;
            OneToMany[NPCID.MoonLordCore] = MoonLord;

            int[] StarWorm = new int[] { mod.Find<ModNPC>("AstrumDeusHeadSpectral").Type, mod.Find<ModNPC>("AstrumDeusHead").Type };
            OneToMany[mod.Find<ModNPC>("AstrumDeusHeadSpectral").Type] = StarWorm;
            OneToMany[mod.Find<ModNPC>("AstrumDeusHead").Type] = StarWorm;

            int[] Cal = new int[] { mod.Find<ModNPC>("CalamitasRun3").Type, mod.Find<ModNPC>("CalamitasRun").Type, mod.Find<ModNPC>("CalamitasRun2").Type };
            OneToMany[mod.Find<ModNPC>("CalamitasRun3").Type] = Cal;
            OneToMany[mod.Find<ModNPC>("CalamitasRun").Type] = Cal;
            OneToMany[mod.Find<ModNPC>("CalamitasRun2").Type] = Cal;

            int[] Void = new int[] { mod.Find<ModNPC>("CeaselessVoid").Type, mod.Find<ModNPC>("DarkEnergy").Type, mod.Find<ModNPC>("DarkEnergy2").Type, mod.Find<ModNPC>("DarkEnergy3").Type };
            OneToMany[mod.Find<ModNPC>("CeaselessVoid").Type] = Void;
            OneToMany[mod.Find<ModNPC>("DarkEnergy").Type] = Void;
            OneToMany[mod.Find<ModNPC>("DarkEnergy2").Type] = Void;
            OneToMany[mod.Find<ModNPC>("DarkEnergy3").Type] = Void;

            int[] Perfs = new int[] { mod.Find<ModNPC>("PerforatorHive").Type, mod.Find<ModNPC>("PerforatorHeadSmall").Type, mod.Find<ModNPC>("PerforatorHeadMedium").Type,
                mod.Find<ModNPC>("PerforatorHeadLarge").Type };
            OneToMany[mod.Find<ModNPC>("PerforatorHive").Type] = Perfs;
            OneToMany[mod.Find<ModNPC>("PerforatorHeadSmall").Type] = Perfs;
            OneToMany[mod.Find<ModNPC>("PerforatorHeadMedium").Type] = Perfs;
            OneToMany[mod.Find<ModNPC>("PerforatorHeadLarge").Type] = Perfs;

            int[] Rav = new int[] { mod.Find<ModNPC>("ScavengerBody").Type, mod.Find<ModNPC>("ScavengerClawRight").Type, mod.Find<ModNPC>("ScavengerClawLeft").Type,
                mod.Find<ModNPC>("ScavengerLegRight").Type, mod.Find<ModNPC>("ScavengerLegLeft").Type, mod.Find<ModNPC>("ScavengerHead").Type };
            OneToMany[mod.Find<ModNPC>("ScavengerBody").Type] = Rav;
            OneToMany[mod.Find<ModNPC>("ScavengerClawRight").Type] = Rav;
            OneToMany[mod.Find<ModNPC>("ScavengerClawLeft").Type] = Rav;
            OneToMany[mod.Find<ModNPC>("ScavengerLegRight").Type] = Rav;
            OneToMany[mod.Find<ModNPC>("ScavengerLegLeft").Type] = Rav;
            OneToMany[mod.Find<ModNPC>("ScavengerHead").Type] = Rav;

            int[] Slimes = new int[] { mod.Find<ModNPC>("SlimeGodCore").Type, mod.Find<ModNPC>("SlimeGod").Type, mod.Find<ModNPC>("SlimeGodSplit").Type,
                mod.Find<ModNPC>("SlimeGodRun").Type, mod.Find<ModNPC>("SlimeGodRunSplit").Type };
            OneToMany[mod.Find<ModNPC>("SlimeGodCore").Type] = Slimes;
            OneToMany[mod.Find<ModNPC>("SlimeGod").Type] = Slimes;
            OneToMany[mod.Find<ModNPC>("SlimeGodSplit").Type] = Slimes;
            OneToMany[mod.Find<ModNPC>("SlimeGodRun").Type] = Slimes;
            OneToMany[mod.Find<ModNPC>("SlimeGodRunSplit").Type] = Slimes;

            SetupExclusionList();
            SetupMinibossHPBarList();
        }

        private static void SetupExclusionList()
        {
            ExclusionList = new List<int>();

            ExclusionList.Add(NPCID.MoonLordFreeEye);
            ExclusionList.Add(NPCID.MoonLordHead);
            ExclusionList.Add(NPCID.MoonLordHand);
            ExclusionList.Add(NPCID.MoonLordCore);

            Mod calamity = ModLoader.GetMod("CalamityModClassicPreTrailer");
            if (calamity != null)
            {
                ExclusionList.Add(calamity.Find<ModNPC>("AquaticScourgeBody").Type);
                ExclusionList.Add(calamity.Find<ModNPC>("AquaticScourgeBodyAlt").Type);
                ExclusionList.Add(calamity.Find<ModNPC>("AquaticScourgeTail").Type);
                ExclusionList.Add(calamity.Find<ModNPC>("AstrumDeusBodySpectral").Type);
                ExclusionList.Add(calamity.Find<ModNPC>("AstrumDeusTailSpectral").Type);
                ExclusionList.Add(calamity.Find<ModNPC>("DesertScourgeBody").Type);
                ExclusionList.Add(calamity.Find<ModNPC>("DesertScourgeTail").Type);
                ExclusionList.Add(calamity.Find<ModNPC>("StormWeaverBody").Type);
                ExclusionList.Add(calamity.Find<ModNPC>("StormWeaverTail").Type);
                ExclusionList.Add(calamity.Find<ModNPC>("StormWeaverBodyNaked").Type);
                ExclusionList.Add(calamity.Find<ModNPC>("StormWeaverTailNaked").Type);
                ExclusionList.Add(calamity.Find<ModNPC>("DevourerofGodsBody").Type);
                ExclusionList.Add(calamity.Find<ModNPC>("DevourerofGodsTail").Type);
                ExclusionList.Add(calamity.Find<ModNPC>("DevourerofGodsBodyS").Type);
                ExclusionList.Add(calamity.Find<ModNPC>("DevourerofGodsTailS").Type);
            }
        }

        private static void SetupMinibossHPBarList()
        {
            MinibossHPBarList = new List<int>();

            //DD2 Event
            MinibossHPBarList.Add(NPCID.DD2Betsy);
            MinibossHPBarList.Add(NPCID.DD2OgreT2);
            MinibossHPBarList.Add(NPCID.DD2OgreT3);
            MinibossHPBarList.Add(NPCID.DD2DarkMageT1); //800 HP
            MinibossHPBarList.Add(NPCID.DD2DarkMageT3);

            //Prehardmode
            MinibossHPBarList.Add(NPCID.DungeonGuardian);

            //Hardmode
            MinibossHPBarList.Add(NPCID.GoblinSummoner); //2000 HP
            MinibossHPBarList.Add(NPCID.Paladin);
            MinibossHPBarList.Add(NPCID.IceGolem);
            MinibossHPBarList.Add(NPCID.SandElemental);
            MinibossHPBarList.Add(NPCID.BigMimicCorruption);
            MinibossHPBarList.Add(NPCID.BigMimicCrimson);
            MinibossHPBarList.Add(NPCID.BigMimicHallow);

            //Moon Events
            MinibossHPBarList.Add(NPCID.MourningWood);
            MinibossHPBarList.Add(NPCID.Pumpking);
            MinibossHPBarList.Add(NPCID.Everscream);
            MinibossHPBarList.Add(NPCID.SantaNK1);
            MinibossHPBarList.Add(NPCID.IceQueen);

            //Eclipse
            MinibossHPBarList.Add(NPCID.Mothron);

            Mod calamity = ModLoader.GetMod("CalamityModClassicPreTrailer");
            if (calamity != null)
            {
				//Prehardmode
				MinibossHPBarList.Add(calamity.Find<ModNPC>("GiantClam").Type);

				//Hardmode
				MinibossHPBarList.Add(calamity.Find<ModNPC>("ThiccWaifu").Type);
                MinibossHPBarList.Add(calamity.Find<ModNPC>("Horse").Type);
                MinibossHPBarList.Add(calamity.Find<ModNPC>("GreatSandShark").Type);
                MinibossHPBarList.Add(calamity.Find<ModNPC>("PlaguebringerShade").Type);
                MinibossHPBarList.Add(calamity.Find<ModNPC>("ArmoredDiggerHead").Type);

                //Abyss
                MinibossHPBarList.Add(calamity.Find<ModNPC>("EidolonWyrmHeadHuge").Type);
                
                //Post-ML
                MinibossHPBarList.Add(calamity.Find<ModNPC>("SupremeCataclysm").Type);
                MinibossHPBarList.Add(calamity.Find<ModNPC>("SupremeCatastrophe").Type);
                MinibossHPBarList.Add(calamity.Find<ModNPC>("ProvSpawnDefense").Type);
                MinibossHPBarList.Add(calamity.Find<ModNPC>("ProvSpawnOffense").Type);
                MinibossHPBarList.Add(calamity.Find<ModNPC>("ProvSpawnHealer").Type);
            }
        }

        public static void Unload()
        {
            BossMainHPBar = null;
            BossComboHPBar = null;
            BossSeperatorBar = null;
            HPBarFont = null;
            Bars = null;
            ExclusionList = null;
            MinibossHPBarList = null;
            OneToMany = null;
        }

        public static void Update()
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                //Is NPC active
                if (!Main.npc[i].active) continue;

                //Is npc in exclusion list
                int type = Main.npc[i].type;
                if (Main.npc[i].type == NPCID.MoonLordCore)
                {
                    AttemptAddBar(i, NPCID.MoonLordCore);
                }
                if (ExclusionList.Contains(type)) continue;

                if (Main.npc[i].type == NPCID.BrainofCthulhu)
                {
                    AttemptAddBar(i, NPCID.BrainofCthulhu);
                }
                else if (Main.npc[i].type == NPCID.SkeletronHead)
                {
                    AttemptAddBar(i, NPCID.SkeletronHead);
                }
                else if (Main.npc[i].type == NPCID.SkeletronPrime)
                {
                    AttemptAddBar(i, NPCID.SkeletronPrime);
                }
                else if (Main.npc[i].type == NPCID.MartianSaucerCore)
                {
                    AttemptAddBar(i, NPCID.MartianSaucerCore);
                }
                else if (Main.npc[i].type == NPCID.PirateShip)
                {
                    AttemptAddBar(i, NPCID.PirateShip);
                }
                else if (Main.npc[i].type == ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("AstrumDeusHeadSpectral").Type)
                {
                    AttemptAddBar(i, ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("AstrumDeusHeadSpectral").Type);
                }
                else if (Main.npc[i].type == ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("CalamitasRun3").Type)
                {
                    AttemptAddBar(i, ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("CalamitasRun3").Type);
                }
                else if (Main.npc[i].type == ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("CeaselessVoid").Type)
                {
                    AttemptAddBar(i, ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("CeaselessVoid").Type);
                }
                else if (Main.npc[i].type == ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("PerforatorHive").Type)
                {
                    AttemptAddBar(i, ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("PerforatorHive").Type);
                }
                else if (Main.npc[i].type == ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("ScavengerBody").Type)
                {
                    AttemptAddBar(i, ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("ScavengerBody").Type);
                }
                else if (Main.npc[i].type == ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("SlimeGodCore").Type)
                {
                    AttemptAddBar(i, ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("SlimeGodCore").Type);
                }
                else if (Main.npc[i].boss || MinibossHPBarList.Contains(type))
                {
                    AttemptAddBar(i);
                }
                //If, specifically, they're the eater of worlds head
                if (Main.npc[i].type == NPCID.EaterofWorldsHead)
                {
                    AttemptAddBar(i, NPCID.EaterofWorldsHead);
                }
            }

            for (int i = 0; i < Bars.Count; i++)
            {
                BossHPUI ui = Bars[i];
                //Update the bar
                ui.Update();
                //Is the NPC the bar is attached to dead?
                if (ui.IsDead())
                {
                    //begin closing anim of the UI
                    ui.StartClosing();
                }
                if (ui.ShouldClose())
                {
                    //remove this bar
                    Bars.RemoveAt(i);
                    i--;
                }
            }
        }

        private static void AttemptAddBar(int index, int type = -1)
        {
            //Limit the number of bars.
            if (Bars.Count >= MAX_BARS) return;

            bool hasBar = false;

            foreach (BossHPUI ui in Bars)
            {
                int id = ui.GetNPCID();
                if (id == index)
                {
                    hasBar = true;
                    break;
                }
                //Sort out the eater of worlds splitting into multiple segments and multiple of them being heads
                if (type == NPCID.EaterofWorldsHead)
                {
                    if (Main.npc[id].type == NPCID.EaterofWorldsHead)
                    {
                        hasBar = true;
                    }
                }
            }

            if (!hasBar)
            {
                BossHPUI newUI = new BossHPUI(index);

                if (type != -1)
                {
                    newUI.SetupForType(type);
                }

                Bars.Add(newUI);
            }
        }

        public static void Draw(SpriteBatch sb)
        {
            int startHeight = 100; //100
            int heightPerOne = 70; //70

            int y = Main.screenHeight - startHeight;
            int x = Main.screenWidth - 420 + ((Main.playerInventory || Main.invasionType > 0 || Main.pumpkinMoon || Main.snowMoon || DD2Event.Ongoing) ? -250 : 0);

            foreach (BossHPUI ui in Bars)
            {
                ui.Draw(sb, x, y);
                y -= heightPerOne;
            }
        }

        //Actual UI handling class
        internal class BossHPUI
        {
            private static readonly Color OrangeColour = new Color(229, 189, 62);
            private static readonly Color OrangeBorderColour = new Color(197, 127, 46);

            private static readonly int MainBarYOffset = 28;
            private static readonly int SepBarYOffset = 18;
            private static readonly int BarMaxWidth = 400;
            private static readonly int OpenAnimTime = 80;
            private static readonly int CloseAnimTime = 120;

            enum SpecialType : byte
            {
                None,
                EaterOfWorlds,
                Creep,
                Skelet,
                SkeletPrime,
                MartSaucer,
                PirShip,
                Astrum
            }

            enum SpecialType2 : byte
            {
                None,
                Calam,
                Ceaseless,
                Perforator,
                Ravage,
                SlimeCore,
                MoonCore
            }

            private int _npcLocation;
            private SpecialType _special = SpecialType.None;
            private SpecialType2 _special2 = SpecialType2.None;
            private int[] _specialData = new int[7];
            private int[] _specialData2 = new int[7];
            private float _maxHealth;
            private int _prevLife;
            private bool _inCombo = false;
            private int _comboStartHealth;
            private int _damageCountdown;
            private NPC _npc
            {
                get
                {
                    return Main.npc[_npcLocation];
                }
            }

            private string _lastName = "";

            private bool _oneToMany = false;
            private int[] _arrayOfIds;

            //EDITABLE
            public void SetupForType(int type)
            {
                if (type == NPCID.MoonLordCore)
                {
                    _special2 = SpecialType2.MoonCore;
                }
                else if (type == ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("SlimeGodCore").Type)
                {
                    _special2 = SpecialType2.SlimeCore;
                }
                else if (type == ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("ScavengerBody").Type)
                {
                    _special2 = SpecialType2.Ravage;
                }
                else if (type == ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("PerforatorHive").Type)
                {
                    _special2 = SpecialType2.Perforator;
                }
                else if (type == ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("CeaselessVoid").Type)
                {
                    _special2 = SpecialType2.Ceaseless;
                }
                else if (type == ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("CalamitasRun3").Type)
                {
                    _special2 = SpecialType2.Calam;
                }
                else if (type == ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("AstrumDeusHeadSpectral").Type)
                {
                    _special = SpecialType.Astrum;
                }
                else
                {
                    switch (type)
                    {
                        case NPCID.EaterofWorldsHead:
                            _special = SpecialType.EaterOfWorlds;
                            break;
                        case NPCID.BrainofCthulhu:
                            _special = SpecialType.Creep;
                            break;
                        case NPCID.SkeletronHead:
                            _special = SpecialType.Skelet;
                            break;
                        case NPCID.SkeletronPrime:
                            _special = SpecialType.SkeletPrime;
                            break;
                        case NPCID.MartianSaucerCore:
                            _special = SpecialType.MartSaucer;
                            break;
                        case NPCID.PirateShip:
                            _special = SpecialType.PirShip;
                            break;
                    }
                }
            }

            //ANIMATION FIELDS
            private int _openAnimCounter = OpenAnimTime;
            private int _closeAnimCounter;

            public BossHPUI(int id)
            {
                _npcLocation = id;

                _maxHealth = Main.npc[id].lifeMax;

                if (OneToMany.ContainsKey(Main.npc[id].type))
                {
                    _oneToMany = true;
                    _arrayOfIds = OneToMany[Main.npc[id].type];
                }
            }

            public int GetNPCID()
            {
                return _npcLocation;
            }
            public bool ShouldClose()
            {
                return _closeAnimCounter >= CloseAnimTime;
            }
            public void StartClosing()
            {
                if (_closeAnimCounter == 0)
                    _closeAnimCounter = 1;
            }

            public bool IsDead()
            {
                bool dead = false;

                if (_npc == null)
                    dead = true;

                int life = _npc.life;
                if (life < 0 || !_npc.active || _npc.lifeMax < 800)
                    dead = true;

                if (_oneToMany)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        if (Main.npc[i].active && Main.npc[i].life > 0 && _arrayOfIds.Contains(Main.npc[i].type))
                        {
                            dead = false;
                        }
                    }
                }

                if (dead && _lastName == "")
                {
                    _lastName = _npc.FullName;
                }

                return dead;
            }

            public void Update()
            {
                if (_closeAnimCounter != 0) return;

                int currentLife = _npc.life;

                //Calculate current life based all types that are available and considered part of one boss
                if (_oneToMany)
                {
                    currentLife = 0;
                    int maxLife = 0;
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        if (Main.npc[i].active && Main.npc[i].life > 0 && _arrayOfIds.Contains(Main.npc[i].type))
                        {
                            if ((Main.npc[i].type == NPCID.MoonLordHead || Main.npc[i].type == NPCID.MoonLordHand || Main.npc[i].type == NPCID.MoonLordCore) && 
                                Main.npc[i].ai[0] == -2f) continue;
                            currentLife += Main.npc[i].life;
                            maxLife += Main.npc[i].lifeMax;
                        }
                    }
                    if (maxLife > _maxHealth)
                    {
                        _maxHealth = maxLife;
                    }
                }

                //Damage countdown
                if (_damageCountdown > 0)
                {
                    _damageCountdown--;
                    if (_damageCountdown == 0)
                    {
                        //This means we need to finish the combo
                        _comboStartHealth = 0;
                        _inCombo = false;
                    }
                }
                //If the current life is not eqaul to the previous frame of life (_prevLife != 0 ensures it's not on it's first frame)
                if (currentLife != _prevLife && _prevLife != 0)
                {
                    //If there's no ongoing combo
                    if (!_inCombo)
                    {
                        //This means we need to start a combo
                        _comboStartHealth = _prevLife;
                        _inCombo = true;
                    }
                    _damageCountdown = 30; //60
                }
                _prevLife = currentLife;

                switch (_special)
                {
                    default:
                        break;
                    case SpecialType.EaterOfWorlds:
                        //Count the segments of the EoW
                        int count = 0;
                        for (int i = 0; i < Main.npc.Length; i++)
                        {
                            if (IsEoW(i))
                            {
                                count++;
                            }
                        }
                        _specialData[0] = count;
                        break;
                    case SpecialType.Creep:
                        int count2 = NPC.CountNPCS(NPCID.Creeper);
                        _specialData[1] = count2;
                        break;
                    case SpecialType.Skelet:
                        int count3 = NPC.CountNPCS(NPCID.SkeletronHand);
                        _specialData[2] = count3;
                        break;
                    case SpecialType.SkeletPrime:
                        int count4 = NPC.CountNPCS(NPCID.PrimeVice) + NPC.CountNPCS(NPCID.PrimeCannon) + 
                            NPC.CountNPCS(NPCID.PrimeSaw) + NPC.CountNPCS(NPCID.PrimeLaser);
                        _specialData[3] = count4;
                        break;
                    case SpecialType.MartSaucer:
                        int count5 = NPC.CountNPCS(NPCID.MartianSaucerTurret) + NPC.CountNPCS(NPCID.MartianSaucerCannon);
                        _specialData[4] = count5;
                        break;
                    case SpecialType.PirShip:
                        int count6 = NPC.CountNPCS(NPCID.PirateShipCannon);
                        _specialData[5] = count6;
                        break;
                    case SpecialType.Astrum:
                        int count7 = NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("AstrumDeusHead").Type);
                        _specialData[6] = count7;
                        break;
                }

                switch (_special2)
                {
                    default:
                        break;
                    case SpecialType2.Calam:
                        int count = NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("CalamitasRun").Type) + 
                            NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("CalamitasRun2").Type);
                        _specialData2[0] = count;
                        break;
                    case SpecialType2.Ceaseless:
                        int count2 = NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("DarkEnergy").Type) + 
                            NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("DarkEnergy2").Type) +
                            NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("DarkEnergy3").Type);
                        _specialData2[1] = count2;
                        break;
                    case SpecialType2.Perforator:
                        int count3 = NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("PerforatorHeadSmall").Type) +
                            NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("PerforatorHeadMedium").Type) +
                            NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("PerforatorHeadLarge").Type);
                        _specialData2[2] = count3;
                        break;
                    case SpecialType2.Ravage:
                        int count4 = NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("ScavengerClawRight").Type) +
                            NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("ScavengerClawLeft").Type) +
                            NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("ScavengerLegRight").Type) +
                            NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("ScavengerLegLeft").Type) +
                            NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("ScavengerHead").Type);
                        _specialData2[3] = count4;
                        break;
                    case SpecialType2.SlimeCore:
                        int count5 = NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("SlimeGod").Type) +
                            NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("SlimeGodSplit").Type) +
                            NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("SlimeGodRun").Type) +
                            NPC.CountNPCS(ModLoader.GetMod("CalamityModClassicPreTrailer").Find<ModNPC>("SlimeGodRunSplit").Type);
                        _specialData2[4] = count5;
                        break;
                    case SpecialType2.MoonCore:
                        if (NPC.CountNPCS(NPCID.MoonLordFreeEye) > 3)
                        {
                            int count6 = NPC.CountNPCS(NPCID.MoonLordFreeEye) - 3;
                            _specialData2[5] = count6;
                        }
                        break;
                }
            }
            public void Draw(SpriteBatch sb, int x, int y)
            {
                //Draw the respective animations.
                //Easier to separate them, even if it does result in more copy pasted code overall.
                if (_openAnimCounter > 0)
                {
                    DrawOpenAnim(sb, x, y);
                    return;
                }

                if (_closeAnimCounter > 0)
                {
                    DrawCloseAnim(sb, x, y);
                    return;
                }

                float percentHealth = (_prevLife / _maxHealth);
                int mainBarWidth = (int)(BarMaxWidth * percentHealth);

                if (_inCombo)
                {
                    //DRAW COMBO HEALTH BAR
                    int comboBarWidth = (int)(BarMaxWidth * (_comboStartHealth / _maxHealth)) - mainBarWidth;
                    float alpha = 1f;
                    if (_damageCountdown < 6)
                    {
                        float val = _damageCountdown * 0.166f;
                        alpha *= val;
                        comboBarWidth = (int)(comboBarWidth * val);
                    }

                    sb.Draw(BossComboHPBar, new Rectangle(x + mainBarWidth, y + MainBarYOffset, comboBarWidth, 15), Color.White * alpha);
                }

                //DRAW MAIN HEALTH BAR
                sb.Draw(BossMainHPBar, new Rectangle(x, y + MainBarYOffset, mainBarWidth, 15), Color.White);

                //DRAW WHITE(ISH) LINE
                sb.Draw(BossSeperatorBar, new Rectangle(x, y + SepBarYOffset, BarMaxWidth, 6), new Color(240, 240, 255));

                //DRAW TEXT
                string percentHealthText = (percentHealth * 100).ToString("N1") + "%";
                if (_prevLife == _maxHealth) percentHealthText = "100%";
                Vector2 textSize = HPBarFont.MeasureString(percentHealthText);
                DrawBorderStringEightWay(sb, HPBarFont, percentHealthText, new Vector2(x, y + 22 - textSize.Y), OrangeColour, OrangeBorderColour * 0.25f);

                string name = _npc.FullName;
                Vector2 nameSize = FontAssets.MouseText.Value.MeasureString(name);
                DrawBorderStringEightWay(sb, FontAssets.MouseText.Value, name, new Vector2(x + BarMaxWidth - nameSize.X, y + 23 - nameSize.Y), Color.White, Color.Black * 0.2f);

                //TODO: Make this toggleable
                if (SHOULD_DRAW_SMALLTEXT_HEALTH)
                {
                    float textScale = 0.75f;

                    switch (_special)
                    {
                        default:
                            break;
                        case SpecialType.EaterOfWorlds:
                            string count = "(Segments left: " + _specialData[0] + ")";
                            Vector2 countSize = FontAssets.ItemStack.Value.MeasureString(count) * textScale;
                            float countX = Math.Max(x, x + mainBarWidth - countSize.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count, new Vector2(countX, y + MainBarYOffset + 17), Color.White, Color.Black * 0.24f, textScale);
                            return;
                        case SpecialType.Creep:
                            string count2 = "(Creepers left: " + _specialData[1] + ")";
                            Vector2 countSize2 = FontAssets.ItemStack.Value.MeasureString(count2) * textScale;
                            float countX2 = Math.Max(x, x + mainBarWidth - countSize2.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count2, new Vector2(countX2, y + MainBarYOffset + 17), Color.White, Color.Black * 0.24f, textScale);
                            return;
                        case SpecialType.Skelet:
                            string count3 = "(Hands left: " + _specialData[2] + ")";
                            Vector2 countSize3 = FontAssets.ItemStack.Value.MeasureString(count3) * textScale;
                            float countX3 = Math.Max(x, x + mainBarWidth - countSize3.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count3, new Vector2(countX3, y + MainBarYOffset + 17), Color.White, Color.Black * 0.24f, textScale);
                            return;
                        case SpecialType.SkeletPrime:
                            string count4 = "(Arms left: " + _specialData[3] + ")";
                            Vector2 countSize4 = FontAssets.ItemStack.Value.MeasureString(count4) * textScale;
                            float countX4 = Math.Max(x, x + mainBarWidth - countSize4.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count4, new Vector2(countX4, y + MainBarYOffset + 17), Color.White, Color.Black * 0.24f, textScale);
                            return;
                        case SpecialType.MartSaucer:
                            string count5 = "(Guns left: " + _specialData[4] + ")";
                            Vector2 countSize5 = FontAssets.ItemStack.Value.MeasureString(count5) * textScale;
                            float countX5 = Math.Max(x, x + mainBarWidth - countSize5.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count5, new Vector2(countX5, y + MainBarYOffset + 17), Color.White, Color.Black * 0.24f, textScale);
                            return;
                        case SpecialType.PirShip:
                            string count6 = "(Cannons left: " + _specialData[5] + ")";
                            Vector2 countSize6 = FontAssets.ItemStack.Value.MeasureString(count6) * textScale;
                            float countX6 = Math.Max(x, x + mainBarWidth - countSize6.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count6, new Vector2(countX6, y + MainBarYOffset + 17), Color.White, Color.Black * 0.24f, textScale);
                            return;
                        case SpecialType.Astrum:
                            string count7 = "(Small Worms left: " + _specialData[6] + ")";
                            Vector2 countSize7 = FontAssets.ItemStack.Value.MeasureString(count7) * textScale;
                            float countX7 = Math.Max(x, x + mainBarWidth - countSize7.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count7, new Vector2(countX7, y + MainBarYOffset + 17), Color.White, Color.Black * 0.24f, textScale);
                            return;
                    }

                    switch (_special2)
                    {
                        default:
                            break;
                        case SpecialType2.Calam:
                            string count = "(Brothers left: " + _specialData2[0] + ")";
                            Vector2 countSize = FontAssets.ItemStack.Value.MeasureString(count) * textScale;
                            float countX = Math.Max(x, x + mainBarWidth - countSize.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count, new Vector2(countX, y + MainBarYOffset + 17), Color.White, Color.Black * 0.24f, textScale);
                            return;
                        case SpecialType2.Ceaseless:
                            string count2 = "(Dark Energy left: " + _specialData2[1] + ")";
                            Vector2 countSize2 = FontAssets.ItemStack.Value.MeasureString(count2) * textScale;
                            float countX2 = Math.Max(x, x + mainBarWidth - countSize2.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count2, new Vector2(countX2, y + MainBarYOffset + 17), Color.White, Color.Black * 0.24f, textScale);
                            return;
                        case SpecialType2.Perforator:
                            string count3 = "(Worms left: " + _specialData2[2] + ")";
                            Vector2 countSize3 = FontAssets.ItemStack.Value.MeasureString(count3) * textScale;
                            float countX3 = Math.Max(x, x + mainBarWidth - countSize3.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count3, new Vector2(countX3, y + MainBarYOffset + 17), Color.White, Color.Black * 0.24f, textScale);
                            return;
                        case SpecialType2.Ravage:
                            string count4 = "(Body Parts left: " + _specialData2[3] + ")";
                            Vector2 countSize4 = FontAssets.ItemStack.Value.MeasureString(count4) * textScale;
                            float countX4 = Math.Max(x, x + mainBarWidth - countSize4.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count4, new Vector2(countX4, y + MainBarYOffset + 17), Color.White, Color.Black * 0.24f, textScale);
                            return;
                        case SpecialType2.SlimeCore:
                            string count5 = "(Large Slimes left: " + _specialData2[4] + ")";
                            Vector2 countSize5 = FontAssets.ItemStack.Value.MeasureString(count5) * textScale;
                            float countX5 = Math.Max(x, x + mainBarWidth - countSize5.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count5, new Vector2(countX5, y + MainBarYOffset + 17), Color.White, Color.Black * 0.24f, textScale);
                            return;
                        case SpecialType2.MoonCore:
                            string count7 = "(Vulnerable Eyes left: " + _specialData2[5] + ")";
                            Vector2 countSize7 = FontAssets.ItemStack.Value.MeasureString(count7) * textScale;
                            float countX7 = Math.Max(x, x + mainBarWidth - countSize7.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count7, new Vector2(countX7, y + MainBarYOffset + 17), Color.White, Color.Black * 0.24f, textScale);
                            return;
                    }

                    string actualLife = "(" + _npc.life + " / " + _npc.lifeMax + ")";
                    Vector2 lifeSize = FontAssets.ItemStack.Value.MeasureString(actualLife) * textScale;
                    float lifeX = Math.Max(x, x + mainBarWidth - lifeSize.X);
                    DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, actualLife, new Vector2(lifeX, y + MainBarYOffset + 17), Color.White, Color.Black * 0.24f, textScale);
                }
            }
            public void DrawOpenAnim(SpriteBatch sb, int x, int y)
            {
                float percentThroughAnim = (OpenAnimTime - _openAnimCounter) / (float)OpenAnimTime;
                int mainBarWidth = (int)(BarMaxWidth * MathHelper.SmoothStep(0f, 1f, percentThroughAnim));

                float flickerValue = percentThroughAnim;
                //FLICKER 3 TIMES, QUICK AND DIRTY METHOD
                if (_openAnimCounter == OpenAnimTime - 4 || _openAnimCounter == OpenAnimTime - 8 || _openAnimCounter == OpenAnimTime - 16)
                {
                    flickerValue = Main.rand.NextFloat(0.7f, 0.8f);
                }
                else if (_openAnimCounter == OpenAnimTime - 5 || _openAnimCounter == OpenAnimTime - 9 || _openAnimCounter == OpenAnimTime - 17)
                {
                    flickerValue = Main.rand.NextFloat(0.4f, 0.5f);
                }

                //DRAW MAIN HEALTH BAR
                sb.Draw(BossMainHPBar, new Rectangle(x, y + MainBarYOffset, mainBarWidth, 15), Color.White * flickerValue);

                //DRAW WHITE(ISH) LINE
                sb.Draw(BossSeperatorBar, new Rectangle(x, y + SepBarYOffset, BarMaxWidth, 6), new Color(240, 240, 255) * flickerValue);

                //DRAW TEXT
                string percentHealthText = "100%";
                Vector2 textSize = HPBarFont.MeasureString(percentHealthText);
                DrawBorderStringEightWay(sb, HPBarFont, percentHealthText, new Vector2(x, y + 22 - textSize.Y), OrangeColour * flickerValue, OrangeBorderColour * 0.25f * flickerValue);

                string name = _npc.FullName;
                Vector2 nameSize = FontAssets.MouseText.Value.MeasureString(name);
                DrawBorderStringEightWay(sb, FontAssets.MouseText.Value, name, new Vector2(x + BarMaxWidth - nameSize.X, y + 23 - nameSize.Y), Color.White * flickerValue, Color.Black * 0.2f * flickerValue);

                //TODO: Make this toggleable
                if (SHOULD_DRAW_SMALLTEXT_HEALTH)
                {
                    float textScale = 0.75f;

                    switch (_special)
                    {
                        default:
                            break;
                        case SpecialType.EaterOfWorlds:
                            string count = "(Segments left: " + _specialData[0] + ")";
                            Vector2 countSize = FontAssets.ItemStack.Value.MeasureString(count) * textScale;
                            float countX = Math.Max(x, x + mainBarWidth - countSize.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count, new Vector2(countX, y + MainBarYOffset + 17), Color.White * flickerValue, Color.Black * 0.24f * flickerValue, textScale);
                            _openAnimCounter--;
                            return;
                        case SpecialType.Creep:
                            string count2 = "(Creepers left: " + _specialData[1] + ")";
                            Vector2 countSize2 = FontAssets.ItemStack.Value.MeasureString(count2) * textScale;
                            float countX2 = Math.Max(x, x + mainBarWidth - countSize2.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count2, new Vector2(countX2, y + MainBarYOffset + 17), Color.White * flickerValue, Color.Black * 0.24f * flickerValue, textScale);
                            _openAnimCounter--;
                            return;
                        case SpecialType.Skelet:
                            string count3 = "(Hands left: " + _specialData[2] + ")";
                            Vector2 countSize3 = FontAssets.ItemStack.Value.MeasureString(count3) * textScale;
                            float countX3 = Math.Max(x, x + mainBarWidth - countSize3.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count3, new Vector2(countX3, y + MainBarYOffset + 17), Color.White * flickerValue, Color.Black * 0.24f * flickerValue, textScale);
                            _openAnimCounter--;
                            return;
                        case SpecialType.SkeletPrime:
                            string count4 = "(Arms left: " + _specialData[3] + ")";
                            Vector2 countSize4 = FontAssets.ItemStack.Value.MeasureString(count4) * textScale;
                            float countX4 = Math.Max(x, x + mainBarWidth - countSize4.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count4, new Vector2(countX4, y + MainBarYOffset + 17), Color.White * flickerValue, Color.Black * 0.24f * flickerValue, textScale);
                            _openAnimCounter--;
                            return;
                        case SpecialType.MartSaucer:
                            string count5 = "(Guns left: " + _specialData[4] + ")";
                            Vector2 countSize5 = FontAssets.ItemStack.Value.MeasureString(count5) * textScale;
                            float countX5 = Math.Max(x, x + mainBarWidth - countSize5.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count5, new Vector2(countX5, y + MainBarYOffset + 17), Color.White * flickerValue, Color.Black * 0.24f * flickerValue, textScale);
                            _openAnimCounter--;
                            return;
                        case SpecialType.PirShip:
                            string count6 = "(Cannons left: " + _specialData[5] + ")";
                            Vector2 countSize6 = FontAssets.ItemStack.Value.MeasureString(count6) * textScale;
                            float countX6 = Math.Max(x, x + mainBarWidth - countSize6.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count6, new Vector2(countX6, y + MainBarYOffset + 17), Color.White * flickerValue, Color.Black * 0.24f * flickerValue, textScale);
                            _openAnimCounter--;
                            return;
                        case SpecialType.Astrum:
                            string count7 = "(Small Worms left: " + _specialData[6] + ")";
                            Vector2 countSize7 = FontAssets.ItemStack.Value.MeasureString(count7) * textScale;
                            float countX7 = Math.Max(x, x + mainBarWidth - countSize7.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count7, new Vector2(countX7, y + MainBarYOffset + 17), Color.White * flickerValue, Color.Black * 0.24f * flickerValue, textScale);
                            _openAnimCounter--;
                            return;
                    }

                    switch (_special2)
                    {
                        default:
                            break;
                        case SpecialType2.Calam:
                            string count = "(Brothers left: " + _specialData2[0] + ")";
                            Vector2 countSize = FontAssets.ItemStack.Value.MeasureString(count) * textScale;
                            float countX = Math.Max(x, x + mainBarWidth - countSize.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count, new Vector2(countX, y + MainBarYOffset + 17), Color.White * flickerValue, Color.Black * 0.24f * flickerValue, textScale);
                            _openAnimCounter--;
                            return;
                        case SpecialType2.Ceaseless:
                            string count2 = "(Dark Energy left: " + _specialData2[1] + ")";
                            Vector2 countSize2 = FontAssets.ItemStack.Value.MeasureString(count2) * textScale;
                            float countX2 = Math.Max(x, x + mainBarWidth - countSize2.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count2, new Vector2(countX2, y + MainBarYOffset + 17), Color.White * flickerValue, Color.Black * 0.24f * flickerValue, textScale);
                            _openAnimCounter--;
                            return;
                        case SpecialType2.Perforator:
                            string count3 = "(Worms left: " + _specialData2[2] + ")";
                            Vector2 countSize3 = FontAssets.ItemStack.Value.MeasureString(count3) * textScale;
                            float countX3 = Math.Max(x, x + mainBarWidth - countSize3.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count3, new Vector2(countX3, y + MainBarYOffset + 17), Color.White * flickerValue, Color.Black * 0.24f * flickerValue, textScale);
                            _openAnimCounter--;
                            return;
                        case SpecialType2.Ravage:
                            string count4 = "(Body Parts left: " + _specialData2[3] + ")";
                            Vector2 countSize4 = FontAssets.ItemStack.Value.MeasureString(count4) * textScale;
                            float countX4 = Math.Max(x, x + mainBarWidth - countSize4.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count4, new Vector2(countX4, y + MainBarYOffset + 17), Color.White * flickerValue, Color.Black * 0.24f * flickerValue, textScale);
                            _openAnimCounter--;
                            return;
                        case SpecialType2.SlimeCore:
                            string count5 = "(Large Slimes left: " + _specialData2[4] + ")";
                            Vector2 countSize5 = FontAssets.ItemStack.Value.MeasureString(count5) * textScale;
                            float countX5 = Math.Max(x, x + mainBarWidth - countSize5.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count5, new Vector2(countX5, y + MainBarYOffset + 17), Color.White * flickerValue, Color.Black * 0.24f * flickerValue, textScale);
                            _openAnimCounter--;
                            return;
                        case SpecialType2.MoonCore:
                            string count7 = "(Vulnerable Eyes left: " + _specialData2[5] + ")";
                            Vector2 countSize7 = FontAssets.ItemStack.Value.MeasureString(count7) * textScale;
                            float countX7 = Math.Max(x, x + mainBarWidth - countSize7.X);
                            DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, count7, new Vector2(countX7, y + MainBarYOffset + 17), Color.White * flickerValue, Color.Black * 0.24f * flickerValue, textScale);
                            _openAnimCounter--;
                            return;
                    }

                    string actualLife = "(" + _npc.life + " / " + _npc.lifeMax + ")";
                    Vector2 lifeSize = FontAssets.ItemStack.Value.MeasureString(actualLife) * textScale;
                    float lifeX = Math.Max(x, x + mainBarWidth - lifeSize.X);
                    DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, actualLife, new Vector2(lifeX, y + MainBarYOffset + 17), Color.White * flickerValue, Color.Black * 0.24f * flickerValue, textScale);
                }

                _openAnimCounter--;
            }
            public void DrawCloseAnim(SpriteBatch sb, int x, int y)
            {
                float percentThroughAnim = _closeAnimCounter / (float)CloseAnimTime;
                float reversePercent = 1f - percentThroughAnim;

                float percentHealth = (_prevLife / _maxHealth);
                if (percentHealth < 0) percentHealth = 0;

                int mainBarWidth = (int)(BarMaxWidth * MathHelper.SmoothStep(0f, 1f, reversePercent) * percentHealth);

                //DRAW MAIN HEALTH BAR
                sb.Draw(BossMainHPBar, new Rectangle(x, y + MainBarYOffset, mainBarWidth, 15), Color.White * reversePercent);

                //DRAW WHITE(ISH) LINE
                sb.Draw(BossSeperatorBar, new Rectangle(x, y + SepBarYOffset, BarMaxWidth, 6), new Color(240, 240, 255) * reversePercent);

                //DRAW TEXT
                string percentHealthText = (percentHealth * 100).ToString("N1") + "%";
                if (_prevLife <= 0) percentHealthText = "0%";
                if (_prevLife == _maxHealth) percentHealthText = "100%";

                Vector2 textSize = HPBarFont.MeasureString(percentHealthText);
                DrawBorderStringEightWay(sb, HPBarFont, percentHealthText, new Vector2(x, y + 22 - textSize.Y), OrangeColour * reversePercent, OrangeBorderColour * 0.25f * reversePercent);

                string name = _lastName;
                Vector2 nameSize = FontAssets.MouseText.Value.MeasureString(name);
                DrawBorderStringEightWay(sb, FontAssets.MouseText.Value, name, new Vector2(x + BarMaxWidth - nameSize.X, y + 23 - nameSize.Y), Color.White * reversePercent, Color.Black * 0.2f * reversePercent);

                _closeAnimCounter++;
            }

            //UTILS
            private void DrawBorderStringEightWay(SpriteBatch sb, DynamicSpriteFont font, string text, Vector2 position, Color main, Color border, float scale = 1f)
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        Vector2 pos = position + new Vector2(x, y);
                        if (x == 0 && y == 0)
                        {
                            continue;
                        }

                        DynamicSpriteFontExtensionMethods.DrawString(sb, font, text, pos, border, 0f, default(Vector2), scale, SpriteEffects.None, 0f);
                    }
                }
                DynamicSpriteFontExtensionMethods.DrawString(sb, font, text, position, main, 0f, default(Vector2), scale, SpriteEffects.None, 0f);
            }
            private bool IsEoW(int id)
            {
                NPC n = Main.npc[id];

                if (!n.active || n.life <= 0) return false;

                return n.type == NPCID.EaterofWorldsHead ||
                       n.type == NPCID.EaterofWorldsBody ||
                       n.type == NPCID.EaterofWorldsTail;
            }
        }
    }
}
