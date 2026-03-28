using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityModClassicPreTrailer.Buffs;
using CalamityModClassicPreTrailer.Buffs.Fabsol;
using CalamityModClassicPreTrailer.Items.Amidias;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Items.Weapons;
using CalamityModClassicPreTrailer.Items.Weapons.AbyssWeapons;
using CalamityModClassicPreTrailer.Items.Weapons.AquaticScourge;
using CalamityModClassicPreTrailer.Items.Weapons.Calamitas;
using CalamityModClassicPreTrailer.Items.Weapons.DevourerofGods;
using CalamityModClassicPreTrailer.Items.Weapons.Perforators;
using CalamityModClassicPreTrailer.Items.Weapons.Plaguebringer;
using CalamityModClassicPreTrailer.Items.Weapons.Providence;
using CalamityModClassicPreTrailer.Items.Weapons.RareVariants;
using CalamityModClassicPreTrailer.Items.Weapons.SlimeGod;
using CalamityModClassicPreTrailer.Items.Weapons.Yharon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.IO;
using Terraria.Map;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.Localization;
using CalamityModClassicPreTrailer.NPCs;
using CalamityModClassicPreTrailer.NPCs.AbyssNPCs;
using CalamityModClassicPreTrailer.NPCs.Astrageldon;
using CalamityModClassicPreTrailer.NPCs.AstralBiomeNPCs;
using CalamityModClassicPreTrailer.NPCs.AstrumDeus;
using CalamityModClassicPreTrailer.NPCs.Bumblefuck;
using CalamityModClassicPreTrailer.NPCs.TheDevourerofGods;
using CalamityModClassicPreTrailer.NPCs.Calamitas;
using CalamityModClassicPreTrailer.NPCs.CosmicWraith;
using CalamityModClassicPreTrailer.NPCs.Crabulon;
using CalamityModClassicPreTrailer.NPCs.Cryogen;
using CalamityModClassicPreTrailer.NPCs.DesertScourge;
using CalamityModClassicPreTrailer.NPCs.GreatSandShark;
using CalamityModClassicPreTrailer.NPCs.HiveMind;
using CalamityModClassicPreTrailer.NPCs.PlaguebringerGoliath;
using CalamityModClassicPreTrailer.NPCs.Yharon;
using CalamityModClassicPreTrailer.NPCs.Leviathan;
using CalamityModClassicPreTrailer.NPCs.NormalNPCs;
using CalamityModClassicPreTrailer.NPCs.Perforator;
using CalamityModClassicPreTrailer.NPCs.PlaguebringerShade;
using CalamityModClassicPreTrailer.NPCs.Providence;
using CalamityModClassicPreTrailer.NPCs.SupremeCalamitas;
using CalamityModClassicPreTrailer.NPCs.Polterghast;
using CalamityModClassicPreTrailer.NPCs.ProfanedGuardianBoss;
using CalamityModClassicPreTrailer.NPCs.Scavenger;
using CalamityModClassicPreTrailer.NPCs.SlimeGod;
using CalamityModClassicPreTrailer.NPCs.StormWeaver;
using CalamityModClassicPreTrailer.NPCs.SunkenSeaNPCs;
using CalamityModClassicPreTrailer.Projectiles.Boss;
using CalamityModClassicPreTrailer.Projectiles.Ranged;
using CalamityModClassicPreTrailer.Projectiles.SunkenSea;
using CalamityModClassicPreTrailer.Tiles;
using CalamityModClassicPreTrailer.UI;
using CalamityModClassicPreTrailer.Skies;
using ReLogic.Content;
using Terraria.Audio;
using AMR = CalamityModClassicPreTrailer.Projectiles.Ranged.AMR;
using Archerfish = CalamityModClassicPreTrailer.Projectiles.Ranged.Archerfish;
using Butcher = CalamityModClassicPreTrailer.Projectiles.Ranged.Butcher;
using Contagion = CalamityModClassicPreTrailer.Projectiles.Ranged.Contagion;
using DankCreeper = CalamityModClassicPreTrailer.Buffs.DankCreeper;
using Drataliornus = CalamityModClassicPreTrailer.Projectiles.Ranged.Drataliornus;
using HolyLight = CalamityModClassicPreTrailer.Projectiles.Boss.HolyLight;
using Norfleet = CalamityModClassicPreTrailer.Projectiles.Ranged.Norfleet;
using Phangasm = CalamityModClassicPreTrailer.Projectiles.Ranged.Phangasm;
using SlimeGod = CalamityModClassicPreTrailer.Buffs.SlimeGod;
using StarfleetMK2 = CalamityModClassicPreTrailer.Projectiles.Ranged.StarfleetMK2;
using TelluricGlare = CalamityModClassicPreTrailer.Projectiles.Ranged.TelluricGlare;
using static Terraria.ModLoader.MusicLoader;

namespace CalamityModClassicPreTrailer
{
	public class CalamityModClassicPreTrailer : Mod
	{
		//Hotkeys
		public static ModKeybind NormalityRelocatorHotKey {get; private set; }
		public static ModKeybind AegisHotKey {get; private set; }
		public static ModKeybind TarraHotKey {get; private set; }
		public static ModKeybind RageHotKey {get; private set; }
		public static ModKeybind AdrenalineHotKey {get; private set; }
		public static ModKeybind AstralTeleportHotKey {get; private set; }
		public static ModKeybind AstralArcanumUIHotkey {get; private set; }
		public static ModKeybind BossBarToggleHotKey {get; private set; }
		public static ModKeybind BossBarToggleSmallTextHotKey {get; private set; }

		//Boss Spawners
		public static int ghostKillCount = 0;
		public static int sharkKillCount = 0;

		//Textures & Shaders
		public static Texture2D heartOriginal2;
		public static Texture2D heartOriginal;
		public static Texture2D rainOriginal;
		public static Texture2D manaOriginal;
		public static Texture2D carpetOriginal;
		public static Texture2D AstralCactusTexture;
		public static Texture2D AstralCactusGlowTexture;
		public static Texture2D AstralSky;
		public static Effect CustomShader;

		//Lists
		public static IList<string> donatorList;
		public static List<int> rangedProjectileExceptionList;
		public static List<int> projectileMinionList;
		public static List<int> enemyImmunityList;
		public static List<int> dungeonEnemyBuffList;
		public static List<int> dungeonProjectileBuffList;
		public static List<int> bossScaleList;
		public static List<int> beeEnemyList;
		public static List<int> beeProjectileList;
		public static List<int> hardModeNerfList;
		public static List<int> debuffList;
		public static List<int> fireWeaponList;
		public static List<int> natureWeaponList;
		public static List<int> alcoholList;
		public static List<int> doubleDamageBuffList; //100% buff
		public static List<int> sixtySixDamageBuffList; //66% buff
		public static List<int> fiftyDamageBuffList; //50% buff
		public static List<int> thirtyThreeDamageBuffList; //33% buff
		public static List<int> twentyFiveDamageBuffList; //25% buff
		public static List<int> twentyDamageBuffList; //20% buff
		public static List<int> weaponAutoreuseList;
		public static List<int> quarterDamageNerfList; //25% nerf
		public static List<int> pumpkinMoonBuffList;
		public static List<int> frostMoonBuffList;
		public static List<int> eclipseBuffList;
		public static List<int> eventProjectileBuffList;
		public static List<int> revengeanceEnemyBuffList;
		public static List<int> revengeanceProjectileBuffList;
		public static List<int> trapProjectileList;
		public static List<int> scopedWeaponList;
		public static List<int> trueMeleeBoostExceptionList;
		public static List<int> skeletonList;

		public static CalamityModClassicPreTrailer Instance;

		public CalamityModClassicPreTrailer()
		{
			Instance = this;
		}

		#region Load
		public override void Load()
		{
			if (!Main.dedServ)
            {
                //Boss Music
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/AquaticScourge"), Find<ModItem>("AquaticScourgeMusicbox").Type, Find<ModTile>("AquaticScourgeMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Astrageldon"), Find<ModItem>("AstrageldonMusicbox").Type, Find<ModTile>("AstrageldonMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/AstrumDeus"), Find<ModItem>("AstrumDeusMusicbox").Type, Find<ModTile>("AstrumDeusMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/LeftAlone"), Find<ModItem>("BrimmyMusicbox").Type, Find<ModTile>("BrimmyMusicbox").Type);
				AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Murderswarm"), Find<ModItem>("BumblebirbMusicbox").Type, Find<ModTile>("BumblebirbMusicbox").Type);
				AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Calamitas"), Find<ModItem>("CalamitasMusicbox").Type, Find<ModTile>("CalamitasMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Void"), Find<ModItem>("CeaselessVoidMusicbox").Type, Find<ModTile>("CeaselessVoidMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Crabulon"), Find<ModItem>("CrabulonMusicbox").Type, Find<ModTile>("CrabulonMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Cryogen"), Find<ModItem>("CryogenMusicbox").Type, Find<ModTile>("CryogenMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/DesertScourge"), Find<ModItem>("DesertScourgeMusicbox").Type, Find<ModTile>("DesertScourgeMusicbox").Type);
				AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/YHARON"), Find<ModItem>("Yharon1Musicbox").Type, Find<ModTile>("Yharon1Musicbox").Type);
				AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/YHARONREBIRTH"), Find<ModItem>("Yharon2Musicbox").Type, Find<ModTile>("Yharon2Musicbox").Type);
				AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/DragonGod"), Find<ModItem>("Yharon3Musicbox").Type, Find<ModTile>("Yharon3Musicbox").Type);
				AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/RUIN"), Find<ModItem>("PolterghastMusicbox").Type, Find<ModTile>("PolterghastMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Guardians"), Find<ModItem>("ProfanedGuardianMusicbox").Type, Find<ModTile>("ProfanedGuardianMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/ProvidenceTheme"), Find<ModItem>("ProvidenceMusicbox").Type, Find<ModTile>("ProvidenceMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Ravager"), Find<ModItem>("RavagerMusicbox").Type, Find<ModTile>("RavagerMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Signus"), Find<ModItem>("SignusMusicbox").Type, Find<ModTile>("SignusMusicbox").Type);
				AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/SirenLure"), Find<ModItem>("SirenIdleMusicbox").Type, Find<ModTile>("SirenIdleMusicbox").Type);
				AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Siren"), Find<ModItem>("SirenMusicbox").Type, Find<ModTile>("SirenMusicbox").Type);
				AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/LeviathanAndSiren"), Find<ModItem>("LeviathanMusicbox").Type, Find<ModTile>("LeviathanMusicbox").Type);
				AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Weaver"), Find<ModItem>("StormWeaverMusicbox").Type, Find<ModTile>("StormWeaverMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/SCG"), Find<ModItem>("SCalGMusicbox").Type, Find<ModTile>("SCalGMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/SCL"), Find<ModItem>("SCalLMusicbox").Type, Find<ModTile>("SCalLMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/SCE"), Find<ModItem>("SCalEMusicbox").Type, Find<ModTile>("SCalEMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/SCA"), Find<ModItem>("SCalAMusicbox").Type, Find<ModTile>("SCalAMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/ScourgeofTheUniverse"), Find<ModItem>("DoGMusicbox").Type, Find<ModTile>("DoGMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/UniversalCollapse"), Find<ModItem>("DoGP2Musicbox").Type, Find<ModTile>("DoGP2Musicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/HiveMind"), Find<ModItem>("HiveMindMusicbox").Type, Find<ModTile>("HiveMindMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/BloodCoagulant"), Find<ModItem>("PerforatorMusicbox").Type, Find<ModTile>("PerforatorMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/PlaguebringerGoliath"), Find<ModItem>("PlaguebringerMusicbox").Type, Find<ModTile>("PlaguebringerMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/SlimeGod"), Find<ModItem>("SlimeGodMusicbox").Type, Find<ModTile>("SlimeGodMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Doomsayer"), Find<ModItem>("CultistMusicbox").Type, Find<ModTile>("CultistMusicbox").Type);

				//Biome Music
				AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Sulphur"), Find<ModItem>("SulphurousMusicbox").Type, Find<ModTile>("SulphurousMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/TheAbyss"), Find<ModItem>("HigherAbyssMusicbox").Type, Find<ModTile>("HigherAbyssMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/TheDeepAbyss"), Find<ModItem>("AbyssLowerMusicbox").Type, Find<ModTile>("AbyssLowerMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/TheVoid"), Find<ModItem>("VoidMusicbox").Type, Find<ModTile>("VoidMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Astral"), Find<ModItem>("AstralMusicbox").Type, Find<ModTile>("AstralMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Calamity"), Find<ModItem>("CalamityMusicbox").Type, Find<ModTile>("CalamityMusicbox").Type);
                AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/Crag"), Find<ModItem>("CragMusicbox").Type, Find<ModTile>("CragMusicbox").Type);
				AddMusicBox(this, GetMusicSlot(this.Name + "/" +"Sounds/Music/SunkenSea"), Find<ModItem>("SunkenSeaMusicbox").Type, Find<ModTile>("SunkenSeaMusicbox").Type);
            }

			if (!Main.dedServ)
			{
				Filters.Scene["CalamityModClassicPreTrailer:DevourerofGodsHead"] = new Filter(new DoGScreenShaderData("FilterMiniTower").UseColor(0.4f, 0.1f, 1f).UseOpacity(0.5f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassicPreTrailer:DevourerofGodsHead"] = new DoGSky();
				Filters.Scene["CalamityModClassicPreTrailer:DevourerofGodsHeadS"] = new Filter(new DoGScreenShaderDataS("FilterMiniTower").UseColor(0.4f, 0.1f, 1f).UseOpacity(0.5f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassicPreTrailer:DevourerofGodsHeadS"] = new DoGSkyS();
				Filters.Scene["CalamityModClassicPreTrailer:CalamitasRun3"] = new Filter(new CalScreenShaderData("FilterMiniTower").UseColor(1.1f, 0.3f, 0.3f).UseOpacity(0.6f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassicPreTrailer:CalamitasRun3"] = new CalSky();
				Filters.Scene["CalamityModClassicPreTrailer:PlaguebringerGoliath"] = new Filter(new PbGScreenShaderData("FilterMiniTower").UseColor(0.3f, 0.9f, 0.2f).UseOpacity(0.65f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassicPreTrailer:PlaguebringerGoliath"] = new PbGSky();
				Filters.Scene["CalamityModClassicPreTrailer:Yharon"] = new Filter(new YScreenShaderData("FilterMiniTower").UseColor(1f, 0.4f, 0f).UseOpacity(0.75f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassicPreTrailer:Yharon"] = new YSky();
				Filters.Scene["CalamityModClassicPreTrailer:Leviathan"] = new Filter(new LevScreenShaderData("FilterMiniTower").UseColor(0f, 0f, 0.5f).UseOpacity(0.5f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassicPreTrailer:Leviathan"] = new LevSky();
				Filters.Scene["CalamityModClassicPreTrailer:Providence"] = new Filter(new ProvScreenShaderData("FilterMiniTower").UseColor(0.6f, 0.45f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassicPreTrailer:Providence"] = new ProvSky();
				Filters.Scene["CalamityModClassicPreTrailer:SupremeCalamitas"] = new Filter(new SCalScreenShaderData("FilterMiniTower").UseColor(1.1f, 0.3f, 0.3f).UseOpacity(0.65f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassicPreTrailer:SupremeCalamitas"] = new SCalSky();
				Filters.Scene["CalamityModClassicPreTrailer:Astral"] = new Filter(new AstralScreenShaderData(new Ref<Effect>(CustomShader), "AstralPass").UseColor(0.18f, 0.08f, 0.24f), EffectPriority.VeryHigh);
				SkyManager.Instance["CalamityModClassicPreTrailer:Astral"] = new AstralSky();
			}

			NormalityRelocatorHotKey = KeybindLoader.RegisterKeybind(this, "Normality Relocator", "Z");
			RageHotKey = KeybindLoader.RegisterKeybind(this,"Rage Mode", "V");
			AdrenalineHotKey = KeybindLoader.RegisterKeybind(this,"Adrenaline Mode", "B");
			AegisHotKey = KeybindLoader.RegisterKeybind(this,"Elysian Guard", "N");
			TarraHotKey = KeybindLoader.RegisterKeybind(this,"Armor Set Bonus", "Y");
			AstralTeleportHotKey = KeybindLoader.RegisterKeybind(this,"Astral Teleport", "P");
			AstralArcanumUIHotkey = KeybindLoader.RegisterKeybind(this,"Astral Arcanum UI Toggle", "O");
			BossBarToggleHotKey = KeybindLoader.RegisterKeybind(this,"Boss Health Bar Toggle", "NumPad0");
			BossBarToggleSmallTextHotKey = KeybindLoader.RegisterKeybind(this,"Boss Health Bar Small Text Toggle", "NumPad1");

			if (!Main.dedServ)
			{
				LoadClient();
			}

			BossHealthBarManager.Load(this);

			Config.Load();

			SetupLists();
		}

		private void LoadClient()
		{
			heartOriginal2 = TextureAssets.Heart.Value;
			heartOriginal = TextureAssets.Heart2.Value;
			rainOriginal = TextureAssets.Rain.Value;
			manaOriginal = TextureAssets.Mana.Value;
			carpetOriginal = TextureAssets.FlyingCarpet.Value;
			AstralCactusGlowTexture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/Tiles/AstralCactusGlow").Value;
			AstralSky = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/AstralSky", AssetRequestMode.ImmediateLoad).Value;
			CustomShader = ModContent.Request<Effect>("CalamityModClassicPreTrailer/Effects/CustomShader", AssetRequestMode.ImmediateLoad).Value;
			
			Filters.Scene["CalamityModClassicPreTrailer:DevourerofGodsHead"] = new Filter(new DoGScreenShaderData("FilterMiniTower").UseColor(0.4f, 0.1f, 1.0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
			SkyManager.Instance["CalamityModClassicPreTrailer:DevourerofGodsHead"] = new DoGSky();

			Filters.Scene["CalamityModClassicPreTrailer:DevourerofGodsHeadS"] = new Filter(new DoGScreenShaderDataS("FilterMiniTower").UseColor(0.4f, 0.1f, 1.0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
			SkyManager.Instance["CalamityModClassicPreTrailer:DevourerofGodsHeadS"] = new DoGSkyS();

			Filters.Scene["CalamityModClassicPreTrailer:CalamitasRun3"] = new Filter(new CalScreenShaderData("FilterMiniTower").UseColor(1.1f, 0.3f, 0.3f).UseOpacity(0.6f), EffectPriority.VeryHigh);
			SkyManager.Instance["CalamityModClassicPreTrailer:CalamitasRun3"] = new CalSky();

			Filters.Scene["CalamityModClassicPreTrailer:PlaguebringerGoliath"] = new Filter(new PbGScreenShaderData("FilterMiniTower").UseColor(0.2f, 0.6f, 0.2f).UseOpacity(0.65f), EffectPriority.VeryHigh);
			SkyManager.Instance["CalamityModClassicPreTrailer:PlaguebringerGoliath"] = new PbGSky();

			Filters.Scene["CalamityModClassicPreTrailer:Yharon"] = new Filter(new YScreenShaderData("FilterMiniTower").UseColor(1f, 0.4f, 0f).UseOpacity(0.75f), EffectPriority.VeryHigh);
			SkyManager.Instance["CalamityModClassicPreTrailer:Yharon"] = new YSky();

			Filters.Scene["CalamityModClassicPreTrailer:Leviathan"] = new Filter(new LevScreenShaderData("FilterMiniTower").UseColor(0f, 0f, 0.5f).UseOpacity(0.5f), EffectPriority.VeryHigh);
			SkyManager.Instance["CalamityModClassicPreTrailer:Leviathan"] = new LevSky();

			Filters.Scene["CalamityModClassicPreTrailer:Providence"] = new Filter(new ProvScreenShaderData("FilterMiniTower").UseColor(0.6f, 0.45f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
			SkyManager.Instance["CalamityModClassicPreTrailer:Providence"] = new ProvSky();

			Filters.Scene["CalamityModClassicPreTrailer:SupremeCalamitas"] = new Filter(new SCalScreenShaderData("FilterMiniTower").UseColor(1.1f, 0.3f, 0.3f).UseOpacity(0.65f), EffectPriority.VeryHigh);
			SkyManager.Instance["CalamityModClassicPreTrailer:SupremeCalamitas"] = new SCalSky();

			Filters.Scene["CalamityModClassicPreTrailer:Astral"] = new Filter(new AstralScreenShaderData(new Ref<Effect>(CustomShader), "AstralPass").UseColor(0.18f, 0.08f, 0.24f), EffectPriority.VeryHigh);
			SkyManager.Instance["CalamityModClassicPreTrailer:Astral"] = new AstralSky();

			Mod mod = ModLoader.GetMod("CalamityModClassicPreTrailer");
			UIHandler.OnLoad(mod);

			AstralArcanumUI.Load(this);
		}
		#endregion

		#region Unload
		public override void Unload()
		{
			NormalityRelocatorHotKey = null;
			RageHotKey = null;
			AdrenalineHotKey = null;
			AegisHotKey = null;
			TarraHotKey = null;
			AstralTeleportHotKey = null;
			AstralArcanumUIHotkey = null;
			BossBarToggleHotKey = null;
			BossBarToggleSmallTextHotKey = null;

			AstralCactusTexture = null;
			AstralCactusGlowTexture = null;
			AstralSky = null;

			donatorList = null;

			rangedProjectileExceptionList = null;
			projectileMinionList = null;
			enemyImmunityList = null;
			dungeonEnemyBuffList = null;
			dungeonProjectileBuffList = null;
			bossScaleList = null;
			beeEnemyList = null;
			beeProjectileList = null;
			hardModeNerfList = null;
			debuffList = null;
			fireWeaponList = null;
			natureWeaponList = null;
			alcoholList = null;
			doubleDamageBuffList = null;
			sixtySixDamageBuffList = null;
			fiftyDamageBuffList = null;
			thirtyThreeDamageBuffList = null;
			twentyFiveDamageBuffList = null;
			weaponAutoreuseList = null;
			quarterDamageNerfList = null;
			pumpkinMoonBuffList = null;
			frostMoonBuffList = null;
			eclipseBuffList = null;
			eventProjectileBuffList = null;
			revengeanceEnemyBuffList = null;
			revengeanceProjectileBuffList = null;
			trapProjectileList = null;
			scopedWeaponList = null;
			trueMeleeBoostExceptionList = null;
			skeletonList = null;

			BossHealthBarManager.Unload();
			base.Unload();

			AstralArcanumUI.Unload();
			base.Unload();

			heartOriginal2 = null;
			heartOriginal = null;
			rainOriginal = null;
			manaOriginal = null;
			carpetOriginal = null;

			Instance = null;
		}
		#endregion

		#region SetupLists
		public static void SetupLists()
		{
			Mod calamity = ModLoader.GetMod("CalamityModClassicPreTrailer");
			if (calamity != null)
			{
				donatorList = new List<string>()
				{
					"Vorbis",
					"SoloMael",
					"Chaotic Reks",
					"The Buildmonger",
					"Yuh",
					"Littlepiggy",
					"LompL",
					"Lilith Saintclaire",
					"Ben Shapiro",
					"Frederik Henschel",
					"Faye",
					"Gibb50",
					"Braden Hajer",
					"Hannes Holmlund",
					"profoundmango69",
					"Jack M Sargent",
					"Hans Volter",
					"Krankwagon",
					"MishiroUsui",
					"pixlgray",
					"Arkhine",
					"Lodude",
					"DevAesthetic",
					"Mister Winchester",
					"Zacky",
					"Veine",
					"Javyz",
					"Shifter",
					"Crysthamyr",
					"Elfinlocks",
					"Ein",
					"2Larry2",
					"Jenonen",
					"Dodu",
					"Arti",
					"Tervastator",
					"Luis Arguello",
					"Alexander Davis",
					"BakaQing",
					"Laura Coonrod",
					"Xaphlactus",
					"MajinBagel",
					"Bendy",
					"Rando Calrissian",
					"Tails the Fox 92",
					"Bread",
					"Minty Candy",
					"Preston Card",
					"MovingTarget_086",
					"Shiro",
					"Chip",
					"Taylor Riverpaw",
					"ShotgunAngel",
					"Sandblast",
					"ThomasThePencil",
					"Aero (Aero#4599)",
					"GlitchOut",
					"Daawnz",
					"CrabBar",
					"Yatagarasu",
					"Jarod Isaac Gordon",
					"Zombieh",
					"MingWhy",
					"Random Weeb"
				};

				rangedProjectileExceptionList = new List<int>()
				{
					ProjectileID.Phantasm,
					ProjectileID.VortexBeater,
					ProjectileID.DD2PhoenixBow,
					ProjectileID.IchorDart,
					ProjectileID.PhantasmArrow,
					ModContent.ProjectileType<Phangasm>(),
					ModContent.ProjectileType<Contagion>(),
					ModContent.ProjectileType<DaemonsFlame>(),
					ModContent.ProjectileType<ExoTornado>(),
					ModContent.ProjectileType<Drataliornus>(),
					ModContent.ProjectileType<FlakKrakenGun>(),
					ModContent.ProjectileType<Butcher>(),
					ModContent.ProjectileType<StarfleetMK2>(),
					ModContent.ProjectileType<TerraBulletSplit>(),
					ModContent.ProjectileType<TerraArrow2>(),
					ModContent.ProjectileType<OMGWTH>(),
					ModContent.ProjectileType<Norfleet>(),
					ModContent.ProjectileType<NorfleetComet>(),
					ModContent.ProjectileType<NorfleetExplosion>()
				};

				projectileMinionList = new List<int>()
				{
					ProjectileID.PygmySpear,
					ProjectileID.UFOMinion,
					ProjectileID.UFOLaser,
					ProjectileID.StardustCellMinionShot,
					ProjectileID.MiniSharkron,
					ProjectileID.MiniRetinaLaser,
					ProjectileID.ImpFireball,
					ProjectileID.HornetStinger,
					ProjectileID.DD2FlameBurstTowerT1Shot,
					ProjectileID.DD2FlameBurstTowerT2Shot,
					ProjectileID.DD2FlameBurstTowerT3Shot,
					ProjectileID.DD2BallistraProj,
					ProjectileID.DD2ExplosiveTrapT1Explosion,
					ProjectileID.DD2ExplosiveTrapT2Explosion,
					ProjectileID.DD2ExplosiveTrapT3Explosion,
					ProjectileID.SpiderEgg,
					ProjectileID.BabySpider,
					ProjectileID.FrostBlastFriendly,
					ProjectileID.MoonlordTurretLaser,
					ProjectileID.RainbowCrystalExplosion
				};

				enemyImmunityList = new List<int>()
				{
					NPCID.KingSlime,
					NPCID.EaterofWorldsHead,
					NPCID.EaterofWorldsBody,
					NPCID.EaterofWorldsTail,
					NPCID.BrainofCthulhu,
					NPCID.Creeper,
					NPCID.EyeofCthulhu,
					NPCID.QueenBee,
					NPCID.SkeletronHead,
					NPCID.SkeletronHand,
					NPCID.WallofFlesh,
					NPCID.WallofFleshEye,
					NPCID.Retinazer,
					NPCID.Spazmatism,
					NPCID.SkeletronPrime,
					NPCID.PrimeCannon,
					NPCID.PrimeSaw,
					NPCID.PrimeLaser,
					NPCID.PrimeVice,
					NPCID.Plantera,
					NPCID.IceQueen,
					NPCID.Pumpking,
					NPCID.Mothron,
					NPCID.Golem,
					NPCID.GolemHead,
					NPCID.GolemFistRight,
					NPCID.GolemFistLeft,
					NPCID.DukeFishron,
					NPCID.CultistBoss,
					NPCID.MoonLordHead,
					NPCID.MoonLordHand,
					NPCID.MoonLordCore,
					NPCID.MoonLordFreeEye,
					NPCID.DD2Betsy
				};

				dungeonEnemyBuffList = new List<int>()
				{
					NPCID.SkeletonSniper,
					NPCID.TacticalSkeleton,
					NPCID.SkeletonCommando,
					NPCID.Paladin,
					NPCID.GiantCursedSkull,
					NPCID.BoneLee,
					NPCID.DiabolistWhite,
					NPCID.DiabolistRed,
					NPCID.NecromancerArmored,
					NPCID.Necromancer,
					NPCID.RaggedCasterOpenCoat,
					NPCID.RaggedCaster,
					NPCID.HellArmoredBonesSword,
					NPCID.HellArmoredBonesMace,
					NPCID.HellArmoredBonesSpikeShield,
					NPCID.HellArmoredBones,
					NPCID.BlueArmoredBonesSword,
					NPCID.BlueArmoredBonesNoPants,
					NPCID.BlueArmoredBonesMace,
					NPCID.BlueArmoredBones,
					NPCID.RustyArmoredBonesSwordNoArmor,
					NPCID.RustyArmoredBonesSword,
					NPCID.RustyArmoredBonesFlail,
					NPCID.RustyArmoredBonesAxe
				};

				dungeonProjectileBuffList = new List<int>()
				{
					ProjectileID.PaladinsHammerHostile,
					ProjectileID.ShadowBeamHostile,
					ProjectileID.InfernoHostileBolt,
					ProjectileID.InfernoHostileBlast,
					ProjectileID.LostSoulHostile,
					ProjectileID.SniperBullet,
					ProjectileID.RocketSkeleton,
					ProjectileID.BulletDeadeye,
					ProjectileID.Shadowflames
				};

				bossScaleList = new List<int>()
				{
					NPCID.EaterofWorldsHead,
					NPCID.EaterofWorldsBody,
					NPCID.EaterofWorldsTail,
					NPCID.Creeper,
					NPCID.SkeletronHand,
					NPCID.WallofFleshEye,
					NPCID.TheHungry,
					NPCID.TheHungryII,
					NPCID.TheDestroyerBody,
					NPCID.TheDestroyerTail,
					NPCID.PrimeCannon,
					NPCID.PrimeVice,
					NPCID.PrimeSaw,
					NPCID.PrimeLaser,
					NPCID.PlanterasTentacle,
					NPCID.Pumpking,
					NPCID.IceQueen,
					NPCID.Mothron,
					NPCID.GolemHead
				};

				beeEnemyList = new List<int>()
				{
					NPCID.GiantMossHornet,
					NPCID.BigMossHornet,
					NPCID.LittleMossHornet,
					NPCID.TinyMossHornet,
					NPCID.MossHornet,
					NPCID.VortexHornetQueen,
					NPCID.VortexHornet,
					NPCID.Bee,
					NPCID.BeeSmall,
					NPCID.QueenBee,
					ModContent.NPCType<PlaguebringerGoliath>(),
					ModContent.NPCType<PlaguebringerShade>(),
					ModContent.NPCType<PlagueBeeLargeG>(),
					ModContent.NPCType<PlagueBeeLarge>(),
					ModContent.NPCType<PlagueBeeG>(),
					ModContent.NPCType<PlagueBee>()
				};

				beeProjectileList = new List<int>()
				{
					ProjectileID.Stinger,
					ProjectileID.HornetStinger,
					ModContent.ProjectileType<PlagueStingerGoliath>(),
					ModContent.ProjectileType<PlagueStingerGoliathV2>(),
					ModContent.ProjectileType<PlagueExplosion>()
				};

				hardModeNerfList = new List<int>()
				{
					ProjectileID.WebSpit,
					ProjectileID.PinkLaser,
					ProjectileID.FrostBlastHostile,
					ProjectileID.RuneBlast,
					ProjectileID.GoldenShowerHostile,
					ProjectileID.RainNimbus,
					ProjectileID.Stinger,
					ProjectileID.FlamingArrow,
					ProjectileID.BulletDeadeye,
					ProjectileID.CannonballHostile
				};

				debuffList = new List<int>()
				{
					BuffID.Poisoned,
					BuffID.Darkness,
					BuffID.Cursed,
					BuffID.OnFire,
					BuffID.Bleeding,
					BuffID.Confused,
					BuffID.Slow,
					BuffID.Weak,
					BuffID.Silenced,
					BuffID.BrokenArmor,
					BuffID.CursedInferno,
					BuffID.Frostburn,
					BuffID.Chilled,
					BuffID.Frozen,
					BuffID.Burning,
					BuffID.Suffocation,
					BuffID.Ichor,
					BuffID.Venom,
					BuffID.Blackout,
					BuffID.Electrified,
					BuffID.Rabies,
					BuffID.Webbed,
					BuffID.Stoned,
					BuffID.Dazed,
					BuffID.VortexDebuff,
					BuffID.WitheredArmor,
					BuffID.WitheredWeapon,
					BuffID.OgreSpit,
					BuffID.BetsysCurse,
					ModContent.BuffType<BrimstoneFlames>(),
					ModContent.BuffType<BurningBlood>(),
					ModContent.BuffType<GlacialState>(),
					ModContent.BuffType<GodSlayerInferno>(),
					ModContent.BuffType<Buffs.HolyLight>(),
					ModContent.BuffType<Irradiated>(),
					ModContent.BuffType<Plague>(),
					ModContent.BuffType<AbyssalFlames>(),
					ModContent.BuffType<CrushDepth>(),
					ModContent.BuffType<Horror>(),
					ModContent.BuffType<MarkedforDeath>()
				};

				fireWeaponList = new List<int>()
				{
					ItemID.FieryGreatsword,
					ItemID.DD2SquireDemonSword,
					ItemID.TheHorsemansBlade,
					ItemID.DD2SquireBetsySword,
					ItemID.Cascade,
					ItemID.HelFire,
					ItemID.MonkStaffT2,
					ItemID.Flamarang,
					ItemID.MoltenFury,
					ItemID.Sunfury,
					ItemID.PhoenixBlaster,
					ItemID.Flamelash,
					ItemID.SolarEruption,
					ItemID.DayBreak,
					ItemID.MonkStaffT3,
					ItemID.HellwingBow,
					ItemID.DD2PhoenixBow,
					ItemID.DD2BetsyBow,
					ItemID.FlareGun,
					ItemID.Flamethrower,
					ItemID.ElfMelter,
					ItemID.FlowerofFire,
					ItemID.MeteorStaff,
					ItemID.ApprenticeStaffT3,
					ItemID.InfernoFork,
					ItemID.HeatRay,
					ItemID.BookofSkulls,
					ItemID.ImpStaff,
					ItemID.DD2FlameburstTowerT1Popper,
					ItemID.DD2FlameburstTowerT2Popper,
					ItemID.DD2FlameburstTowerT3Popper,
					ItemID.MolotovCocktail,
					ModContent.ItemType<AegisBlade>(),
					ModContent.ItemType<BalefulHarvester>(),
					ModContent.ItemType<Chaotrix>(),
					ModContent.ItemType<CometQuasher>(),
					ModContent.ItemType<DraconicDestruction>(),
					ModContent.ItemType<Items.Weapons.Drataliornus>(),
					ModContent.ItemType<EnergyStaff>(),
					ModContent.ItemType<ExsanguinationLance>(),
					ModContent.ItemType<FirestormCannon>(),
					ModContent.ItemType<FlameburstShortsword>(),
					ModContent.ItemType<FlameScythe>(),
					ModContent.ItemType<FlameScytheMelee>(),
					ModContent.ItemType<FlareBolt>(),
					ModContent.ItemType<FlarefrostBlade>(),
					ModContent.ItemType<FlarewingBow>(),
					ModContent.ItemType<ForbiddenSun>(),
					ModContent.ItemType<FrigidflashBolt>(),
					ModContent.ItemType<GreatbowofTurmoil>(),
					ModContent.ItemType<HarvestStaff>(),
					ModContent.ItemType<HellBurst>(),
					ModContent.ItemType<HellfireFlamberge>(),
					ModContent.ItemType<Hellkite>(),
					ModContent.ItemType<HellwingStaff>(),
					ModContent.ItemType<Helstorm>(),
					ModContent.ItemType<InfernaCutter>(),
					ModContent.ItemType<Lazhar>(),
					ModContent.ItemType<MeteorFist>(),
					ModContent.ItemType<Mourningstar>(),
					ModContent.ItemType<PhoenixBlade>(),
					ModContent.ItemType<Photoviscerator>(),
					ModContent.ItemType<RedSun>(),
					ModContent.ItemType<SpectralstormCannon>(),
					ModContent.ItemType<SunGodStaff>(),
					ModContent.ItemType<SunSpiritStaff>(),
					ModContent.ItemType<TearsofHeaven>(),
					ModContent.ItemType<TerraFlameburster>(),
					ModContent.ItemType<TheEmpyrean>(),
					ModContent.ItemType<TheWand>(),
					ModContent.ItemType<VenusianTrident>(),
					ModContent.ItemType<Vesuvius>(),
					ModContent.ItemType<BlissfulBombardier>(),
					ModContent.ItemType<HolyCollider>(),
					ModContent.ItemType<MoltenAmputator>(),
					ModContent.ItemType<PurgeGuzzler>(),
					ModContent.ItemType<SolarFlare>(),
					ModContent.ItemType<Items.Weapons.Providence.TelluricGlare>(),
					ModContent.ItemType<AngryChickenStaff>(),
					ModContent.ItemType<ChickenCannon>(),
					ModContent.ItemType<DragonRage>(),
					ModContent.ItemType<DragonsBreath>(),
					ModContent.ItemType<PhoenixFlameBarrage>(),
					ModContent.ItemType<ProfanedTrident>(),
					ModContent.ItemType<TheBurningSky>()
				};

				natureWeaponList = new List<int>()
				{
					ItemID.BladeofGrass,
					ItemID.ChlorophyteClaymore,
					ItemID.ChlorophyteSaber,
					ItemID.ChlorophytePartisan,
					ItemID.ChlorophyteShotbow,
					ItemID.Seedler,
					ItemID.ChristmasTreeSword,
					ItemID.TerraBlade,
					ItemID.JungleYoyo,
					ItemID.Yelets,
					ItemID.MushroomSpear,
					ItemID.ThornChakram,
					ItemID.Bananarang,
					ItemID.FlowerPow,
					ItemID.BeesKnees,
					ItemID.Toxikarp,
					ItemID.Bladetongue,
					ItemID.PoisonStaff,
					ItemID.VenomStaff,
					ItemID.StaffofEarth,
					ItemID.BeeGun,
					ItemID.LeafBlower,
					ItemID.WaspGun,
					ItemID.CrystalSerpent,
					ItemID.Razorpine,
					ItemID.HornetStaff,
					ItemID.QueenSpiderStaff,
					ItemID.SlimeStaff,
					ItemID.PygmyStaff,
					ItemID.RavenStaff,
					ItemID.BatScepter,
					ItemID.SpiderStaff,
					ItemID.Beenade,
					ItemID.FrostDaggerfish,
					ModContent.ItemType<DepthBlade>(),
					ModContent.ItemType<AbyssBlade>(),
					ModContent.ItemType<NeptunesBounty>(),
					ModContent.ItemType<AquaticDissolution>(),
					ModContent.ItemType<ArchAmaryllis>(),
					ModContent.ItemType<BiomeBlade>(),
					ModContent.ItemType<TrueBiomeBlade>(),
					ModContent.ItemType<OmegaBiomeBlade>(),
					ModContent.ItemType<BladedgeGreatbow>(),
					ModContent.ItemType<BlossomFlux>(),
					ModContent.ItemType<EvergladeSpray>(),
					ModContent.ItemType<FeralthornClaymore>(),
					ModContent.ItemType<Floodtide>(),
					ModContent.ItemType<FourSeasonsGalaxia>(),
					ModContent.ItemType<GammaFusillade>(),
					ModContent.ItemType<GleamingMagnolia>(),
					ModContent.ItemType<HarvestStaff>(),
					ModContent.ItemType<HellionFlowerSpear>(),
					ModContent.ItemType<Lazhar>(),
					ModContent.ItemType<LifefruitScythe>(),
					ModContent.ItemType<ManaRose>(),
					ModContent.ItemType<MangroveChakram>(),
					ModContent.ItemType<MangroveChakramMelee>(),
					ModContent.ItemType<MantisClaws>(),
					ModContent.ItemType<Mariana>(),
					ModContent.ItemType<Mistlestorm>(),
					ModContent.ItemType<Monsoon>(),
					ModContent.ItemType<Alluvion>(),
					ModContent.ItemType<Items.Weapons.Needler>(),
					ModContent.ItemType<NettlelineGreatbow>(),
					ModContent.ItemType<Quagmire>(),
					ModContent.ItemType<Shroomer>(),
					ModContent.ItemType<SolsticeClaymore>(),
					ModContent.ItemType<SporeKnife>(),
					ModContent.ItemType<Items.Weapons.Spyker>(),
					ModContent.ItemType<StormSaber>(),
					ModContent.ItemType<StormRuler>(),
					ModContent.ItemType<Items.Weapons.StormSurge>(),
					ModContent.ItemType<TarragonThrowingDart>(),
					ModContent.ItemType<TerraEdge>(),
					ModContent.ItemType<TerraLance>(),
					ModContent.ItemType<TerraRay>(),
					ModContent.ItemType<TerraShiv>(),
					ModContent.ItemType<Terratomere>(),
					ModContent.ItemType<TerraFlameburster>(),
					ModContent.ItemType<TheSwarmer>(),
					ModContent.ItemType<Verdant>(),
					ModContent.ItemType<Barinautical>(),
					ModContent.ItemType<DeepseaStaff>(),
					ModContent.ItemType<Downpour>(),
					ModContent.ItemType<SubmarineShocker>(),
					ModContent.ItemType<Items.Weapons.AbyssWeapons.Archerfish>(),
					ModContent.ItemType<BallOFugu>(),
					ModContent.ItemType<BlackAnurian>(),
					ModContent.ItemType<CalamarisLament>(),
					ModContent.ItemType<HerringStaff>(),
					ModContent.ItemType<Lionfish>()
				};

				alcoholList = new List<int>()
				{
					ModContent.BuffType<BloodyMary>(),
					ModContent.BuffType<CaribbeanRum>(),
					ModContent.BuffType<CinnamonRoll>(),
					ModContent.BuffType<Everclear>(),
					ModContent.BuffType<EvergreenGin>(),
					ModContent.BuffType<Fireball>(),
					ModContent.BuffType<GrapeBeer>(),
					ModContent.BuffType<Margarita>(),
					ModContent.BuffType<Moonshine>(),
					ModContent.BuffType<MoscowMule>(),
					ModContent.BuffType<RedWine>(),
					ModContent.BuffType<Rum>(),
					ModContent.BuffType<Screwdriver>(),
					ModContent.BuffType<StarBeamRye>(),
					ModContent.BuffType<Tequila>(),
					ModContent.BuffType<TequilaSunrise>(),
					ModContent.BuffType<Vodka>(),
					ModContent.BuffType<Whiskey>(),
					ModContent.BuffType<WhiteWine>()
				};

				doubleDamageBuffList = new List<int>()
				{
					ItemID.BallOHurt,
					ItemID.TheMeatball,
					ItemID.BlueMoon,
					ItemID.Sunfury,
					ItemID.DaoofPow,
					ItemID.FlowerPow,
					ItemID.Anchor,
					ItemID.KOCannon,
					ItemID.GolemFist,
					ItemID.BreakerBlade,
					ItemID.SpectreStaff,
					ItemID.MonkStaffT2,
					ItemID.ProximityMineLauncher,
					ItemID.FireworksLauncher
				};

				sixtySixDamageBuffList = new List<int>()
				{
					ItemID.TrueNightsEdge,
					ItemID.WandofSparking,
					ItemID.MedusaHead,
					ItemID.StaffofEarth,
					ItemID.ChristmasTreeSword,
					ItemID.MonkStaffT1,
					ItemID.InfernoFork,
					ItemID.VenomStaff
				};

				fiftyDamageBuffList = new List<int>()
				{
					ItemID.NightsEdge,
					ItemID.ElfMelter,
					ItemID.Flamethrower,
					ItemID.MoonlordTurretStaff,
					ItemID.WaspGun,
					ItemID.Keybrand,
					ItemID.PulseBow,
					ItemID.PaladinsHammer
				};

				thirtyThreeDamageBuffList = new List<int>()
				{
					ItemID.TerraBlade,
					ItemID.CrystalVileShard,
					ItemID.SoulDrain,
					ItemID.ClingerStaff,
					ItemID.ChargedBlasterCannon,
					ItemID.NettleBurst,
					ItemID.Excalibur,
					ItemID.AmberStaff,
					ItemID.BluePhasesaber,
					ItemID.RedPhasesaber,
					ItemID.GreenPhasesaber,
					ItemID.WhitePhasesaber,
					ItemID.YellowPhasesaber,
					ItemID.PurplePhasesaber,
					ItemID.TheRottedFork,
					ItemID.VampireKnives,
					ItemID.Cascade
				};

				twentyFiveDamageBuffList = new List<int>()
				{
					ItemID.Muramasa,
					ItemID.StakeLauncher
				};

				twentyDamageBuffList = new List<int>()
				{
					ItemID.ChainGuillotines,
					ItemID.FlowerofFrost,
					ItemID.PoisonStaff,
					ItemID.Gungnir,
					ItemID.BookStaff
				};

				weaponAutoreuseList = new List<int>()
				{
					ItemID.NightsEdge,
					ItemID.TrueNightsEdge,
					ItemID.TrueExcalibur,
					ItemID.PhoenixBlaster,
					ItemID.VenusMagnum,
					ItemID.MagicDagger,
					ItemID.BeamSword,
					ItemID.MonkStaffT2,
					ItemID.PaladinsHammer
				};

				quarterDamageNerfList = new List<int>()
				{
					ItemID.DaedalusStormbow,
					ItemID.PhoenixBlaster,
					ItemID.VenusMagnum,
					ItemID.BlizzardStaff
				};

				pumpkinMoonBuffList = new List<int>()
				{
					NPCID.Scarecrow1,
					NPCID.Scarecrow2,
					NPCID.Scarecrow3,
					NPCID.Scarecrow4,
					NPCID.Scarecrow5,
					NPCID.Scarecrow6,
					NPCID.Scarecrow7,
					NPCID.Scarecrow8,
					NPCID.Scarecrow9,
					NPCID.Scarecrow10,
					NPCID.HeadlessHorseman,
					NPCID.MourningWood,
					NPCID.Splinterling,
					NPCID.Pumpking,
					NPCID.PumpkingBlade,
					NPCID.Hellhound,
					NPCID.Poltergeist
				};

				frostMoonBuffList = new List<int>()
				{
					NPCID.ZombieElf,
					NPCID.ZombieElfBeard,
					NPCID.ZombieElfGirl,
					NPCID.PresentMimic,
					NPCID.GingerbreadMan,
					NPCID.Yeti,
					NPCID.Everscream,
					NPCID.IceQueen,
					NPCID.SantaNK1,
					NPCID.ElfCopter,
					NPCID.Nutcracker,
					NPCID.NutcrackerSpinning,
					NPCID.ElfArcher,
					NPCID.Krampus,
					NPCID.Flocko
				};

				eclipseBuffList = new List<int>()
				{
					NPCID.Eyezor,
					NPCID.Reaper,
					NPCID.Frankenstein,
					NPCID.SwampThing,
					NPCID.Vampire,
					NPCID.VampireBat,
					NPCID.Butcher,
					NPCID.CreatureFromTheDeep,
					NPCID.Fritz,
					NPCID.Nailhead,
					NPCID.Psycho,
					NPCID.DeadlySphere,
					NPCID.DrManFly,
					NPCID.ThePossessed,
					NPCID.Mothron,
					NPCID.MothronEgg,
					NPCID.MothronSpawn
				};

				eventProjectileBuffList = new List<int>()
				{
					ProjectileID.FlamingWood,
					ProjectileID.GreekFire1,
					ProjectileID.GreekFire2,
					ProjectileID.GreekFire3,
					ProjectileID.FlamingScythe,
					ProjectileID.FlamingArrow,
					ProjectileID.PineNeedleHostile,
					ProjectileID.OrnamentHostile,
					ProjectileID.OrnamentHostileShrapnel,
					ProjectileID.FrostWave,
					ProjectileID.FrostShard,
					ProjectileID.Missile,
					ProjectileID.Present,
					ProjectileID.Spike,
					ProjectileID.BulletDeadeye,
					ProjectileID.EyeLaser,
					ProjectileID.Nail,
					ProjectileID.DrManFlyFlask
				};

				revengeanceEnemyBuffList = new List<int>()
				{
					NPCID.ServantofCthulhu,
					NPCID.EyeofCthulhu,
					NPCID.EaterofWorldsHead,
					NPCID.DevourerHead,
					NPCID.GiantWormHead,
					NPCID.MeteorHead,
					NPCID.SkeletronHead,
					NPCID.SkeletronHand,
					NPCID.BoneSerpentHead,
					NPCID.ManEater,
					NPCID.KingSlime,
					NPCID.Snatcher,
					NPCID.Piranha,
					NPCID.Shark,
					NPCID.SpikeBall,
					NPCID.BlazingWheel,
					NPCID.Mimic,
					NPCID.WyvernHead,
					NPCID.DiggerHead,
					NPCID.SeekerHead,
					NPCID.AnglerFish,
					NPCID.Werewolf,
					NPCID.Wraith,
					NPCID.WallofFlesh,
					NPCID.TheHungry,
					NPCID.TheHungryII,
					NPCID.LeechHead,
					NPCID.Spazmatism,
					NPCID.Retinazer,
					NPCID.SkeletronPrime,
					NPCID.PrimeSaw,
					NPCID.PrimeVice,
					NPCID.TheDestroyer,
					NPCID.Arapaima,
					NPCID.BlackRecluse,
					NPCID.WallCreeper,
					NPCID.WallCreeperWall,
					NPCID.BlackRecluseWall,
					NPCID.AngryTrapper,
					NPCID.Lihzahrd,
					NPCID.LihzahrdCrawler,
					NPCID.PirateCaptain,
					NPCID.QueenBee,
					NPCID.FlyingSnake,
					NPCID.Golem,
					NPCID.GolemFistLeft,
					NPCID.GolemFistRight,
					NPCID.Reaper,
					NPCID.Plantera,
					NPCID.PlanterasHook,
					NPCID.PlanterasTentacle,
					NPCID.BrainofCthulhu,
					NPCID.Paladin,
					NPCID.BoneLee,
					NPCID.MourningWood,
					NPCID.Pumpking,
					NPCID.PumpkingBlade,
					NPCID.PresentMimic,
					NPCID.Everscream,
					NPCID.IceQueen,
					NPCID.SantaNK1,
					NPCID.DukeFishron,
					NPCID.MoonLordHand,
					NPCID.StardustWormHead,
					NPCID.SolarCrawltipedeHead,
					NPCID.CultistDragonHead,
					NPCID.Butcher,
					NPCID.Psycho,
					NPCID.DeadlySphere,
					NPCID.BigMimicCorruption,
					NPCID.BigMimicCrimson,
					NPCID.BigMimicHallow,
					NPCID.Mothron,
					NPCID.DuneSplicerHead,
					NPCID.SlimeSpiked,
					NPCID.SandShark,
					NPCID.SandsharkCorrupt,
					NPCID.SandsharkCrimson,
					NPCID.SandsharkHallow,
					NPCID.DD2Betsy,
					ModContent.NPCType<Astrageldon>(),
					ModContent.NPCType<AstrumDeusHead>(),
					ModContent.NPCType<AstrumDeusHeadSpectral>(),
					ModContent.NPCType<Bumblefuck>(),
					ModContent.NPCType<CalamitasRun3>(),
					ModContent.NPCType<CosmicWraith>(),
					ModContent.NPCType<CrabulonIdle>(),
					ModContent.NPCType<Cryogen>(),
					ModContent.NPCType<DesertScourgeHead>(),
					ModContent.NPCType<GreatSandShark>(),
					ModContent.NPCType<HiveMindP2>(),
					ModContent.NPCType<NPCs.HiveMind.DankCreeper>(),
					ModContent.NPCType<AquaticAberration>(),
					ModContent.NPCType<Leviathan>(),
					ModContent.NPCType<Siren>(),
					ModContent.NPCType<PerforatorHeadLarge>(),
					ModContent.NPCType<PerforatorHeadMedium>(),
					ModContent.NPCType<PerforatorHeadSmall>(),
					ModContent.NPCType<PlaguebringerGoliath>(),
					ModContent.NPCType<PlagueHomingMissile>(),
					ModContent.NPCType<PlagueMine>(),
					ModContent.NPCType<Polterghast>(),
					ModContent.NPCType<PolterghastHook>(),
					ModContent.NPCType<PolterPhantom>(),
					ModContent.NPCType<ProfanedGuardianBoss>(),
					ModContent.NPCType<RockPillar>(),
					ModContent.NPCType<ScavengerBody>(),
					ModContent.NPCType<ScavengerClawRight>(),
					ModContent.NPCType<ScavengerClawLeft>(),
					ModContent.NPCType<ProfanedGuardianBoss2>(),
					ModContent.NPCType<ProvSpawnDefense>(),
					ModContent.NPCType<ProvSpawnOffense>(),
					ModContent.NPCType<NPCs.SlimeGod.SlimeGod>(),
					ModContent.NPCType<SlimeGodRun>(),
					ModContent.NPCType<SlimeGodCore>(),
					ModContent.NPCType<SlimeGodSplit>(),
					ModContent.NPCType<SlimeGodRunSplit>(),
					ModContent.NPCType<StormWeaverHead>(),
					ModContent.NPCType<StormWeaverHeadNaked>(),
					ModContent.NPCType<SupremeCalamitas>(),
					ModContent.NPCType<DevourerofGodsHead>(),
					ModContent.NPCType<DevourerofGodsHead2>(),
					ModContent.NPCType<DevourerofGodsHeadS>(),
					ModContent.NPCType<Yharon>(),
					ModContent.NPCType<AquaticScourgeHead>(),
					ModContent.NPCType<BobbitWormHead>(),
					ModContent.NPCType<AquaticSeekerHead>(),
					ModContent.NPCType<ColossalSquid>(),
					ModContent.NPCType<EidolonWyrmHead>(),
					ModContent.NPCType<EidolonWyrmHeadHuge>(),
					ModContent.NPCType<GulperEelHead>(),
					ModContent.NPCType<Mauler>(),
					ModContent.NPCType<Reaper>(),
					ModContent.NPCType<Atlas>(),
					ModContent.NPCType<ArmoredDiggerHead>(),
					ModContent.NPCType<Cnidrion>(),
					ModContent.NPCType<Horse>(),
					ModContent.NPCType<ScornEater>(),
					ModContent.NPCType<PrismTurtle>(),
					ModContent.NPCType<GhostBell>(),
					ModContent.NPCType<EutrophicRay>(),
					ModContent.NPCType<Clam>(),
					ModContent.NPCType<SeaSerpent1>(),
					ModContent.NPCType<BlindedAngler>(),
					ModContent.NPCType<GiantClam>()
				};

				revengeanceProjectileBuffList = new List<int>()
				{
					ProjectileID.SandBallFalling,
					ProjectileID.AshBallFalling,
					ProjectileID.DemonSickle,
					ProjectileID.EbonsandBallFalling,
					ProjectileID.PearlSandBallFalling,
					ProjectileID.CursedFlameHostile,
					ProjectileID.EyeFire,
					ProjectileID.Boulder,
					ProjectileID.DeathLaser,
					ProjectileID.PoisonDartTrap,
					ProjectileID.SpikyBallTrap,
					ProjectileID.SpearTrap,
					ProjectileID.FlamethrowerTrap,
					ProjectileID.FlamesTrap,
					ProjectileID.CrimsandBallFalling,
					ProjectileID.Fireball,
					ProjectileID.EyeBeam,
					ProjectileID.PoisonSeedPlantera,
					ProjectileID.ThornBall,
					ProjectileID.PaladinsHammerHostile,
					ProjectileID.RocketSkeleton,
					ProjectileID.FlamingWood,
					ProjectileID.FlamingScythe,
					ProjectileID.FrostWave,
					ProjectileID.Present,
					ProjectileID.Spike,
					ProjectileID.SaucerDeathray,
					ProjectileID.PhantasmalEye,
					ProjectileID.PhantasmalSphere,
					ProjectileID.PhantasmalDeathray,
					ProjectileID.CultistBossIceMist,
					ProjectileID.CultistBossLightningOrbArc,
					ProjectileID.CultistBossFireBall,
					ProjectileID.NebulaBolt,
					ProjectileID.NebulaSphere,
					ProjectileID.NebulaLaser,
					ProjectileID.StardustSoldierLaser,
					ProjectileID.VortexLaser,
					ProjectileID.VortexVortexLightning,
					ProjectileID.VortexLightning,
					ProjectileID.VortexAcid,
					ProjectileID.GeyserTrap,
					ProjectileID.SandnadoHostile,
					ProjectileID.DD2BetsyFireball,
					ProjectileID.DD2BetsyFlameBreath,
					ModContent.ProjectileType<AbyssMine>(),
					ModContent.ProjectileType<AbyssMine2>(),
					ModContent.ProjectileType<AstralFlame>(),
					ModContent.ProjectileType<BloodGeyser>(),
					ModContent.ProjectileType<BrimstoneBarrage>(),
					ModContent.ProjectileType<BrimstoneFireblast>(),
					ModContent.ProjectileType<BrimstoneGigaBlast>(),
					ModContent.ProjectileType<BrimstoneHellblast>(),
					ModContent.ProjectileType<BrimstoneHellblast2>(),
					ModContent.ProjectileType<BrimstoneHellfireball>(),
					ModContent.ProjectileType<BrimstoneLaser>(),
					ModContent.ProjectileType<BrimstoneLaserSplit>(),
					ModContent.ProjectileType<BrimstoneMonster>(),
					ModContent.ProjectileType<BrimstoneWave>(),
					ModContent.ProjectileType<DarkEnergyBall>(),
					ModContent.ProjectileType<DeusMine>(),
					ModContent.ProjectileType<DoGDeath>(),
					ModContent.ProjectileType<DoGFire>(),
					ModContent.ProjectileType<CosmicFlameBurst>(),
					ModContent.ProjectileType<FlareBomb>(),
					ModContent.ProjectileType<Flarenado>(),
					ModContent.ProjectileType<HiveBombGoliath>(),
					ModContent.ProjectileType<HolyBlast>(),
					ModContent.ProjectileType<HolyBomb>(),
					ModContent.ProjectileType<HolyShot>(),
					ModContent.ProjectileType<HolySpear>(),
					ModContent.ProjectileType<IceBomb>(),
					ModContent.ProjectileType<IchorShot>(),
					ModContent.ProjectileType<Infernado>(),
					ModContent.ProjectileType<LeviathanBomb>(),
					ModContent.ProjectileType<MoltenBlast>(),
					ModContent.ProjectileType<Mushmash>(),
					ModContent.ProjectileType<PhantomMine>(),
					ModContent.ProjectileType<PhantomBlast>(),
					ModContent.ProjectileType<PhantomBlast2>(),
					ModContent.ProjectileType<ProfanedSpear>(),
					ModContent.ProjectileType<ProvidenceCrystalShard>(),
					ModContent.ProjectileType<ProvidenceHolyRay>(),
					ModContent.ProjectileType<RedLightningFeather>(),
					ModContent.ProjectileType<SandPoisonCloud>(),
					ModContent.ProjectileType<ScavengerNuke>(),
					ModContent.ProjectileType<SirenSong>(),
					ModContent.ProjectileType<YharonFireball>(),
					ModContent.ProjectileType<YharonFireball2>(),
					ModContent.ProjectileType<PearlBurst>(),
					ModContent.ProjectileType<PearlRain>()
				};

				trapProjectileList = new List<int>()
				{
					ProjectileID.PoisonDartTrap,
					ProjectileID.SpikyBallTrap,
					ProjectileID.SpearTrap,
					ProjectileID.FlamethrowerTrap,
					ProjectileID.FlamesTrap,
					ProjectileID.PoisonDart,
					ProjectileID.GeyserTrap
				};

				scopedWeaponList = new List<int>()
				{
					ModContent.ItemType<Items.Weapons.AMR>(),
					ModContent.ItemType<Shroomer>(),
					ModContent.ItemType<SpectreRifle>(),
					ModContent.ItemType<Svantechnical>(),
					ModContent.ItemType<Skullmasher>()
				};

				trueMeleeBoostExceptionList = new List<int>()
				{
					ItemID.FlowerPow,
					ItemID.Flairon,
					ItemID.ChlorophytePartisan,
					ItemID.MushroomSpear,
					ItemID.NorthPole,
					ItemID.WoodYoyo,
					ItemID.CorruptYoyo,
					ItemID.CrimsonYoyo,
					ItemID.JungleYoyo,
					ItemID.Cascade,
					ItemID.Chik,
					ItemID.Code2,
					ItemID.Rally,
					ItemID.Yelets,
					ItemID.RedsYoyo,
					ItemID.ValkyrieYoyo,
					ItemID.Amarok,
					ItemID.HelFire,
					ItemID.Kraken,
					ItemID.TheEyeOfCthulhu,
					ItemID.FormatC,
					ItemID.Gradient,
					ItemID.Valor,
					ItemID.Terrarian,
					ModContent.ItemType<BallOFugu>(),
					ModContent.ItemType<TyphonsGreed>(),
					ModContent.ItemType<AmidiasTrident>(),
					ModContent.ItemType<HellionFlowerSpear>(),
					ModContent.ItemType<StarnightLance>(),
					ModContent.ItemType<TerraLance>(),
					ModContent.ItemType<Items.Weapons.Polterghast.BansheeHook>(),
					ModContent.ItemType<SpatialLance>(),
					ModContent.ItemType<StreamGouge>(),
					ModContent.ItemType<AirSpinner>(),
					ModContent.ItemType<Aorta>(),
					ModContent.ItemType<Azathoth>(),
					ModContent.ItemType<Chaotrix>(),
					ModContent.ItemType<Cnidarian>(),
					ModContent.ItemType<Lacerator>(),
					ModContent.ItemType<Quagmire>(),
					ModContent.ItemType<Shimmerspark>(),
					ModContent.ItemType<SolarFlare>(),
					ModContent.ItemType<TheEyeofCalamitas>(),
					ModContent.ItemType<TheGodsGambit>(),
					ModContent.ItemType<TheObliterator>(),
					ModContent.ItemType<ThePlaguebringer>(),
					ModContent.ItemType<Verdant>(),
					ModContent.ItemType<YinYo>()
				};

				skeletonList = new List<int>()
				{
					NPCID.BigPantlessSkeleton,
					NPCID.SmallPantlessSkeleton,
					NPCID.BigMisassembledSkeleton,
					NPCID.SmallMisassembledSkeleton,
					NPCID.BigHeadacheSkeleton,
					NPCID.SmallHeadacheSkeleton,
					NPCID.BigSkeleton,
					NPCID.SmallSkeleton,
					NPCID.HeavySkeleton,
					NPCID.Skeleton,
					NPCID.ArmoredSkeleton,
					NPCID.SkeletonArcher,
					NPCID.HeadacheSkeleton,
					NPCID.MisassembledSkeleton,
					NPCID.PantlessSkeleton,
					NPCID.SkeletonTopHat,
					NPCID.SkeletonAstonaut,
					NPCID.SkeletonAlien,
					NPCID.BoneThrowingSkeleton,
					NPCID.BoneThrowingSkeleton2,
					NPCID.BoneThrowingSkeleton3,
					NPCID.BoneThrowingSkeleton4,
					NPCID.GreekSkeleton
				};
			}
		}
		#endregion

		#region ModSupport
		public override void PostSetupContent()
		{
			WeakReferenceSupport.Setup();
		}

		public override object Call(params object[] args)
		{
			return ModSupportPreTrailer.Call(args);
		}
		#endregion

		#region DrawingStuff
		public static Color GetNPCColor(NPC npc, Vector2? position = null, bool effects = true, float shadowOverride = 0f)
		{
			return npc.GetAlpha(BuffEffects(npc, GetLightColor(position != null ? (Vector2)position : npc.Center), (shadowOverride != 0f ? shadowOverride : 0f), effects, npc.poisoned, npc.onFire, npc.onFire2, Main.player[Main.myPlayer].detectCreature, false, false, false, npc.venom, npc.midas, npc.ichor, npc.onFrostBurn, false, false, npc.dripping, npc.drippingSlime, npc.loveStruck, npc.stinky));
		}

		public static Color GetLightColor(Vector2 position)
		{
			return Lighting.GetColor((int)(position.X / 16f), (int)(position.Y / 16f));
		}

		public static Color BuffEffects(Entity codable, Color lightColor, float shadow = 0f, bool effects = true, bool poisoned = false, bool onFire = false, bool onFire2 = false, bool hunter = false, bool noItems = false, bool blind = false, bool bleed = false, bool venom = false, bool midas = false, bool ichor = false, bool onFrostBurn = false, bool burned = false, bool honey = false, bool dripping = false, bool drippingSlime = false, bool loveStruck = false, bool stinky = false)
		{
			float cr = 1f; float cg = 1f; float cb = 1f; float ca = 1f;
			if (effects && honey && Main.rand.Next(30) == 0)
			{
				int dustID = Dust.NewDust(codable.position, codable.width, codable.height, 152, 0f, 0f, 150, default(Color), 1f);
				Main.dust[dustID].velocity.Y = 0.3f;
				Main.dust[dustID].velocity.X *= 0.1f;
				Main.dust[dustID].scale += (float)Main.rand.Next(3, 4) * 0.1f;
				Main.dust[dustID].alpha = 100;
				Main.dust[dustID].noGravity = true;
				Main.dust[dustID].velocity += codable.velocity * 0.1f;
			}
			if (poisoned)
			{
				if (effects && Main.rand.Next(30) == 0)
				{
					int dustID = Dust.NewDust(codable.position, codable.width, codable.height, 46, 0f, 0f, 120, default(Color), 0.2f);
					Main.dust[dustID].noGravity = true;
					Main.dust[dustID].fadeIn = 1.9f;
				}
				cr *= 0.65f;
				cb *= 0.75f;
			}
			if (venom)
			{
				if (effects && Main.rand.Next(10) == 0)
				{
					int dustID = Dust.NewDust(codable.position, codable.width, codable.height, 171, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[dustID].noGravity = true;
					Main.dust[dustID].fadeIn = 1.5f;
				}
				cg *= 0.45f;
				cr *= 0.75f;
			}
			if (midas)
			{
				cb *= 0.3f;
				cr *= 0.85f;
			}
			if (ichor)
			{
				if (codable is NPC)
				{
					lightColor = new Color(255, 255, 0, 255);
				}
				else
				{
					cb = 0f;
				}
			}
			if (burned)
			{
				if (effects)
				{
					int dustID = Dust.NewDust(new Vector2(codable.position.X - 2f, codable.position.Y - 2f), codable.width + 4, codable.height + 4, 6, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 2f);
					Main.dust[dustID].noGravity = true;
					Main.dust[dustID].velocity *= 1.8f;
					Main.dust[dustID].velocity.Y -= 0.75f;
				}
				if (codable is Player)
				{
					cr = 1f;
					cb *= 0.6f;
					cg *= 0.7f;
				}
			}
			if (onFrostBurn)
			{
				if (effects)
				{
					if (Main.rand.Next(4) < 3)
					{
						int dustID = Dust.NewDust(new Vector2(codable.position.X - 2f, codable.position.Y - 2f), codable.width + 4, codable.height + 4, 135, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
						Main.dust[dustID].noGravity = true;
						Main.dust[dustID].velocity *= 1.8f;
						Main.dust[dustID].velocity.Y -= 0.5f;
						if (Main.rand.Next(4) == 0)
						{
							Main.dust[dustID].noGravity = false;
							Main.dust[dustID].scale *= 0.5f;
						}
					}
					Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 0.1f, 0.6f, 1f);
				}
				if (codable is Player)
				{
					cr *= 0.5f;
					cg *= 0.7f;
				}
			}
			if (onFire)
			{
				if (effects)
				{
					if (Main.rand.Next(4) != 0)
					{
						int dustID = Dust.NewDust(codable.position - new Vector2(2f, 2f), codable.width + 4, codable.height + 4, 6, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
						Main.dust[dustID].noGravity = true;
						Main.dust[dustID].velocity *= 1.8f;
						Main.dust[dustID].velocity.Y -= 0.5f;
						if (Main.rand.Next(4) == 0)
						{
							Main.dust[dustID].noGravity = false;
							Main.dust[dustID].scale *= 0.5f;
						}
					}
					Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
				}
				if (codable is Player)
				{
					cb *= 0.6f;
					cg *= 0.7f;
				}
			}
			if (dripping && shadow == 0f && Main.rand.Next(4) != 0)
			{
				Vector2 position = codable.position;
				position.X -= 2f; position.Y -= 2f;
				if (Main.rand.Next(2) == 0)
				{
					int dustID = Dust.NewDust(position, codable.width + 4, codable.height + 2, 211, 0f, 0f, 50, default(Color), 0.8f);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[dustID].alpha += 25;
					}
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[dustID].alpha += 25;
					}
					Main.dust[dustID].noLight = true;
					Main.dust[dustID].velocity *= 0.2f;
					Main.dust[dustID].velocity.Y += 0.2f;
					Main.dust[dustID].velocity += codable.velocity;
				}
				else
				{
					int dustID = Dust.NewDust(position, codable.width + 8, codable.height + 8, 211, 0f, 0f, 50, default(Color), 1.1f);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[dustID].alpha += 25;
					}
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[dustID].alpha += 25;
					}
					Main.dust[dustID].noLight = true;
					Main.dust[dustID].noGravity = true;
					Main.dust[dustID].velocity *= 0.2f;
					Main.dust[dustID].velocity.Y += 1f;
					Main.dust[dustID].velocity += codable.velocity;
				}
			}
			if (drippingSlime && shadow == 0f)
			{
				int alpha = 175;
				Color newColor = new Color(0, 80, 255, 100);
				if (Main.rand.Next(4) != 0)
				{
					if (Main.rand.Next(2) == 0)
					{
						Vector2 position2 = codable.position;
						position2.X -= 2f; position2.Y -= 2f;
						int dustID = Dust.NewDust(position2, codable.width + 4, codable.height + 2, 4, 0f, 0f, alpha, newColor, 1.4f);
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[dustID].alpha += 25;
						}
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[dustID].alpha += 25;
						}
						Main.dust[dustID].noLight = true;
						Main.dust[dustID].velocity *= 0.2f;
						Main.dust[dustID].velocity.Y += 0.2f;
						Main.dust[dustID].velocity += codable.velocity;
					}
				}
				cr *= 0.8f;
				cg *= 0.8f;
			}
			if (onFire2)
			{
				if (effects)
				{
					if (Main.rand.Next(4) != 0)
					{
						int dustID = Dust.NewDust(codable.position - new Vector2(2f, 2f), codable.width + 4, codable.height + 4, 75, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
						Main.dust[dustID].noGravity = true;
						Main.dust[dustID].velocity *= 1.8f;
						Main.dust[dustID].velocity.Y -= 0.5f;
						if (Main.rand.Next(4) == 0)
						{
							Main.dust[dustID].noGravity = false;
							Main.dust[dustID].scale *= 0.5f;
						}
					}
					Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
				}
				if (codable is Player)
				{
					cb *= 0.6f;
					cg *= 0.7f;
				}
			}
			if (noItems)
			{
				cr *= 0.65f;
				cg *= 0.8f;
			}
			if (blind)
			{
				cr *= 0.7f;
				cg *= 0.65f;
			}
			if (bleed)
			{
				bool dead = (codable is Player ? ((Player)codable).dead : codable is NPC ? ((NPC)codable).life <= 0 : false);
				if (effects && !dead && Main.rand.Next(30) == 0)
				{
					int dustID = Dust.NewDust(codable.position, codable.width, codable.height, 5, 0f, 0f, 0, default(Color), 1f);
					Main.dust[dustID].velocity.Y += 0.5f;
					Main.dust[dustID].velocity *= 0.25f;
				}
				cg *= 0.9f;
				cb *= 0.9f;
			}
			if (loveStruck && effects && shadow == 0f && Main.instance.IsActive && !Main.gamePaused && Main.rand.Next(5) == 0)
			{
				Vector2 value = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
				value.Normalize();
				value.X *= 0.66f;
				int goreID = Gore.NewGore(codable.GetSource_FromThis(null),codable.position + new Vector2((float)Main.rand.Next(codable.width + 1), (float)Main.rand.Next(codable.height + 1)), value * (float)Main.rand.Next(3, 6) * 0.33f, 331, (float)Main.rand.Next(40, 121) * 0.01f);
				Main.gore[goreID].sticky = false;
				Main.gore[goreID].velocity *= 0.4f;
				Main.gore[goreID].velocity.Y -= 0.6f;
			}
			if (stinky && shadow == 0f)
			{
				cr *= 0.7f;
				cb *= 0.55f;
				if (effects && Main.rand.Next(5) == 0 && Main.instance.IsActive && !Main.gamePaused)
				{
					Vector2 value2 = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
					value2.Normalize(); value2.X *= 0.66f; value2.Y = Math.Abs(value2.Y);
					Vector2 vector = value2 * (float)Main.rand.Next(3, 5) * 0.25f;
					int dustID = Dust.NewDust(codable.position, codable.width, codable.height, 188, vector.X, vector.Y * 0.5f, 100, default(Color), 1.5f);
					Main.dust[dustID].velocity *= 0.1f;
					Main.dust[dustID].velocity.Y -= 0.5f;
				}
			}
			lightColor.R = (byte)((float)lightColor.R * cr);
			lightColor.G = (byte)((float)lightColor.G * cg);
			lightColor.B = (byte)((float)lightColor.B * cb);
			lightColor.A = (byte)((float)lightColor.A * ca);
			if (codable is NPC)
			{
				NPCLoader.DrawEffects((NPC)codable, ref lightColor);
			}
			if (hunter && (codable is NPC ? ((NPC)codable).lifeMax > 1 : true))
			{
				if (effects && !Main.gamePaused && Main.instance.IsActive && Main.rand.Next(50) == 0)
				{
					int dustID = Dust.NewDust(codable.position, codable.width, codable.height, 15, 0f, 0f, 150, default(Color), 0.8f);
					Main.dust[dustID].velocity *= 0.1f;
					Main.dust[dustID].noLight = true;
				}
				byte colorR = 50, colorG = 255, colorB = 50;
				if (codable is NPC && !(((NPC)codable).friendly || ((NPC)codable).catchItem > 0 || (((NPC)codable).damage == 0 && ((NPC)codable).lifeMax == 5)))
				{
					colorR = 255; colorG = 50;
				}
				if (!(codable is NPC) && lightColor.R < 150)
				{
					lightColor.A = Main.mouseTextColor;
				}
				if (lightColor.R < colorR)
				{
					lightColor.R = colorR;
				}
				if (lightColor.G < colorG)
				{
					lightColor.G = colorG;
				}
				if (lightColor.B < colorB)
				{
					lightColor.B = colorB;
				}
			}
			return lightColor;
		}

		public static void DrawTexture(object sb, Texture2D texture, int shader, Entity codable, Color? overrideColor = null, bool drawCentered = false)
		{
			Color lightColor = (overrideColor != null ? (Color)overrideColor : codable is NPC ? GetNPCColor(((NPC)codable), codable.Center, false) : codable is Projectile ? ((Projectile)codable).GetAlpha(GetLightColor(codable.Center)) : GetLightColor(codable.Center));
			int frameCount = (codable is NPC ? Main.npcFrameCount[((NPC)codable).type] : 1);
			Rectangle frame = (codable is NPC ? ((NPC)codable).frame : new Rectangle(0, 0, texture.Width, texture.Height));
			float scale = (codable is NPC ? ((NPC)codable).scale : ((Projectile)codable).scale);
			float rotation = (codable is NPC ? ((NPC)codable).rotation : ((Projectile)codable).rotation);
			int spriteDirection = (codable is NPC ? ((NPC)codable).spriteDirection : ((Projectile)codable).spriteDirection);
			float offsetY = (codable is NPC ? ((NPC)codable).gfxOffY : 0f);
			DrawTexture(sb, texture, shader, codable.position + new Vector2(0f, offsetY), codable.width, codable.height, scale, rotation, spriteDirection, frameCount, frame, lightColor, drawCentered);
		}

		public static void DrawTexture(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, float scale, float rotation, int direction, int framecount, Rectangle frame, Color? overrideColor = null, bool drawCentered = false)
		{
			Vector2 origin = new Vector2((float)(texture.Width / 2), (float)(texture.Height / framecount / 2));
			Color lightColor = overrideColor != null ? (Color)overrideColor : GetLightColor(position + new Vector2(width * 0.5f, height * 0.5f));
			if (sb is List<DrawData>)
			{
				DrawData dd = new DrawData(texture, GetDrawPosition(position, origin, width, height, texture.Width, texture.Height, framecount, scale, drawCentered), frame, lightColor, rotation, origin, scale, direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
				dd.shader = shader;
				((List<DrawData>)sb).Add(dd);
			}
			else if (sb is SpriteBatch)
			{
				bool applyDye = shader > 0;
				if (applyDye)
				{
					((SpriteBatch)sb).End();
					((SpriteBatch)sb).Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
					GameShaders.Armor.ApplySecondary(shader, Main.player[Main.myPlayer], null);
				}
				((SpriteBatch)sb).Draw(texture, GetDrawPosition(position, origin, width, height, texture.Width, texture.Height, framecount, scale, drawCentered), frame, lightColor, rotation, origin, scale, direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
				if (applyDye)
				{
					((SpriteBatch)sb).End();
					((SpriteBatch)sb).Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
				}
			}
		}

		public static Vector2 GetDrawPosition(Vector2 position, Vector2 origin, int width, int height, int texWidth, int texHeight, int framecount, float scale, bool drawCentered = false)
		{
			Vector2 screenPos = new Vector2((int)Main.screenPosition.X, (int)Main.screenPosition.Y);
			if (drawCentered)
			{
				Vector2 texHalf = new Vector2(texWidth / 2, texHeight / framecount / 2);
				return (position + new Vector2(width * 0.5f, height * 0.5f)) - (texHalf * scale) + (origin * scale) - screenPos;
			}
			return position - screenPos + new Vector2(width * 0.5f, height) - new Vector2(texWidth * scale / 2f, texHeight * scale / (float)framecount) + (origin * scale) + new Vector2(0f, 5f);
		}
		#endregion

		#region Recipes
		public override void AddRecipeGroups()
		{
			CalamityRecipesPreTrailer.AddRecipeGroups();
		}

		public override void AddRecipes()
		{
			CalamityRecipesPreTrailer.AddRecipes();
		}
		#endregion

		#region Seasons
		public static Season season
		{
			get
			{
				DateTime date = DateTime.Now;
				int day = date.DayOfYear - Convert.ToInt32((DateTime.IsLeapYear(date.Year)) && date.DayOfYear > 59);

				if (day < 80 || day >= 355)
				{
					return Season.Winter;
				}

				else if (day >= 80 && day < 172)
				{
					return Season.Spring;
				}

				else if (day >= 172 && day < 266)
				{
					return Season.Summer;
				}

				else
				{
					return Season.Fall;
				}
			}
		}
		#endregion

		#region Packets
		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			CalamityModClassicPreTrailerMessageType msgType = (CalamityModClassicPreTrailerMessageType)reader.ReadByte();
			switch (msgType)
			{
				case CalamityModClassicPreTrailerMessageType.MeleeLevelSync:
					Main.player[reader.ReadInt32()].GetModPlayer<CalamityPlayerPreTrailer>().HandleLevels(reader, 0);
					break;
				case CalamityModClassicPreTrailerMessageType.RangedLevelSync:
					Main.player[reader.ReadInt32()].GetModPlayer<CalamityPlayerPreTrailer>().HandleLevels(reader, 1);
					break;
				case CalamityModClassicPreTrailerMessageType.MagicLevelSync:
					Main.player[reader.ReadInt32()].GetModPlayer<CalamityPlayerPreTrailer>().HandleLevels(reader, 2);
					break;
				case CalamityModClassicPreTrailerMessageType.SummonLevelSync:
					Main.player[reader.ReadInt32()].GetModPlayer<CalamityPlayerPreTrailer>().HandleLevels(reader, 3);
					break;
				case CalamityModClassicPreTrailerMessageType.RogueLevelSync:
					Main.player[reader.ReadInt32()].GetModPlayer<CalamityPlayerPreTrailer>().HandleLevels(reader, 4);
					break;
				case CalamityModClassicPreTrailerMessageType.ExactMeleeLevelSync:
					Main.player[reader.ReadInt32()].GetModPlayer<CalamityPlayerPreTrailer>().HandleExactLevels(reader, 0);
					break;
				case CalamityModClassicPreTrailerMessageType.ExactRangedLevelSync:
					Main.player[reader.ReadInt32()].GetModPlayer<CalamityPlayerPreTrailer>().HandleExactLevels(reader, 1);
					break;
				case CalamityModClassicPreTrailerMessageType.ExactMagicLevelSync:
					Main.player[reader.ReadInt32()].GetModPlayer<CalamityPlayerPreTrailer>().HandleExactLevels(reader, 2);
					break;
				case CalamityModClassicPreTrailerMessageType.ExactSummonLevelSync:
					Main.player[reader.ReadInt32()].GetModPlayer<CalamityPlayerPreTrailer>().HandleExactLevels(reader, 3);
					break;
				case CalamityModClassicPreTrailerMessageType.ExactRogueLevelSync:
					Main.player[reader.ReadInt32()].GetModPlayer<CalamityPlayerPreTrailer>().HandleExactLevels(reader, 4);
					break;
				case CalamityModClassicPreTrailerMessageType.StressSync:
					Main.player[reader.ReadInt32()].GetModPlayer<CalamityPlayerPreTrailer>().HandleStress(reader);
					break;
				case CalamityModClassicPreTrailerMessageType.BossRushStage:
					int stage = reader.ReadInt32();
					CalamityWorldPreTrailer.bossRushStage = stage;
					break;
				case CalamityModClassicPreTrailerMessageType.AdrenalineSync:
					Main.player[reader.ReadInt32()].GetModPlayer<CalamityPlayerPreTrailer>().HandleAdrenaline(reader);
					break;
				case CalamityModClassicPreTrailerMessageType.TeleportPlayer:
					Main.player[reader.ReadInt32()].GetModPlayer<CalamityPlayerPreTrailer>().HandleTeleport(reader.ReadInt32(), true, whoAmI);
					break;
				case CalamityModClassicPreTrailerMessageType.DoGCountdownSync:
					int countdown = reader.ReadInt32();
					CalamityWorldPreTrailer.DoGSecondStageCountdown = countdown;
					break;
				case CalamityModClassicPreTrailerMessageType.BossSpawnCountdownSync:
					int countdown2 = reader.ReadInt32();
					CalamityWorldPreTrailer.bossSpawnCountdown = countdown2;
					break;
				case CalamityModClassicPreTrailerMessageType.BossTypeSync:
					int type = reader.ReadInt32();
					CalamityWorldPreTrailer.bossType = type;
					break;
				case CalamityModClassicPreTrailerMessageType.DeathCountSync:
					Main.player[reader.ReadInt32()].GetModPlayer<CalamityPlayerPreTrailer>().HandleDeathCount(reader);
					break;
				default:
					CalamityModClassicPreTrailer.Instance.Logger.Error("CalamityModClassicPreTrailer: Unknown Message type: " + msgType);
					break;
			}
		}
		#endregion
	}

	public enum Season : byte
	{
		Winter,
		Spring,
		Summer,
		Fall
	}

	enum CalamityModClassicPreTrailerMessageType : byte
	{
		MeleeLevelSync,
		RangedLevelSync,
		MagicLevelSync,
		SummonLevelSync,
		RogueLevelSync,
		ExactMeleeLevelSync,
		ExactRangedLevelSync,
		ExactMagicLevelSync,
		ExactSummonLevelSync,
		ExactRogueLevelSync,
		StressSync,
		AdrenalineSync,
		TeleportPlayer,
		BossRushStage,
		DoGCountdownSync,
		BossSpawnCountdownSync,
		BossTypeSync,
		DeathCountSync
	}
}