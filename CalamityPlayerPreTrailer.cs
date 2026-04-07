using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using CalamityModClassicPreTrailer.NPCs;
using CalamityModClassicPreTrailer.NPCs.TheDevourerofGods;
using CalamityModClassicPreTrailer.NPCs.Calamitas;
using CalamityModClassicPreTrailer.NPCs.PlaguebringerGoliath;
using CalamityModClassicPreTrailer.NPCs.Yharon;
using CalamityModClassicPreTrailer.NPCs.Leviathan;
using CalamityModClassicPreTrailer.Projectiles;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.UI;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using ReLogic.Content;
using Terraria.ModLoader.IO;

namespace CalamityModClassicPreTrailer
{
	public class CalamityPlayerPreTrailer : ModPlayer
	{
		#region InstanceVars

		#region NormalVars
		public static bool areThereAnyDamnBosses = false;

		private const int saveVersion = 0;

		public const float defEndurance = 0.33f;

		public int dashMod;

		public int dashTimeMod;

		public int projTypeJustHitBy;

		public int gainRageCooldown = 60;

		public int bossRushImmunityFrameCurseTimer = 0;

		public int aBulwarkRareMeleeBoostTimer = 0;

		public int ataraxiaDamageBoostCooldown = 0;

		public int ataraxiaDamageBoostCancelTimer = 1200;

		public int nebulaManaNerfCounter = 0;

		public int alcoholPoisonLevel = 0;

		public int modStealthTimer;

		public int hInfernoBoost = 0;

		public int pissWaterBoost = 0;

		public int packetTimer = 0;

		public int sCalDeathCount = 0;

		public int sCalKillCount = 0;

		public int deathCount = 0;

		public int bloodflareHeartTimer = 180;

		public int bloodflareManaTimer = 180;

		public float ataraxiaDamageBoost = 0f;

		public float rogueStealth = 0f;

		public float rogueStealthMax = 0f;

		public float modStealth = 1f;

		public float aquaticBoost = 1f;

		public float shieldInvinc = 5f;

		public bool playRogueStealthSound = false;

		public bool playFullRageSound = true;

		public bool playFullAdrenalineSound = true;

		public bool playAdrenalineBurnoutSound = true;

		public bool playFullAdrenalineBurnoutSound = true;

		public bool onyxExcavator = false;

		public bool angryDog = false;

		public bool fab = false;

		public bool crysthamyr = false;

		public bool drawBossHPBar = true;

		public bool shouldDrawSmallText = true;
		#endregion

		#region ProficiencyStuff
		public int meleeLevel = 0;
		public int rangedLevel = 0;
		public int magicLevel = 0;
		public int rogueLevel = 0;
		public int summonLevel = 0;

		public bool shootFireworksLevelUpMelee = true;
		public bool shootFireworksLevelUpRanged = true;
		public bool shootFireworksLevelUpMagic = true;
		public bool shootFireworksLevelUpSummon = true;
		public bool shootFireworksLevelUpRogue = true;

		public int exactMeleeLevel = 0;
		public int exactRangedLevel = 0;
		public int exactMagicLevel = 0;
		public int exactSummonLevel = 0;
		public int exactRogueLevel = 0;

		public int gainLevelCooldown = 120; // 120 frames before gaining another stat point, avoiding i-frame nonsense
		#endregion

		#region PetStuff
		public bool akato = false;

		public bool leviPet = false;

		public bool sirenPet = false;

		public bool fox = false;

		public bool chibii = false;

		public bool brimling = false;

		public bool bearPet = false;

		public bool kendra = false;

		public bool trashMan = false;

		public int trashManChest = -1;
		#endregion

		#region StressStuff
		public const int stressMax = 10000;

		public int stress;

		public int stressCD;

		public bool heartOfDarkness = false;

		public bool draedonsHeart = false;

		public bool draedonsStressGain = false;

		public bool afflicted = false;

		public bool affliction = false;

		public bool stressPills = false;

		public bool laudanum = false;

		public bool stressLevel500 = false;

		public bool rageMode = false;
		#endregion

		#region AdrenalineStuff
		public const int adrenalineMax = 10000;

		public int adrenalineMaxTimer = 300;

		public int adrenalineDmgDown = 600;

		public float adrenalineDmgMult = 1f;

		public int adrenaline;

		public int adrenalineCD;

		public bool adrenalineMode = false;
		#endregion

		#region PermanentStuff
		public bool extraAccessoryML = false;

		public bool eCore = false;

		public bool pHeart = false;

		public bool cShard = false;

		public bool mFruit = false;

		public bool bOrange = false;

		public bool eBerry = false;

		public bool dFruit = false;

		public bool revJamDrop = false;

		public bool rageBoostOne = false;

		public bool rageBoostTwo = false;

		public bool rageBoostThree = false;

		public bool adrenalineBoostOne = false;

		public bool adrenalineBoostTwo = false;

		public bool adrenalineBoostThree = false;
		#endregion

		#region LoreStuff
		public bool desertScourgeLore = false;

		public bool eaterOfWorldsLore = false;

		public bool hiveMindLore = false;

		public bool perforatorLore = false;

		public bool queenBeeLore = false;

		public bool skeletronLore = false;

		public bool wallOfFleshLore = false;

		public bool destroyerLore = false;

		public bool aquaticScourgeLore = false;

		public bool skeletronPrimeLore = false;

		public bool brimstoneElementalLore = false;

		public bool calamitasLore = false;

		public bool planteraLore = false;

		public bool leviathanAndSirenLore = false;

		public bool astrumAureusLore = false;

		public bool astrumDeusLore = false;

		public bool golemLore = false;

		public bool plaguebringerGoliathLore = false;

		public bool dukeFishronLore = false;

		public bool ravagerLore = false;

		public bool lunaticCultistLore = false;

		public bool moonLordLore = false;

		public bool providenceLore = false;

		public bool polterghastLore = false;

		public bool DoGLore = false;

		public bool yharonLore = false;

		public bool SCalLore = false;
		#endregion

		#region AccessoryStuff
		public bool fasterMeleeLevel = false;

		public bool fasterRangedLevel = false;

		public bool fasterMagicLevel = false;

		public bool fasterSummonLevel = false;

		public bool fasterRogueLevel = false;

		public bool luxorsGift = false;

		public bool fungalSymbiote = false;

		public bool trinketOfChi = false;

		public bool gladiatorSword = false;

		public bool unstablePrism = false;

		public bool regenator = false;

		public bool theBee = false;

		public int theBeeDamage = 0;

		public bool rBrain = false;

		public bool bloodyWormTooth = false;

		public bool rampartOfDeities = false;

		public bool vexation = false;

		public bool fBulwark = false;

		public bool dodgeScarf = false;

		public bool badgeOfBravery = false;

		public bool badgeOfBraveryRare = false;

		public bool scarfCooldown = false;

		public bool cryogenSoul = false;

		public bool yInsignia = false;

		public bool eGauntlet = false;

		public bool eTalisman = false;

		public bool statisBeltOfCurses = false;

		public bool elysianAegis = false;

		public bool elysianGuard = false;

		public bool nCore = false;

		public bool deepDiver = false;

		public bool abyssalDivingSuitPlates = false;

		public bool abyssalDivingSuitCooldown = false;

		public int abyssalDivingSuitPlateHits = 0;

		public bool sirenWaterBuff = false;

		public bool sirenIce = false;

		public bool sirenIceCooldown = false;

		public bool aSpark = false;

		public bool aSparkRare = false;

		public bool aBulwark = false;

		public bool aBulwarkRare = false;

		public bool dAmulet = false;

		public bool fCarapace = false;

		public bool gShell = false;

		public bool absorber = false;

		public bool aAmpoule = false;

		public bool pAmulet = false;

		public bool fBarrier = false;

		public bool aBrain = false;

		public bool lol = false;

		public bool raiderTalisman = false;

		public int raiderStack = 0;

		public bool sGenerator = false;

		public bool sDefense = false;

		public bool sPower = false;

		public bool sRegen = false;

		public bool IBoots = false;

		public bool elysianFire = false;

		public bool sTracers = false;

		public bool eTracers = false;

		public bool cTracers = false;

		public bool frostFlare = false;

		public bool beeResist = false;

		public bool uberBees = false;

		public bool projRef = false;

		public bool projRefRare = false;

		public int projRefRareLifeRegenCounter = 0;

		public bool nanotech = false;

		public bool eQuiver = false;

		public bool shadowMinions = false;

		public bool tearMinions = false;

		public bool alchFlask = false;

		public bool community = false;

		public bool fleshTotem = false;

		public int fleshTotemCooldown = 0;

		public bool bloodPact = false;

		public bool bloodflareCore = false;

		public bool coreOfTheBloodGod = false;

		public bool elementalHeart = false;

		public bool crownJewel = false;

		public bool celestialJewel = false;

		public bool astralArcanum = false;

		public bool harpyRing = false;

		public bool ironBoots = false;

		public bool depthCharm = false;

		public bool anechoicPlating = false;

		public bool jellyfishNecklace = false;

		public bool abyssalAmulet = false;

		public bool lumenousAmulet = false;

		public bool reaperToothNecklace = false;

		public bool aquaticEmblem = false;

		public bool darkSunRing = false;

		public bool calamityRing = false;

		public bool eArtifact = false;

		public bool dArtifact = false;

		public bool gArtifact = false;

		public bool pArtifact = false;

		public bool giantPearl = false;

		public bool normalityRelocator = false;

		public bool fabledTortoise = false;
		#endregion

		#region ArmorSetStuff
		public bool victideSet = false;

		public bool aeroSet = false;

		public bool statigelSet = false;

		public bool tarraSet = false;

		public bool tarraMelee = false;

		public bool tarraDefense = false;

		public int tarraCooldown = 0;

		public int tarraDefenseTime = 600;

		public bool tarraMage = false;

		public int tarraMageHealCooldown = 0;

		public int tarraCrits = 0;

		public bool tarraRanged = false;

		public bool tarraThrowing = false;

		public int tarraThrowingCrits = 0;

		public int tarraThrowingCritTimer = 0;

		public bool tarraSummon = false;

		public bool bloodflareSet = false;

		public bool bloodflareMelee = false;

		public int bloodflareMeleeHits = 0;

		public int bloodflareFrenzyTimer = 0;

		public int bloodflareFrenzyCooldown = 0;

		public bool bloodflareRanged = false;

		public int bloodflareRangedCooldown = 0;

		public bool bloodflareThrowing = false;

		public bool bloodflareMage = false;

		public int bloodflareMageCooldown = 0;

		public bool bloodflareSummon = false;

		public int bloodflareSummonTimer = 0;

		public bool godSlayer = false;

		public bool godSlayerDamage = false;

		public bool godSlayerMage = false;

		public bool godSlayerRanged = false;

		public bool godSlayerThrowing = false;

		public bool godSlayerSummon = false;

		public float godSlayerDmg;

		public bool godSlayerReflect = false;

		public bool godSlayerCooldown = false;

		public bool ataxiaBolt = false;

		public bool ataxiaFire = false;

		public bool ataxiaVolley = false;

		public bool ataxiaBlaze = false;

		public bool daedalusAbsorb = false;

		public bool daedalusShard = false;

		public bool reaverSpore = false;

		public bool reaverDoubleTap = false;

		public bool flamethrowerBoost = false;

		public bool shadeRegen = false;

		public bool shadowSpeed = false;

		public bool dsSetBonus = false;

		public bool auricBoost = false;

		public bool daedalusReflect = false;

		public bool daedalusSplit = false;

		public bool reaverBlast = false;

		public bool reaverBurst = false;

		public bool astralStarRain = false;

		public int astralStarRainCooldown = 0;

		public float ataxiaDmg;

		public bool ataxiaMage = false;

		public bool ataxiaGeyser = false;

		public float xerocDmg;

		public bool xerocSet = false;

		public bool silvaSet = false;

		public bool silvaMelee = false;

		public bool silvaRanged = false;

		public bool silvaThrowing = false;

		public bool silvaMage = false;

		public bool silvaSummon = false;

		public bool hasSilvaEffect = false;

		public int silvaCountdown = 600;

		public int silvaHitCounter = 0;

		public bool auricSet = false;

		public bool omegaBlueChestplate = false;

		public bool omegaBlueSet = false;

		public bool omegaBlueHentai = false;

		public int omegaBlueCooldown = 0;

		public bool molluskSet = false;
		#endregion

		#region DebuffStuff
		public bool lethalLavaBurn = false;

		public bool aCrunch = false;

		public bool NOU = false;

		public bool hAttack = false;

		public bool horror = false;

		public bool irradiated = false;

		public bool bFlames = false;

		public bool aFlames = false;

		public bool gsInferno = false;

		public bool pFlames = false;

		public bool hFlames = false;

		public bool hInferno = false;

		public bool gState = false;

		public bool bBlood = false;

		public bool eGravity = false;

		public bool weakPetrification = false;

		public bool vHex = false;

		public bool eGrav = false;

		public bool warped = false;

		public bool marked = false;

		public bool cDepth = false;

		public bool fishAlert = false;

		public bool bOut = false;

		public bool clamity = false;
		#endregion

		#region BuffStuff
		public bool trinketOfChiBuff = false;

		public int chiBuffTimer = 0;

		public bool corrEffigy = false;

		public bool crimEffigy = false;

		public bool rRage = false;

		public bool tRegen = false;

		public bool xRage = false;

		public bool xWrath = false;

		public bool graxDefense = false;

		public bool sMeleeBoost = false;

		public bool tFury = false;

		public bool cadence = false;

		public bool omniscience = false;

		public bool zerg = false;

		public bool zen = false;

		public bool yPower = false;

		public bool aWeapon = false;

		public bool tScale = false;

		public bool fabsolVodka = false;

		public bool mushy = false;

		public bool molten = false;

		public bool shellBoost = false;

		public bool cFreeze = false;

		public bool invincible = false;

		public bool shine = false;

		public bool anechoicCoating = false;

		public bool enraged = false;

		public int revivifyTimer = 0;

		public bool permafrostsConcoction = false;

		public bool armorCrumbling = false;

		public bool armorShattering = false;

		public bool ceaselessHunger = false;

		public bool calcium = false;

		public bool soaring = false;

		public bool bounding = false;

		public bool triumph = false;

		public bool penumbra = false;

		public bool photosynthesis = false;

		public bool astralInjection = false;

		public bool gravityNormalizer = false;

		public bool holyWrath = false;

		public bool profanedRage = false;

		public bool draconicSurge = false;

		public int draconicSurgeCooldown = 0;

		public bool vodka = false;

		public bool redWine = false;

		public bool grapeBeer = false;

		public bool moonshine = false;

		public bool rum = false;

		public bool whiskey = false;

		public bool fireball = false;

		public bool everclear = false;

		public bool bloodyMary = false;

		public bool tequila = false;

		public bool caribbeanRum = false;

		public bool cinnamonRoll = false;

		public bool tequilaSunrise = false;

		public bool margarita = false;

		public bool starBeamRye = false;

		public bool screwdriver = false;

		public bool moscowMule = false;

		public bool whiteWine = false;

		public bool evergreenGin = false;

		public bool tranquilityCandle = false;

		public bool chaosCandle = false;

		public bool purpleCandle = false;

		public bool blueCandle = false;

		public bool pinkCandle = false;

		public int pinkCandleTimer = 60;

		public bool yellowCandle = false;

		public bool trippy = false;

		public bool amidiasBlessing = false;
		#endregion

		#region SummonStuff
		public bool glSword = false;

		public bool mWorm = false;

		public bool iClasper = false;

		public bool herring = false;

		public bool calamari = false;

		public bool cEyes = false;

		public bool cSlime = false;

		public bool cSlime2 = false;

		public bool bStar = false;

		public bool aStar = false;

		public bool SP = false;

		public bool dCreeper = false;

		public bool bClot = false;

		public bool eAxe = false;

		public bool SPG = false;

		public bool aChicken = false;

		public bool cLamp = false;

		public bool pGuy = false;

		public bool gDefense = false;

		public bool gOffense = false;

		public bool gHealer = false;

		public bool cEnergy = false;

		public bool sWaifu = false;

		public bool dWaifu = false;

		public bool cWaifu = false;

		public bool bWaifu = false;

		public bool slWaifu = false;

		public bool fClump = false;

		public bool rDevil = false;

		public bool aValkyrie = false;

		public bool sCrystal = false;

		public bool sGod = false;

		public bool sandnado = false;

		public bool vUrchin = false;

		public bool cSpirit = false;

		public bool rOrb = false;

		public bool dCrystal = false;

		public bool sandWaifu = false;

		public bool sandBoobWaifu = false;

		public bool cloudWaifu = false;

		public bool brimstoneWaifu = false;

		public bool sirenWaifu = false;

		public bool allWaifus = false;

		public bool fungalClump = false;

		public bool redDevil = false;

		public bool valkyrie = false;

		public bool slimeGod = false;

		public bool urchin = false;

		public bool chaosSpirit = false;

		public bool reaverOrb = false;

		public bool daedalusCrystal = false;

		public int healCounter = 300;

		public bool shellfish = false;

		public bool hCrab = false;
		#endregion

		#region BiomeStuff
		public bool ZoneCalamity => Player.InModBiome(ModContent.GetInstance<Crag>());

		public bool ZoneAstral => Player.InModBiome(ModContent.GetInstance<Astral>());

		public bool ZoneSunkenSea => Player.InModBiome(ModContent.GetInstance<SunkenSea>());

		public bool ZoneSulphur => Player.InModBiome(ModContent.GetInstance<Sulphur>());

		public bool ZoneAbyss => ZoneAbyssLayer1 || ZoneAbyssLayer2 || ZoneAbyssLayer3 || ZoneAbyssLayer4;

		public bool ZoneAbyssLayer1 => Player.InModBiome(ModContent.GetInstance<AbyssLayer1Biome>());

		public bool ZoneAbyssLayer2 => Player.InModBiome(ModContent.GetInstance<AbyssLayer2Biome>());

		public bool ZoneAbyssLayer3 => Player.InModBiome(ModContent.GetInstance<AbyssLayer3Biome>());

		public bool ZoneAbyssLayer4 => Player.InModBiome(ModContent.GetInstance<AbyssLayer4Biome>());

		public int abyssBreathCD;
		#endregion

		#region TransformationStuff
		public bool abyssalDivingSuitPrevious;

		public bool abyssalDivingSuit;

		public bool abyssalDivingSuitHide;

		public bool abyssalDivingSuitForce;

		public bool abyssalDivingSuitPower;

		public bool sirenBoobsPrevious;

		public bool sirenBoobs;

		public bool sirenBoobsHide;

		public bool sirenBoobsForce;

		public bool sirenBoobsPower;

		public bool sirenBoobsAltPrevious;

		public bool sirenBoobsAlt;

		public bool sirenBoobsAltHide;

		public bool sirenBoobsAltForce;

		public bool sirenBoobsAltPower;

		public bool snowmanPrevious;

		public bool snowman;

		public bool snowmanHide;

		public bool snowmanForce;

		public bool snowmanNoseless;

		public bool snowmanPower;
		#endregion
		#endregion

		#region SavingAndLoading
		public override void Initialize()
		{
			extraAccessoryML = false;
			eCore = false;
			mFruit = false;
			bOrange = false;
			eBerry = false;
			dFruit = false;
			pHeart = false;
			cShard = false;
			revJamDrop = false;
			rageBoostOne = false;
			rageBoostTwo = false;
			rageBoostThree = false;
			adrenalineBoostOne = false;
			adrenalineBoostTwo = false;
			adrenalineBoostThree = false;
			drawBossHPBar = true;
			shouldDrawSmallText = true;
		}

		public override void SaveData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
		{
			var boost = new List<string>();
			if (extraAccessoryML) boost.Add("extraAccessoryML");
			if (eCore) boost.Add("etherealCore");
			if (mFruit) boost.Add("miracleFruit");
			if (bOrange) boost.Add("bloodOrange");
			if (eBerry) boost.Add("elderBerry");
			if (dFruit) boost.Add("dragonFruit");
			if (pHeart) boost.Add("phantomHeart");
			if (cShard) boost.Add("cometShard");
			if (revJamDrop) boost.Add("revJam");
			if (rageBoostOne) boost.Add("rageOne");
			if (rageBoostTwo) boost.Add("rageTwo");
			if (rageBoostThree) boost.Add("rageThree");
			if (adrenalineBoostOne) boost.Add("adrenalineOne");
			if (adrenalineBoostTwo) boost.Add("adrenalineTwo");
			if (adrenalineBoostThree) boost.Add("adrenalineThree");
			if (drawBossHPBar) boost.Add("bossHPBar");
			if (shouldDrawSmallText) boost.Add("drawSmallText");

			tag["boost"] = boost;
			tag["stress"] = stress;
			tag["adrenaline"] = adrenaline;
			tag["sCalDeathCount"] = sCalDeathCount;
			tag["sCalKillCount"] = sCalKillCount;
			tag["meleeLevel"] = meleeLevel;
			tag["exactMeleeLevel"] = exactMeleeLevel;
			tag["rangedLevel"] = rangedLevel;
			tag["exactRangedLevel"] = exactRangedLevel;
			tag["magicLevel"] = magicLevel;
			tag["exactMagicLevel"] = exactMagicLevel;
			tag["summonLevel"] = summonLevel;
			tag["exactSummonLevel"] = exactSummonLevel;
			tag["rogueLevel"] = rogueLevel;
			tag["exactRogueLevel"] = exactRogueLevel;
			tag["deathCount"] = deathCount;
		}

		public override void LoadData(TagCompound tag)
		{
			var boost = tag.GetList<string>("boost");
			extraAccessoryML = boost.Contains("extraAccessoryML");
			eCore = boost.Contains("etherealCore");
			mFruit = boost.Contains("miracleFruit");
			bOrange = boost.Contains("bloodOrange");
			eBerry = boost.Contains("elderBerry");
			dFruit = boost.Contains("dragonFruit");
			pHeart = boost.Contains("phantomHeart");
			cShard = boost.Contains("cometShard");
			revJamDrop = boost.Contains("revJam");
			rageBoostOne = boost.Contains("rageOne");
			rageBoostTwo = boost.Contains("rageTwo");
			rageBoostThree = boost.Contains("rageThree");
			adrenalineBoostOne = boost.Contains("adrenalineOne");
			adrenalineBoostTwo = boost.Contains("adrenalineTwo");
			adrenalineBoostThree = boost.Contains("adrenalineThree");
			drawBossHPBar = boost.Contains("bossHPBar");
			shouldDrawSmallText = boost.Contains("drawSmallText");

			stress = tag.GetInt("stress");
			adrenaline = tag.GetInt("adrenaline");
			sCalDeathCount = tag.GetInt("sCalDeathCount");
			sCalKillCount = tag.GetInt("sCalKillCount");
			deathCount = tag.GetInt("deathCount");

			meleeLevel = tag.GetInt("meleeLevel");
			rangedLevel = tag.GetInt("rangedLevel");
			magicLevel = tag.GetInt("magicLevel");
			summonLevel = tag.GetInt("summonLevel");
			rogueLevel = tag.GetInt("rogueLevel");
			exactMeleeLevel = tag.GetInt("exactMeleeLevel");
			exactRangedLevel = tag.GetInt("exactRangedLevel");
			exactMagicLevel = tag.GetInt("exactMagicLevel");
			exactSummonLevel = tag.GetInt("exactSummonLevel");
			exactRogueLevel = tag.GetInt("exactRogueLevel");
		}
		#endregion

		#region ResetEffects
		public override void ResetEffects()
		{
			if (extraAccessoryML)
			{
				Player.extraAccessorySlots = 1;
			}
			if (extraAccessoryML && Player.extraAccessory && (Main.expertMode || Main.gameMenu))
			{
				Player.extraAccessorySlots = 2;
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				if (Config.BossRushAccessoryCurse)
				{
					Player.extraAccessorySlots = 0;
				}
			}

			rogueStealthMax = 0f;

			dashMod = 0;
			alcoholPoisonLevel = 0;

			akato = false;
			leviPet = false;
			sirenPet = false;
			fox = false;
			chibii = false;
			brimling = false;
			bearPet = false;
			kendra = false;
			trashMan = false;
			onyxExcavator = false;
			angryDog = false;
			fab = false;
			crysthamyr = false;

			abyssalDivingSuitPlates = false;
			abyssalDivingSuitCooldown = false;

			sirenWaterBuff = false;
			sirenIce = false;
			sirenIceCooldown = false;

			draedonsHeart = false;
			draedonsStressGain = false;

			afflicted = false;
			affliction = false;

			fasterMeleeLevel = false;
			fasterRangedLevel = false;
			fasterMagicLevel = false;
			fasterSummonLevel = false;
			fasterRogueLevel = false;

			dodgeScarf = false;
			scarfCooldown = false;

			elysianAegis = false;

			nCore = false;

			godSlayer = false;
			godSlayerDamage = false;
			godSlayerMage = false;
			godSlayerRanged = false;
			godSlayerThrowing = false;
			godSlayerSummon = false;
			godSlayerReflect = false;
			godSlayerCooldown = false;

			silvaSet = false;
			silvaMelee = false;
			silvaRanged = false;
			silvaThrowing = false;
			silvaMage = false;
			silvaSummon = false;

			auricSet = false;
			auricBoost = false;

			omegaBlueChestplate = false;
			omegaBlueSet = false;
			omegaBlueHentai = false;

			molluskSet = false;

			ataxiaBolt = false;
			ataxiaGeyser = false;
			ataxiaFire = false;
			ataxiaVolley = false;
			ataxiaBlaze = false;
			ataxiaMage = false;

			shadeRegen = false;

			flamethrowerBoost = false;

			shadowSpeed = false;
			dsSetBonus = false;

			desertScourgeLore = false;
			eaterOfWorldsLore = false;
			hiveMindLore = false;
			perforatorLore = false;
			queenBeeLore = false;
			skeletronLore = false;
			wallOfFleshLore = false;
			destroyerLore = false;
			aquaticScourgeLore = false;
			skeletronPrimeLore = false;
			brimstoneElementalLore = false;
			calamitasLore = false;
			planteraLore = false;
			leviathanAndSirenLore = false;
			astrumAureusLore = false;
			astrumDeusLore = false;
			golemLore = false;
			plaguebringerGoliathLore = false;
			dukeFishronLore = false;
			ravagerLore = false;
			lunaticCultistLore = false;
			moonLordLore = false;
			providenceLore = false;
			polterghastLore = false;
			DoGLore = false;
			yharonLore = false;
			SCalLore = false;

			luxorsGift = false;
			fungalSymbiote = false;
			trinketOfChi = false;
			gladiatorSword = false;
			unstablePrism = false;
			regenator = false;
			deepDiver = false;
			theBee = false;
			rBrain = false;
			bloodyWormTooth = false;
			rampartOfDeities = false;
			vexation = false;
			fBulwark = false;
			badgeOfBravery = false;
			badgeOfBraveryRare = false;
			aSpark = false;
			aSparkRare = false;
			aBulwark = false;
			aBulwarkRare = false;
			dAmulet = false;
			fCarapace = false;
			gShell = false;
			absorber = false;
			aAmpoule = false;
			pAmulet = false;
			fBarrier = false;
			aBrain = false;
			frostFlare = false;
			beeResist = false;
			uberBees = false;
			projRef = false;
			projRefRare = false;
			nanotech = false;
			eQuiver = false;
			cryogenSoul = false;
			yInsignia = false;
			eGauntlet = false;
			eTalisman = false;
			statisBeltOfCurses = false;
			heartOfDarkness = false;
			shadowMinions = false;
			tearMinions = false;
			alchFlask = false;
			community = false;
			stressPills = false;
			laudanum = false;
			fleshTotem = false;
			bloodPact = false;
			bloodflareCore = false;
			coreOfTheBloodGod = false;
			elementalHeart = false;
			crownJewel = false;
			celestialJewel = false;
			astralArcanum = false;
			harpyRing = false;
			darkSunRing = false;
			calamityRing = false;
			eArtifact = false;
			dArtifact = false;
			gArtifact = false;
			pArtifact = false;
			giantPearl = false;
			normalityRelocator = false;
			fabledTortoise = false;
			lol = false;
			raiderTalisman = false;
			sGenerator = false;
			sDefense = false;
			sRegen = false;
			sPower = false;
			IBoots = false;
			elysianFire = false;
			sTracers = false;
			eTracers = false;
			cTracers = false;

			daedalusReflect = false;
			daedalusSplit = false;
			daedalusAbsorb = false;
			daedalusShard = false;

			reaverSpore = false;
			reaverDoubleTap = false;
			reaverBlast = false;
			reaverBurst = false;

			ironBoots = false;
			depthCharm = false;
			anechoicPlating = false;
			jellyfishNecklace = false;
			abyssalAmulet = false;
			lumenousAmulet = false;
			reaperToothNecklace = false;
			aquaticEmblem = false;

			astralStarRain = false;

			victideSet = false;

			aeroSet = false;

			statigelSet = false;

			tarraSet = false;
			tarraMelee = false;
			tarraMage = false;
			tarraRanged = false;
			tarraThrowing = false;
			tarraSummon = false;

			bloodflareSet = false;
			bloodflareMelee = false;
			bloodflareRanged = false;
			bloodflareThrowing = false;
			bloodflareMage = false;
			bloodflareSummon = false;

			xerocSet = false;

			NOU = false;
			weakPetrification = false;

			lethalLavaBurn = false;
			aCrunch = false;
			hAttack = false;
			horror = false;
			irradiated = false;
			bFlames = false;
			aFlames = false;
			gsInferno = false;
			pFlames = false;
			hFlames = false;
			hInferno = false;
			gState = false;
			bBlood = false;
			eGravity = false;
			vHex = false;
			eGrav = false;
			warped = false;
			marked = false;
			cDepth = false;
			fishAlert = false;
			bOut = false;
			clamity = false;
			enraged = false;
			snowmanNoseless = false;

			trinketOfChiBuff = false;
			corrEffigy = false;
			crimEffigy = false;
			rRage = false;
			xRage = false;
			xWrath = false;
			graxDefense = false;
			sMeleeBoost = false;
			tFury = false;
			cadence = false;
			omniscience = false;
			zerg = false;
			zen = false;
			permafrostsConcoction = false;
			armorCrumbling = false;
			armorShattering = false;
			ceaselessHunger = false;
			calcium = false;
			soaring = false;
			bounding = false;
			triumph = false;
			penumbra = false;
			photosynthesis = false;
			astralInjection = false;
			gravityNormalizer = false;
			holyWrath = false;
			profanedRage = false;
			draconicSurge = false;
			trippy = false;
			amidiasBlessing = false;
			yPower = false;
			aWeapon = false;
			tScale = false;
			fabsolVodka = false;
			invincible = false;
			shine = false;
			anechoicCoating = false;
			mushy = false;
			molten = false;
			shellBoost = false;
			cFreeze = false;
			tRegen = false;

			vodka = false;
			redWine = false;
			grapeBeer = false;
			moonshine = false;
			rum = false;
			whiskey = false;
			fireball = false;
			everclear = false;
			bloodyMary = false;
			tequila = false;
			caribbeanRum = false;
			cinnamonRoll = false;
			tequilaSunrise = false;
			margarita = false;
			starBeamRye = false;
			screwdriver = false;
			moscowMule = false;
			whiteWine = false;
			evergreenGin = false;

			tranquilityCandle = false;
			chaosCandle = false;
			purpleCandle = false;
			blueCandle = false;
			pinkCandle = false;
			yellowCandle = false;

			glSword = false;
			mWorm = false;
			iClasper = false;
			herring = false;
			calamari = false;
			cEyes = false;
			cSlime = false;
			cSlime2 = false;
			bStar = false;
			aStar = false;
			SP = false;
			dCreeper = false;
			bClot = false;
			eAxe = false;
			SPG = false;
			aChicken = false;
			cLamp = false;
			pGuy = false;
			cEnergy = false;
			gDefense = false;
			gOffense = false;
			gHealer = false;
			sWaifu = false;
			dWaifu = false;
			cWaifu = false;
			bWaifu = false;
			slWaifu = false;
			fClump = false;
			rDevil = false;
			aValkyrie = false;
			sCrystal = false;
			sGod = false;
			sandnado = false;
			vUrchin = false;
			cSpirit = false;
			rOrb = false;
			dCrystal = false;
			sandWaifu = false;
			sandBoobWaifu = false;
			cloudWaifu = false;
			brimstoneWaifu = false;
			sirenWaifu = false;
			allWaifus = false;
			fungalClump = false;
			redDevil = false;
			valkyrie = false;
			slimeGod = false;
			urchin = false;
			chaosSpirit = false;
			reaverOrb = false;
			daedalusCrystal = false;
			shellfish = false;
			hCrab = false;

			abyssalDivingSuitPrevious = abyssalDivingSuit;
			abyssalDivingSuit = abyssalDivingSuitHide = abyssalDivingSuitForce = abyssalDivingSuitPower = false;

			sirenBoobsPrevious = sirenBoobs;
			sirenBoobs = sirenBoobsHide = sirenBoobsForce = sirenBoobsPower = false;
			sirenBoobsAltPrevious = sirenBoobsAlt;
			sirenBoobsAlt = sirenBoobsAltHide = sirenBoobsAltForce = sirenBoobsAltPower = false;

			snowmanPrevious = snowman;
			snowman = snowmanHide = snowmanForce = snowmanPower = false;

			rageMode = false;
			adrenalineMode = false;
		}
		#endregion

		#region UpdateDead
		public override void UpdateDead()
		{
			#region Debuffs
			stress = 0;
			adrenaline = 0;
			adrenalineMaxTimer = 300;
			adrenalineDmgDown = 600;
			adrenalineDmgMult = 1f;
			raiderStack = 0;
			fleshTotemCooldown = 0;
			astralStarRainCooldown = 0;
			bloodflareMageCooldown = 0;
			tarraMageHealCooldown = 0;
			ataraxiaDamageBoost = 0f;
			bossRushImmunityFrameCurseTimer = 0;
			aBulwarkRareMeleeBoostTimer = 0;
			ataraxiaDamageBoostCooldown = 0;
			ataraxiaDamageBoostCancelTimer = 1200;
			theBeeDamage = 0;
			lethalLavaBurn = false;
			aCrunch = false;
			hAttack = false;
			horror = false;
			irradiated = false;
			bFlames = false;
			aFlames = false;
			gsInferno = false;
			pFlames = false;
			hFlames = false;
			hInferno = false;
			gState = false;
			bBlood = false;
			eGravity = false;
			vHex = false;
			eGrav = false;
			warped = false;
			marked = false;
			cDepth = false;
			fishAlert = false;
			bOut = false;
			clamity = false;
			snowmanNoseless = false;
			scarfCooldown = false;
			godSlayerCooldown = false;
			abyssalDivingSuitCooldown = false;
			abyssalDivingSuitPlateHits = 0;
			sirenIceCooldown = false;
			#endregion

			#region Buffs
			sDefense = false;
			sRegen = false;
			sPower = false;
			onyxExcavator = false;
			angryDog = false;
			fab = false;
			crysthamyr = false;
			abyssalDivingSuitPlates = false;
			sirenWaterBuff = false;
			sirenIce = false;
			trinketOfChiBuff = false;
			chiBuffTimer = 0;
			corrEffigy = false;
			crimEffigy = false;
			rRage = false;
			xRage = false;
			xWrath = false;
			graxDefense = false;
			sMeleeBoost = false;
			tFury = false;
			cadence = false;
			omniscience = false;
			zerg = false;
			zen = false;
			permafrostsConcoction = false;
			armorCrumbling = false;
			armorShattering = false;
			ceaselessHunger = false;
			calcium = false;
			soaring = false;
			bounding = false;
			triumph = false;
			penumbra = false;
			photosynthesis = false;
			astralInjection = false;
			gravityNormalizer = false;
			holyWrath = false;
			profanedRage = false;
			draconicSurge = false;
			draconicSurgeCooldown = 0;
			yPower = false;
			aWeapon = false;
			tScale = false;
			fabsolVodka = false;
			invincible = false;
			shine = false;
			anechoicCoating = false;
			mushy = false;
			molten = false;
			enraged = false;
			shellBoost = false;
			cFreeze = false;
			tRegen = false;
			rageMode = false;
			adrenalineMode = false;
			vodka = false;
			redWine = false;
			grapeBeer = false;
			moonshine = false;
			rum = false;
			whiskey = false;
			fireball = false;
			everclear = false;
			bloodyMary = false;
			tequila = false;
			caribbeanRum = false;
			cinnamonRoll = false;
			tequilaSunrise = false;
			margarita = false;
			starBeamRye = false;
			screwdriver = false;
			moscowMule = false;
			whiteWine = false;
			evergreenGin = false;
			tranquilityCandle = false;
			chaosCandle = false;
			purpleCandle = false;
			blueCandle = false;
			pinkCandle = false;
			pinkCandleTimer = 60;
			yellowCandle = false;
			trippy = false;
			amidiasBlessing = false;
			revivifyTimer = 0;
			healCounter = 300;
			#endregion

			#region Armorbonuses
			flamethrowerBoost = false;
			shadowSpeed = false;
			godSlayer = false;
			godSlayerDamage = false;
			godSlayerMage = false;
			godSlayerRanged = false;
			godSlayerThrowing = false;
			godSlayerSummon = false;
			godSlayerReflect = false;
			auricBoost = false;
			silvaSet = false;
			silvaMelee = false;
			silvaRanged = false;
			silvaThrowing = false;
			silvaMage = false;
			silvaSummon = false;
			hasSilvaEffect = false;
			silvaCountdown = 600;
			silvaHitCounter = 0;
			auricSet = false;
			omegaBlueChestplate = false;
			omegaBlueSet = false;
			omegaBlueCooldown = 0;
			molluskSet = false;
			daedalusReflect = false;
			daedalusSplit = false;
			daedalusAbsorb = false;
			daedalusShard = false;
			reaverSpore = false;
			reaverDoubleTap = false;
			shadeRegen = false;
			dsSetBonus = false;
			reaverBlast = false;
			reaverBurst = false;
			astralStarRain = false;
			ataxiaMage = false;
			ataxiaBolt = false;
			ataxiaGeyser = false;
			ataxiaFire = false;
			ataxiaVolley = false;
			ataxiaBlaze = false;
			victideSet = false;
			aeroSet = false;
			statigelSet = false;
			tarraSet = false;
			tarraMelee = false;
			tarraMage = false;
			tarraRanged = false;
			tarraThrowing = false;
			tarraThrowingCrits = 0;
			tarraThrowingCritTimer = 0;
			tarraSummon = false;
			bloodflareSet = false;
			bloodflareMelee = false;
			bloodflareMeleeHits = 0;
			bloodflareFrenzyTimer = 0;
			bloodflareFrenzyCooldown = 0;
			bloodflareRanged = false;
			bloodflareRangedCooldown = 0;
			bloodflareThrowing = false;
			bloodflareMage = false;
			bloodflareSummon = false;
			bloodflareSummonTimer = 0;
			xerocSet = false;
			IBoots = false;
			elysianFire = false;
			elysianAegis = false;
			elysianGuard = false;
			#endregion

			if (CalamityWorldPreTrailer.bossRushActive)
			{
				if (!CalamityGlobalNPC.AnyLivingPlayers())
				{
					CalamityWorldPreTrailer.bossRushActive = false;
					CalamityWorldPreTrailer.bossRushStage = 0;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						var netMessage = Mod.GetPacket();
						netMessage.Write((byte)CalamityModClassicPreTrailerMessageType.BossRushStage);
						netMessage.Write(CalamityWorldPreTrailer.bossRushStage);
						netMessage.Send();
					}
					for (int doom = 0; doom < 200; doom++)
					{
						if (Main.npc[doom].active && Main.npc[doom].boss)
						{
							Main.npc[doom].active = false;
							Main.npc[doom].netUpdate = true;
						}
					}
				}
			}
			if (CalamityWorldPreTrailer.armageddon && !areThereAnyDamnBosses)
			{
				Player.respawnTimer -= 5;
			}
			else if (Player.respawnTimer > 300 && Main.expertMode) //600 normal 900 expert
			{
				Player.respawnTimer--;
			}
		}
		#endregion

		#region InventoryStartup
		public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)/* tModPorter Suggestion: Return an Item array to add to the players starting items. Use ModifyStartingInventory for modifying them if needed */
		{
			Item rev = new Item();
			rev.SetDefaults(Mod.Find<ModItem>("Revenge").Type);
			Item iHeart = new Item();
			iHeart.SetDefaults(Mod.Find<ModItem>("IronHeart").Type);
			Item starterBag = new Item();
			starterBag.SetDefaults(Mod.Find<ModItem>("StarterBag").Type);
			if (!mediumCoreDeath)
			{
				return new Item[]
				{
					rev,
					iHeart,
					starterBag
				};
			}
			return null;
		}
		#endregion

		#region LifeRegen
		public override void UpdateBadLifeRegen()
		{
			Point point = Player.Center.ToTileCoordinates();
			#region FirstDebuffs
			if (bFlames || aFlames)
			{
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 16;
			}
			if (gsInferno || (ZoneCalamity && Player.lavaWet))
			{
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 30;
			}
			if (ZoneSulphur && Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir) && !aquaticScourgeLore)
			{
				Player.AddBuff(BuffID.Poisoned, 2, true);
				pissWaterBoost++;
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				if (pissWaterBoost > 1800)
				{
					Player.lifeRegen -= 6;
				}
				else if (pissWaterBoost > 1440)
				{
					Player.lifeRegen -= 4;
				}
				else if (pissWaterBoost > 1080)
				{
					Player.lifeRegen -= 3;
				}
				else if (pissWaterBoost > 720)
				{
					Player.lifeRegen -= 2;
				}
				else if (pissWaterBoost > 360)
				{
					Player.lifeRegen -= 1;
				}
			}
			else
			{
				pissWaterBoost = 0;
			}
			if (hFlames)
			{
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 20;
			}
			if (pFlames)
			{
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 20;
				Player.blind = true;
				Player.statDefense -= 8;
				Player.moveSpeed -= 0.15f;
			}
			if (bBlood)
			{
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 8;
				Player.blind = true;
				Player.statDefense -= 3;
				Player.moveSpeed += 0.2f;
				Player.GetDamage(DamageClass.Melee) += 0.05f;
				Player.GetDamage(DamageClass.Ranged) -= 0.1f;
				Player.GetDamage(DamageClass.Magic) -= 0.1f;
			}
			if (horror)
			{
				Player.blind = true;
				Player.statDefense -= 15;
				Player.moveSpeed -= 0.15f;
			}
			if (aCrunch)
			{
				Player.statDefense /= 3;
				Player.endurance *= 0.33f;
			}
			if (vHex)
			{
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 16;
				Player.blind = true;
				Player.statDefense -= 30;
				Player.moveSpeed -= 0.1f;
				if (Player.wingTimeMax <= 0)
				{
					Player.wingTimeMax = 0;
				}
				Player.wingTimeMax /= 2;
			}
			if (cDepth)
			{
				if (Player.statDefense > 0)
				{
					int depthDamage = depthCharm ? 9 : 18;
					int subtractDefense = (int)((double)Player.statDefense * 0.05); //240 defense = 0 damage taken with depth charm
					int calcDepthDamage = depthDamage - subtractDefense;
					if (calcDepthDamage < 0)
					{
						calcDepthDamage = 0;
					}
					if (Player.lifeRegen > 0)
					{
						Player.lifeRegen = 0;
					}
					Player.lifeRegenTime = 0;
					Player.lifeRegen -= calcDepthDamage;
				}
			}
			#endregion
			#region Buffs
			if (tRegen)
			{
				Player.lifeRegen += 3;
			}
			if (sRegen)
			{
				Player.lifeRegen += 2;
			}
			if (tarraSet)
			{
				Player.calmed = (tarraMelee ? false : true);
				Player.lifeMagnet = true;
			}
			if (aChicken)
			{
				Player.lifeRegen += 1;
				Player.statDefense += 5;
				Player.moveSpeed += 0.1f;
			}
			if (cadence)
			{
				Player.discountAvailable = true;
				Player.lifeMagnet = true;
				Player.calmed = true;
				Player.loveStruck = true;
				Player.lifeRegen += 2;
				Player.statLifeMax2 += Player.statLifeMax / 5 / 20 * 10;
			}
			if (omniscience)
			{
				Player.detectCreature = true;
				Player.dangerSense = true;
				Player.findTreasure = true;
			}
			if (aWeapon)
			{
				Player.moveSpeed += 0.15f;
			}
			if (mushy)
			{
				Player.statDefense += 5;
				Player.lifeRegen += 5;
			}
			if (molten)
			{
				Player.resistCold = true;
			}
			if (shellBoost)
			{
				Player.moveSpeed += 0.9f;
			}
			if (celestialJewel || astralArcanum)
			{
				bool lesserEffect = false;
				for (int l = 0; l < 22; l++)
				{
					int hasBuff = Player.buffType[l];
					bool shouldAffect = CalamityModClassicPreTrailer.alcoholList.Contains(hasBuff);
					if (shouldAffect)
					{
						lesserEffect = true;
					}
				}
				if (lesserEffect)
				{
					Player.lifeRegen += 1;
					Player.statDefense += 20;
				}
				else
				{
					if (Player.lifeRegen < 0)
					{
						if (Player.lifeRegenTime < 1800)
						{
							Player.lifeRegenTime = 1800;
						}
						Player.lifeRegen += 4;
						Player.statDefense += 20;
					}
					else
					{
						Player.lifeRegen += 2;
					}
				}
			}
			else if (crownJewel)
			{
				bool lesserEffect = false;
				for (int l = 0; l < 22; l++)
				{
					int hasBuff = Player.buffType[l];
					bool shouldAffect = CalamityModClassicPreTrailer.alcoholList.Contains(hasBuff);
					if (shouldAffect)
					{
						lesserEffect = true;
					}
				}
				if (lesserEffect)
				{
					Player.statDefense += 10;
				}
				else
				{
					if (Player.lifeRegen < 0)
					{
						if (Player.lifeRegenTime < 1800)
						{
							Player.lifeRegenTime = 1800;
						}
						Player.lifeRegen += 2;
						Player.statDefense += 10;
					}
					else
					{
						Player.lifeRegen += 1;
					}
				}
			}
			if (permafrostsConcoction)
			{
				if (Player.statLife < Player.statLifeMax2 / 2)
					Player.lifeRegen++;
				if (Player.statLife < Player.statLifeMax2 / 4)
					Player.lifeRegen++;
				if (Player.statLife < Player.statLifeMax2 / 10)
					Player.lifeRegen += 2;
				if (Player.poisoned || Player.onFire || bFlames)
					Player.lifeRegen += 4;
			}
			if (CalamityCollisionPreTrailer.HotWetCollision(Player.position, Player.width, Player.height) || (ZoneAbyssLayer4 && point.Y > Main.maxTilesY - 300) || ZoneSunkenSea)
			{
				if (Player.lifeRegen < 0)
				{
					Player.lifeRegen += 2;
					if (Player.lifeRegen > 0)
					{
						Player.lifeRegen = 0;
					}
				}
				Player.lifeRegenTime += 1;
				Player.lifeRegen += 1;
			}
			#endregion
			#region LastDebuffs
			if (omegaBlueChestplate)
			{
				if (Player.lifeRegen > 0)
					Player.lifeRegen = 0;

				Player.lifeRegenTime = 0;

				if (Player.lifeRegenCount > 0)
					Player.lifeRegenCount = 0;
			}
			if (Config.LethalLava)
			{
				if (Main.myPlayer == Player.whoAmI)
				{
					if (Collision.LavaCollision(Player.position, Player.width, (Player.waterWalk ? (Player.height - 6) : Player.height)))
					{
						if (Player.lavaImmune && !Player.immune)
						{
							if (Player.lavaTime > 0)
							{
								Player.lavaTime--;
							}
						}
						if (Player.lavaTime <= 0)
						{
							Player.AddBuff(Mod.Find<ModBuff>("LethalLavaBurn").Type, 2, true);
						}
					}
				}
				if (lethalLavaBurn)
				{
					if (Player.lifeRegen > 0)
					{
						Player.lifeRegen = 0;
					}
					Player.lifeRegenTime = 0;
					int lifeRegenDown = (Player.lavaImmune ? 9 : 18);
					if (Player.lavaRose)
					{
						lifeRegenDown = 3;
					}
					Player.lifeRegen -= lifeRegenDown;
				}
			}
			if (hInferno)
			{
				hInfernoBoost++;
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				if (hInfernoBoost > 240)
				{
					Player.lifeRegen -= 192;
				}
				else if (hInfernoBoost > 180)
				{
					Player.lifeRegen -= 96;
				}
				else if (hInfernoBoost > 120)
				{
					Player.lifeRegen -= 48;
				}
				else if (hInfernoBoost > 60)
				{
					Player.lifeRegen -= 24;
				}
				else if (hInfernoBoost > 0)
				{
					Player.lifeRegen -= 12;
				}
			}
			else
			{
				hInfernoBoost = 0;
			}
			if (gState)
			{
				Player.statDefense /= 2;
				Player.velocity.Y = 0f;
				Player.velocity.X = 0f;
			}
			if (eGravity)
			{
				Player.velocity.X *= 0.99f;
				if (Player.wingTimeMax < 0)
				{
					Player.wingTimeMax = 0;
				}
				Player.wingTimeMax /= 4;
				if (Player.wingTimeMax > 400)
				{
					Player.wingTimeMax = 100;
				}
			}
			if (eGrav)
			{
				Player.velocity.X *= 0.99f;
				if (Player.wingTimeMax < 0)
				{
					Player.wingTimeMax = 0;
				}
				Player.wingTimeMax /= 3;
				if (Player.wingTimeMax > 400)
				{
					Player.wingTimeMax = 200;
				}
			}
			if (warped)
			{
				Player.velocity.X *= 0.99f;
			}
			if (molluskSet)
			{
				Player.velocity.X *= 0.985f;
			}
			if ((warped || caribbeanRum) && !Player.slowFall)
			{
				Player.velocity.Y *= 1.01f;
			}
			if (weakPetrification || CalamityWorldPreTrailer.bossRushActive)
			{
				if (Player.mount.Active)
					Player.mount.Dismount(Player);
			}
			if (silvaCountdown > 0 && hasSilvaEffect && silvaSet)
			{
				if (Player.lifeRegen < 0)
					Player.lifeRegen = 0;
			}
			#endregion
		}

		#region UpdateLifeRegen
		public override void UpdateLifeRegen()
		{
			if (!Player.shinyStone)
			{
				int lifeRegenTimeMaxBoost = (areThereAnyDamnBosses ? 450 : 1800);
				int lifeRegenMaxBoost = (areThereAnyDamnBosses ? 1 : 4);
				float lifeRegenLifeRegenTimeMaxBoost = (areThereAnyDamnBosses ? 8f : 30f);
				if ((double)Math.Abs(Player.velocity.X) < 0.05 && (double)Math.Abs(Player.velocity.Y) < 0.05 && Player.itemAnimation == 0)
				{
					if (shadeRegen)
					{
						if (Player.lifeRegenTime > 90 && Player.lifeRegenTime < lifeRegenTimeMaxBoost)
						{
							Player.lifeRegenTime = lifeRegenTimeMaxBoost;
						}
						Player.lifeRegenTime += lifeRegenMaxBoost;
						Player.lifeRegen += lifeRegenMaxBoost;
						float num3 = (float)((double)Player.lifeRegenTime * 2.5); //lifeRegenTime max is 3600
						num3 /= 300f;
						if (num3 > 0f)
						{
							if (num3 > lifeRegenLifeRegenTimeMaxBoost)
							{
								num3 = lifeRegenLifeRegenTimeMaxBoost;
							}
							Player.lifeRegen += (int)num3;
						}
						if (Player.lifeRegen > 0 && Player.statLife < Player.statLifeMax2)
						{
							Player.lifeRegenCount++;
							if ((Main.rand.Next(30000) < Player.lifeRegenTime || Main.rand.Next(30) == 0))
							{
								int num5 = Dust.NewDust(Player.position, Player.width, Player.height, 173, 0f, 0f, 200, default(Color), 1f);
								Main.dust[num5].noGravity = true;
								Main.dust[num5].velocity *= 0.75f;
								Main.dust[num5].fadeIn = 1.3f;
								Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
								vector.Normalize();
								vector *= (float)Main.rand.Next(50, 100) * 0.04f;
								Main.dust[num5].velocity = vector;
								vector.Normalize();
								vector *= 34f;
								Main.dust[num5].position = Player.Center - vector;
							}
						}
					}
					else if (cFreeze)
					{
						if (Player.lifeRegenTime > 90 && Player.lifeRegenTime < lifeRegenTimeMaxBoost)
						{
							Player.lifeRegenTime = lifeRegenTimeMaxBoost;
						}
						Player.lifeRegenTime += lifeRegenMaxBoost;
						Player.lifeRegen += lifeRegenMaxBoost;
						float num3 = (float)((double)Player.lifeRegenTime * 2.5); //lifeRegenTime max is 3600
						num3 /= 300f;
						if (num3 > 0f)
						{
							if (num3 > lifeRegenLifeRegenTimeMaxBoost)
							{
								num3 = lifeRegenLifeRegenTimeMaxBoost;
							}
							Player.lifeRegen += (int)num3;
						}
						if (Player.lifeRegen > 0 && Player.statLife < Player.statLifeMax2)
						{
							Player.lifeRegenCount++;
							if ((Main.rand.Next(30000) < Player.lifeRegenTime || Main.rand.Next(30) == 0))
							{
								int num5 = Dust.NewDust(Player.position, Player.width, Player.height, 67, 0f, 0f, 200, new Color(150, Main.DiscoG, 255), 0.75f);
								Main.dust[num5].noGravity = true;
								Main.dust[num5].velocity *= 0.75f;
								Main.dust[num5].fadeIn = 1.3f;
								Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
								vector.Normalize();
								vector *= (float)Main.rand.Next(50, 100) * 0.04f;
								Main.dust[num5].velocity = vector;
								vector.Normalize();
								vector *= 34f;
								Main.dust[num5].position = Player.Center - vector;
							}
						}
					}
					else if (draedonsHeart)
					{
						if (Player.lifeRegenTime > 90 && Player.lifeRegenTime < lifeRegenTimeMaxBoost)
						{
							Player.lifeRegenTime = lifeRegenTimeMaxBoost;
						}
						Player.lifeRegenTime += lifeRegenMaxBoost;
						Player.lifeRegen += lifeRegenMaxBoost;
						float num3 = (float)((double)Player.lifeRegenTime * 2.5); //lifeRegenTime max is 3600
						num3 /= 300f;
						if (num3 > 0f)
						{
							if (num3 > lifeRegenLifeRegenTimeMaxBoost)
							{
								num3 = lifeRegenLifeRegenTimeMaxBoost;
							}
							Player.lifeRegen += (int)num3;
						}
						if (Player.lifeRegen > 0 && Player.statLife < Player.statLifeMax2)
						{
							Player.lifeRegenCount++;
							if ((Main.rand.Next(30000) < Player.lifeRegenTime || Main.rand.Next(2) == 0))
							{
								int num5 = Dust.NewDust(Player.position, Player.width, Player.height, 107, 0f, 0f, 200, default(Color), 1f);
								Main.dust[num5].noGravity = true;
								Main.dust[num5].velocity *= 0.75f;
								Main.dust[num5].fadeIn = 1.3f;
								Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
								vector.Normalize();
								vector *= (float)Main.rand.Next(50, 100) * 0.04f;
								Main.dust[num5].velocity = vector;
								vector.Normalize();
								vector *= 34f;
								Main.dust[num5].position = Player.Center - vector;
							}
						}
					}
					else if (photosynthesis)
					{
						int lifeRegenTimeMaxBoost2 = Main.dayTime ? lifeRegenTimeMaxBoost : (lifeRegenTimeMaxBoost / 5);
						int lifeRegenMaxBoost2 = Main.dayTime ? lifeRegenMaxBoost : (lifeRegenMaxBoost / 5);
						float lifeRegenLifeRegenTimeMaxBoost2 = Main.dayTime ? lifeRegenLifeRegenTimeMaxBoost : (lifeRegenLifeRegenTimeMaxBoost / 5);
						if (Player.lifeRegenTime > 90 && Player.lifeRegenTime < lifeRegenTimeMaxBoost2)
						{
							Player.lifeRegenTime = lifeRegenTimeMaxBoost2;
						}
						Player.lifeRegenTime += lifeRegenMaxBoost2;
						Player.lifeRegen += lifeRegenMaxBoost2;
						float num3 = (float)((double)Player.lifeRegenTime * 2.5); //lifeRegenTime max is 3600
						num3 /= 300f;
						if (num3 > 0f)
						{
							if (num3 > lifeRegenLifeRegenTimeMaxBoost2)
							{
								num3 = lifeRegenLifeRegenTimeMaxBoost2;
							}
							Player.lifeRegen += (int)num3;
						}
						if (Player.lifeRegen > 0 && Player.statLife < Player.statLifeMax2)
						{
							Player.lifeRegenCount++;
							if ((Main.rand.Next(30000) < Player.lifeRegenTime || Main.rand.Next(2) == 0))
							{
								int num5 = Dust.NewDust(Player.position, Player.width, Player.height, 244, 0f, 0f, 200, default(Color), 1f);
								Main.dust[num5].noGravity = true;
								Main.dust[num5].velocity *= 0.75f;
								Main.dust[num5].fadeIn = 1.3f;
								Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
								vector.Normalize();
								vector *= (float)Main.rand.Next(50, 100) * 0.04f;
								Main.dust[num5].velocity = vector;
								vector.Normalize();
								vector *= 34f;
								Main.dust[num5].position = Player.Center - vector;
							}
						}
					}
				}
			}
		}
		#endregion
		#endregion

		#region HotKeys
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (CalamityModClassicPreTrailer.NormalityRelocatorHotKey.JustPressed && normalityRelocator && Main.myPlayer == Player.whoAmI)
			{
				Vector2 teleportLocation;
				teleportLocation.X = (float)Main.mouseX + Main.screenPosition.X;
				if (Player.gravDir == 1f)
				{
					teleportLocation.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)Player.height;
				}
				else
				{
					teleportLocation.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
				}
				teleportLocation.X -= (float)(Player.width / 2);
				if (teleportLocation.X > 50f && teleportLocation.X < (float)(Main.maxTilesX * 16 - 50) && teleportLocation.Y > 50f && teleportLocation.Y < (float)(Main.maxTilesY * 16 - 50))
				{
					int x = (int)(teleportLocation.X / 16f);
					int y = (int)(teleportLocation.Y / 16f);
					if (!Collision.SolidCollision(teleportLocation, Player.width, Player.height))
					{
						Player.Teleport(teleportLocation, 4, 0);
						NetMessage.SendData(65, -1, -1, null, 0, (float)Player.whoAmI, teleportLocation.X, teleportLocation.Y, 1, 0, 0);
						if (Player.chaosState)
						{
							Player.statLife -= Player.statLifeMax2 / 4;
							PlayerDeathReason damageSource = PlayerDeathReason.ByOther(13);
							if (Main.rand.Next(2) == 0)
							{
								damageSource = PlayerDeathReason.ByOther(Player.Male ? 14 : 15);
							}
							if (Player.statLife <= 0)
							{
								Player.KillMe(damageSource, 1.0, 0, false);
							}
							Player.lifeRegenCount = 0;
							Player.lifeRegenTime = 0;
						}
						Player.AddBuff(BuffID.ChaosState, 360, true);
					}
				}
			}
			if (CalamityModClassicPreTrailer.BossBarToggleHotKey.JustPressed)
			{
				if (drawBossHPBar)
				{
					drawBossHPBar = false;
				}
				else
				{
					drawBossHPBar = true;
				}
			}
			if (CalamityModClassicPreTrailer.BossBarToggleSmallTextHotKey.JustPressed)
			{
				if (shouldDrawSmallText)
				{
					shouldDrawSmallText = false;
				}
				else
				{
					shouldDrawSmallText = true;
				}
			}
			if (CalamityModClassicPreTrailer.TarraHotKey.JustPressed)
			{
				if (tarraMelee && tarraCooldown <= 0)
				{
					tarraDefense = true;
				}
				if (bloodflareRanged && bloodflareRangedCooldown <= 0)
				{
					bloodflareRangedCooldown = 1800;
					SoundEngine.PlaySound(SoundID.Zombie104, Player.position);
					for (int num502 = 0; num502 < 64; num502++)
					{
						int dust = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 16f), Player.width, Player.height - 16, 60, 0f, 0f, 0, default(Color), 1f);
						Main.dust[dust].velocity *= 3f;
						Main.dust[dust].scale *= 1.15f;
					}
					int num226 = 36;
					for (int num227 = 0; num227 < num226; num227++)
					{
						Vector2 vector6 = Vector2.Normalize(Player.velocity) * new Vector2((float)Player.width / 2f, (float)Player.height) * 0.75f;
						vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Player.Center;
						Vector2 vector7 = vector6 - Player.Center;
						int num228 = Dust.NewDust(vector6 + vector7, 0, 0, 60, vector7.X * 1.5f, vector7.Y * 1.5f, 100, default(Color), 1.4f);
						Main.dust[num228].noGravity = true;
						Main.dust[num228].noLight = true;
						Main.dust[num228].velocity = vector7;
					}
					float spread = 45f * 0.0174f;
					double startAngle = Math.Atan2(Player.velocity.X, Player.velocity.Y) - spread / 2;
					double deltaAngle = spread / 8f;
					double offsetAngle;
					int i;
					int damage = 800;
					if (Player.whoAmI == Main.myPlayer)
					{
						for (i = 0; i < 8; i++)
						{
							float ai1 = (Main.rand.NextFloat() + 0.5f);
							float randomSpeed = (float)Main.rand.Next(1, 7);
							float randomSpeed2 = (float)Main.rand.Next(1, 7);
							offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f) + randomSpeed, Mod.Find<ModProjectile>("BloodflareSoul").Type, damage, 0f, Player.whoAmI, 0f, ai1);
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f) + randomSpeed2, Mod.Find<ModProjectile>("BloodflareSoul").Type, damage, 0f, Player.whoAmI, 0f, ai1);
						}
					}
				}
				if (omegaBlueSet && omegaBlueCooldown <= 0)
				{
					omegaBlueCooldown = 1800;
					SoundEngine.PlaySound(SoundID.Zombie104, Player.position);
					for (int i = 0; i < 66; i++)
					{
						int d = Dust.NewDust(Player.position, Player.width, Player.height, 20, 0, 0, 100, Color.Transparent, 2.6f);
						Main.dust[d].noGravity = true;
						Main.dust[d].noLight = true;
						Main.dust[d].fadeIn = 1f;
						Main.dust[d].velocity *= 6.6f;
					}
				}
				if (dsSetBonus)
				{
					SoundEngine.PlaySound(SoundID.Zombie104, Player.position);
					for (int num502 = 0; num502 < 36; num502++)
					{
						int dust = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 16f), Player.width, Player.height - 16, 235, 0f, 0f, 0, default(Color), 1f);
						Main.dust[dust].velocity *= 3f;
						Main.dust[dust].scale *= 1.15f;
					}
					int num226 = 36;
					for (int num227 = 0; num227 < num226; num227++)
					{
						Vector2 vector6 = Vector2.Normalize(Player.velocity) * new Vector2((float)Player.width / 2f, (float)Player.height) * 0.75f;
						vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Player.Center;
						Vector2 vector7 = vector6 - Player.Center;
						int num228 = Dust.NewDust(vector6 + vector7, 0, 0, 235, vector7.X * 1.5f, vector7.Y * 1.5f, 100, default(Color), 1.4f);
						Main.dust[num228].noGravity = true;
						Main.dust[num228].noLight = true;
						Main.dust[num228].velocity = vector7;
					}
					if (Player.whoAmI == Main.myPlayer)
					{
						Player.AddBuff(Mod.Find<ModBuff>("Enraged").Type, 600, false);
					}
					if (Main.netMode != 1)
					{
						for (int l = 0; l < 200; l++)
						{
							NPC nPC = Main.npc[l];
							if (nPC.active && !nPC.friendly && !nPC.dontTakeDamage && Vector2.Distance(Player.Center, nPC.Center) <= 3000f)
							{
								nPC.AddBuff(Mod.Find<ModBuff>("Enraged").Type, 600, false);
							}
						}
					}
				}
			}
			if (CalamityModClassicPreTrailer.AstralArcanumUIHotkey.JustPressed && astralArcanum)
			{
				AstralArcanumUI.Toggle();
			}
			if (CalamityModClassicPreTrailer.AstralTeleportHotKey.JustPressed)
			{
				if (celestialJewel)
				{
					if (Main.netMode == 0)
					{
						Player.TeleportationPotion();
						SoundEngine.PlaySound(SoundID.Item6, Player.position);
					}
					else if (Main.netMode == 1 && Player.whoAmI == Main.myPlayer)
					{
						NetMessage.SendData(73, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
			if (CalamityModClassicPreTrailer.AegisHotKey.JustPressed)
			{
				if (elysianAegis && !Player.mount.Active)
				{
					elysianGuard = !elysianGuard;
				}
			}
			if (CalamityModClassicPreTrailer.RageHotKey.JustPressed)
			{
				if (stress == stressMax && !rageMode)
				{
					SoundEngine.PlaySound(SoundID.Zombie104, Player.position);
					for (int num502 = 0; num502 < 64; num502++)
					{
						int dust = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 16f), Player.width, Player.height - 16, 235, 0f, 0f, 0, default(Color), 1f);
						Main.dust[dust].velocity *= 3f;
						Main.dust[dust].scale *= 1.15f;
					}
					int num226 = 36;
					for (int num227 = 0; num227 < num226; num227++)
					{
						Vector2 vector6 = Vector2.Normalize(Player.velocity) * new Vector2((float)Player.width / 2f, (float)Player.height) * 0.75f;
						vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Player.Center;
						Vector2 vector7 = vector6 - Player.Center;
						int num228 = Dust.NewDust(vector6 + vector7, 0, 0, 235, vector7.X * 1.5f, vector7.Y * 1.5f, 100, default(Color), 1.4f);
						Main.dust[num228].noGravity = true;
						Main.dust[num228].noLight = true;
						Main.dust[num228].velocity = vector7;
					}
					Player.AddBuff(Mod.Find<ModBuff>("RageMode").Type, 300);
				}
			}
			if (CalamityModClassicPreTrailer.AdrenalineHotKey.JustPressed)
			{
				if (adrenaline == adrenalineMax && !adrenalineMode)
				{
					SoundEngine.PlaySound(SoundID.Zombie104, Player.position);
					for (int num502 = 0; num502 < 64; num502++)
					{
						int dust = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 16f), Player.width, Player.height - 16, 206, 0f, 0f, 0, default(Color), 1f);
						Main.dust[dust].velocity *= 3f;
						Main.dust[dust].scale *= 2f;
					}
					int num226 = 36;
					for (int num227 = 0; num227 < num226; num227++)
					{
						Vector2 vector6 = Vector2.Normalize(Player.velocity) * new Vector2((float)Player.width / 2f, (float)Player.height) * 0.75f;
						vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Player.Center;
						Vector2 vector7 = vector6 - Player.Center;
						int num228 = Dust.NewDust(vector6 + vector7, 0, 0, 206, vector7.X * 1.5f, vector7.Y * 1.5f, 100, default(Color), 1.4f);
						Main.dust[num228].noGravity = true;
						Main.dust[num228].noLight = true;
						Main.dust[num228].velocity = vector7;
					}
					Player.AddBuff(Mod.Find<ModBuff>("AdrenalineMode").Type, 300);
				}
			}
		}
		#endregion

		#region TeleportMethods
		public void HandleTeleport(int teleportType, bool forceHandle = false, int whoAmI = 0)
		{
			bool syncData = forceHandle || Main.netMode == 0;
			if (syncData)
			{
				TeleportPlayer(teleportType, forceHandle, whoAmI);
			}
			else
			{
				SyncTeleport(teleportType);
			}
		}

		public static void TeleportPlayer(int teleportType, bool syncData = false, int whoAmI = 0)
		{
			Player player;
			if (!syncData)
			{
				player = Main.LocalPlayer;
			}
			else
			{
				player = Main.player[whoAmI];
			}
			switch (teleportType)
			{
				case 0: UnderworldTeleport(player, syncData); break;
				case 1: DungeonTeleport(player, syncData); break;
				case 2: JungleTeleport(player, syncData); break;
				default: break;
			}
		}

		public void SyncTeleport(int teleportType)
		{
			ModPacket netMessage = Mod.GetPacket();
			netMessage.Write((byte)CalamityModClassicPreTrailerMessageType.TeleportPlayer);
			netMessage.Write(teleportType);
			netMessage.Send();
		}

		public static void UnderworldTeleport(Player player, bool syncData = false)
		{
			int teleportStartX = 100;
			int teleportRangeX = Main.maxTilesX - 200;
			int teleportStartY = Main.maxTilesY - 200;
			int teleportRangeY = 50;
			bool flag = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int width = player.width;
			Vector2 vector = new Vector2((float)num2, (float)num3) * 16f + new Vector2((float)(-(float)width / 2 + 8), (float)(-(float)player.height));
			while (!flag && num < 1000)
			{
				num++;
				num2 = teleportStartX + Main.rand.Next(teleportRangeX);
				num3 = teleportStartY + Main.rand.Next(teleportRangeY);
				vector = new Vector2((float)num2, (float)num3) * 16f + new Vector2((float)(-(float)width / 2 + 8), (float)(-(float)player.height));
				if (!Collision.SolidCollision(vector, width, player.height))
				{
					int i = 0;
					while (i < 100)
					{
						Tile tile = Main.tile[num2, num3 + i];
						vector = new Vector2((float)num2, (float)(num3 + i)) * 16f + new Vector2((float)(-(float)width / 2 + 8), (float)(-(float)player.height));
						Vector4 vector2 = Collision.SlopeCollision(vector, player.velocity, width, player.height, player.gravDir, false);
						bool arg_1FF_0 = !Collision.SolidCollision(vector, width, player.height);
						if (vector2.Z == player.velocity.X && vector2.Y == player.velocity.Y && vector2.X == vector.X)
						{
							bool arg_1FE_0 = vector2.Y == vector.Y;
						}
						if (arg_1FF_0)
						{
							i++;
						}
						else
						{
							if (tile.HasTile && !tile.IsActuated && Main.tileSolid[(int)tile.TileType])
							{
								break;
							}
							i++;
						}
					}
					if (!Collision.LavaCollision(vector, width, player.height) && Collision.HurtTiles(vector, width, player.height, player).y <= 0f)
					{
						Collision.SlopeCollision(vector, player.velocity, width, player.height, player.gravDir, false);
						if (Collision.SolidCollision(vector, width, player.height) && i < 99)
						{
							Vector2 vector3 = Vector2.UnitX * 16f;
							if (!(Collision.TileCollision(vector - vector3, vector3, player.width, player.height, false, false, (int)player.gravDir) != vector3))
							{
								vector3 = -Vector2.UnitX * 16f;
								if (!(Collision.TileCollision(vector - vector3, vector3, player.width, player.height, false, false, (int)player.gravDir) != vector3))
								{
									vector3 = Vector2.UnitY * 16f;
									if (!(Collision.TileCollision(vector - vector3, vector3, player.width, player.height, false, false, (int)player.gravDir) != vector3))
									{
										vector3 = -Vector2.UnitY * 16f;
										if (!(Collision.TileCollision(vector - vector3, vector3, player.width, player.height, false, false, (int)player.gravDir) != vector3))
										{
											flag = true;
											num3 += i;
											break;
										}
									}
								}
							}
						}
					}
				}
			}
			if (!flag)
			{
				return;
			}
			ModTeleport(player, vector, syncData, false);
		}

		public static void DungeonTeleport(Player player, bool syncData = false)
		{
			ModTeleport(player, new Vector2(Main.dungeonX, Main.dungeonY), syncData, true);
		}

		public static void JungleTeleport(Player player, bool syncData = false)
		{
			int jungleSide = CalamityWorldPreTrailer.abyssSide ? ((Main.maxTilesX / 2) + (Main.maxTilesX / 4)) : (Main.maxTilesX / 5);
			int teleportStartX = jungleSide;
			int teleportRangeX = 200;
			int teleportStartY = (int)Main.worldSurface;
			int teleportRangeY = 50;
			bool flag = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int width = player.width;
			Vector2 vector = new Vector2((float)num2, (float)num3) * 16f + new Vector2((float)(-(float)width / 2 + 8), (float)(-(float)player.height));
			while (!flag && num < 1000)
			{
				num++;
				num2 = teleportStartX + Main.rand.Next(teleportRangeX);
				num3 = teleportStartY + Main.rand.Next(teleportRangeY);
				vector = new Vector2((float)num2, (float)num3) * 16f + new Vector2((float)(-(float)width / 2 + 8), (float)(-(float)player.height));
				if (!Collision.SolidCollision(vector, width, player.height))
				{
					int i = 0;
					while (i < 100)
					{
						Tile tile = Main.tile[num2, num3 + i];
						vector = new Vector2((float)num2, (float)(num3 + i)) * 16f + new Vector2((float)(-(float)width / 2 + 8), (float)(-(float)player.height));
						Vector4 vector2 = Collision.SlopeCollision(vector, player.velocity, width, player.height, player.gravDir, false);
						bool arg_1FF_0 = !Collision.SolidCollision(vector, width, player.height);
						if (vector2.Z == player.velocity.X && vector2.Y == player.velocity.Y && vector2.X == vector.X)
						{
							bool arg_1FE_0 = vector2.Y == vector.Y;
						}
						if (arg_1FF_0)
						{
							i++;
						}
						else
						{
							if (tile.HasTile && !tile.IsActuated && Main.tileSolid[(int)tile.TileType])
							{
								break;
							}
							i++;
						}
					}
					if (!Collision.LavaCollision(vector, width, player.height) && Collision.HurtTiles(vector, width, player.height, player).y <= 0f)
					{
						Collision.SlopeCollision(vector, player.velocity, width, player.height, player.gravDir, false);
						if (Collision.SolidCollision(vector, width, player.height) && i < 99)
						{
							Vector2 vector3 = Vector2.UnitX * 16f;
							if (!(Collision.TileCollision(vector - vector3, vector3, player.width, player.height, false, false, (int)player.gravDir) != vector3))
							{
								vector3 = -Vector2.UnitX * 16f;
								if (!(Collision.TileCollision(vector - vector3, vector3, player.width, player.height, false, false, (int)player.gravDir) != vector3))
								{
									vector3 = Vector2.UnitY * 16f;
									if (!(Collision.TileCollision(vector - vector3, vector3, player.width, player.height, false, false, (int)player.gravDir) != vector3))
									{
										vector3 = -Vector2.UnitY * 16f;
										if (!(Collision.TileCollision(vector - vector3, vector3, player.width, player.height, false, false, (int)player.gravDir) != vector3))
										{
											flag = true;
											num3 += i;
											break;
										}
									}
								}
							}
						}
					}
				}
			}
			if (!flag)
			{
				return;
			}
			ModTeleport(player, vector, syncData, false);
		}

		public static void ModTeleport(Player player, Vector2 pos, bool syncData = false, bool convertFromTiles = false)
		{
			bool postImmune = player.immune;
			int postImmunteTime = player.immuneTime;
			if (convertFromTiles)
			{
				pos = new Vector2(pos.X * 16 + 8 - player.width / 2, pos.Y * 16 - player.height);
			}
			player.grappling[0] = -1;
			player.grapCount = 0;
			for (int index = 0; index < 1000; ++index)
			{
				if (Main.projectile[index].active && Main.projectile[index].owner == player.whoAmI && Main.projectile[index].aiStyle == 7)
				{
					Main.projectile[index].Kill();
				}
			}
			player.Teleport(pos, 2, 0);
			player.velocity = Vector2.Zero;
			player.immune = postImmune;
			player.immuneTime = postImmunteTime;
			for (int index = 0; index < 100; ++index)
			{
				Main.dust[Dust.NewDust(player.position, player.width, player.height, 164, player.velocity.X * 0.2f, player.velocity.Y * 0.2f, 150, Color.Cyan, 1.2f)].velocity *= 0.5f;
			}
			Main.TeleportEffect(player.getRect(), 1);
			Main.TeleportEffect(player.getRect(), 3);
			SoundEngine.PlaySound(SoundID.Item6, player.position);
			if (Main.netMode != 2)
			{
				return;
			}
			if (syncData)
			{
				RemoteClient.CheckSection(player.whoAmI, player.position, 1);
				NetMessage.SendData(65, -1, -1, null, 0, (float)player.whoAmI, pos.X, pos.Y, 3, 0, 0);
			}
		}
		#endregion

		#region UpdateEquips
		
		public override void UpdateVisibleVanityAccessories()
		{
			for (int n = 13; n < 18 + Player.extraAccessorySlots; n++)
			{
				Item item = Player.armor[n];
				if (item.type == ModContent.ItemType<Items.Permafrost.Popo>())
				{
					snowmanHide = false;
					snowmanForce = true;
				}
				else if (item.type == ModContent.ItemType<Items.Armor.AbyssalDivingSuit>())
				{
					abyssalDivingSuitHide = false;
					abyssalDivingSuitForce = true;
				}
				else if (item.type == ModContent.ItemType<Items.Armor.SirensHeart>())
				{
					sirenBoobsHide = false;
					sirenBoobsForce = true;
				}
				else if (item.type == ModContent.ItemType<Items.Armor.SirensHeartAlt>())
				{
					sirenBoobsAltHide = false;
					sirenBoobsAltForce = true;
				}
			}
		}

		public override void UpdateEquips()
		{
			if (Config.MiningSpeedBoost)
			{
				Player.pickSpeed *= 0.75f;
			}

			#region MeleeSpeed
			float meleeSpeedMult = 0f;
			if (bBlood)
			{
				meleeSpeedMult += 0.025f;
			}
			if (rRage)
			{
				meleeSpeedMult += 0.05f;
			}
			if (graxDefense)
			{
				meleeSpeedMult += 0.05f;
			}
			if (sMeleeBoost)
			{
				meleeSpeedMult += 0.05f;
			}
			if (yPower)
			{
				meleeSpeedMult += 0.05f;
			}
			if (darkSunRing)
			{
				meleeSpeedMult += 0.12f;
			}
			if (badgeOfBravery)
			{
				meleeSpeedMult += 0.15f;
			}
			if (badgeOfBraveryRare)
			{
				meleeSpeedMult += 0.2f;
			}
			if (eGauntlet)
			{
				meleeSpeedMult += 0.15f;
			}
			if (yInsignia)
			{
				meleeSpeedMult += 0.1f;
			}
			if (bloodyMary)
			{
				if (Main.bloodMoon)
				{
					meleeSpeedMult += 0.15f;
				}
			}
			if (community)
			{
				float floatTypeBoost = 0.01f +
					(NPC.downedSlimeKing ? 0.01f : 0f) +
					(NPC.downedBoss1 ? 0.01f : 0f) +
					(NPC.downedBoss2 ? 0.01f : 0f) +
					(NPC.downedQueenBee ? 0.01f : 0f) + //0.05
					(NPC.downedBoss3 ? 0.01f : 0f) +
					(Main.hardMode ? 0.01f : 0f) +
					(NPC.downedMechBossAny ? 0.01f : 0f) +
					(NPC.downedPlantBoss ? 0.01f : 0f) +
					(NPC.downedGolemBoss ? 0.01f : 0f) + //0.1
					(NPC.downedFishron ? 0.01f : 0f) +
					(NPC.downedAncientCultist ? 0.01f : 0f) +
					(NPC.downedMoonlord ? 0.01f : 0f) +
					(CalamityWorldPreTrailer.downedProvidence ? 0.02f : 0f) + //0.15
					(CalamityWorldPreTrailer.downedDoG ? 0.02f : 0f) + //0.17
					(CalamityWorldPreTrailer.downedYharon ? 0.03f : 0f); //0.2
				meleeSpeedMult += (floatTypeBoost * 0.25f);
			}
			if (eArtifact)
			{
				meleeSpeedMult += 0.1f;
			}
			if (Config.ProficiencyEnabled)
			{
				meleeSpeedMult += GetMeleeSpeedBonus();
			}
			Player.GetAttackSpeed(DamageClass.Melee) += meleeSpeedMult;
			#endregion

			if (snowman)
			{
				if (Player.whoAmI == Main.myPlayer && !snowmanNoseless)
				{
					Player.AddBuff(ModContent.BuffType<Buffs.Permafrost.Popo>(), 60, true);
				}
			}
			if (abyssalDivingSuit)
			{
				Player.AddBuff(ModContent.BuffType<Buffs.AbyssalDivingSuitBuff>(), 60, true);
				if (Player.whoAmI == Main.myPlayer)
				{
					if (abyssalDivingSuitCooldown)
					{
						for (int l = 0; l < 22; l++)
						{
							int hasBuff = Player.buffType[l];
							if (Player.buffTime[l] < 30 && hasBuff == Mod.Find<ModBuff>("AbyssalDivingSuitPlatesBroken").Type)
							{
								abyssalDivingSuitPlateHits = 0;
								Player.DelBuff(l);
								l = -1;
							}
						}
					}
					else
					{
						Player.AddBuff(Mod.Find<ModBuff>("AbyssalDivingSuitPlates").Type, 2);
					}
				}
			}
			if (sirenBoobs)
			{
				Player.AddBuff(ModContent.BuffType<Buffs.SirenBobs>(), 60, true);
			}
			else if (sirenBoobsAlt)
			{
				Player.AddBuff(ModContent.BuffType<Buffs.SirenBobsAlt>(), 60, true);
			}
			if ((sirenBoobs || sirenBoobsAlt) && NPC.downedBoss3)
			{
				if (Player.whoAmI == Main.myPlayer && !sirenIceCooldown)
				{
					Player.AddBuff(Mod.Find<ModBuff>("IceShieldBuff").Type, 2);
				}
			}
		}
		#endregion

		#region PreUpdateBuffs
		public override void PreUpdateBuffs()
		{
			//Remove the mighty wind buff if the player is in the astral desert.
			if (Player.ZoneDesert && ZoneAstral && Player.HasBuff(BuffID.WindPushed))
			{
				Player.ClearBuff(BuffID.WindPushed);
			}
		}
		#endregion

		#region PostUpdate

		#region PostUpdateBuffs
		public override void PostUpdateBuffs()
		{
			if (NOU)
				NOULOL();

			if (CalamityWorldPreTrailer.defiled)
				Defiled();

			if (weakPetrification)
				WeakPetrification();

			if (CalamityWorldPreTrailer.bossRushActive)
				BossRushMovementChanges();

			if (Player.mount.Active || Player.mount.Cart || (Config.BossRushDashCurse && CalamityWorldPreTrailer.bossRushActive))
			{
				Player.dashDelay = 60;
				dashInactive = false;
				dashMod = 0;
			}

			if (silvaCountdown > 0 && hasSilvaEffect && silvaSet)
			{
				if (Player.lifeRegen < 0)
					Player.lifeRegen = 0;
			}
		}
		#endregion

		#region PostUpdateEquips
		public override void PostUpdateEquips()
		{
			if (NOU)
				NOULOL();

			if (CalamityWorldPreTrailer.defiled)
				Defiled();

			if (weakPetrification)
				WeakPetrification();

			if (CalamityWorldPreTrailer.bossRushActive)
				BossRushMovementChanges();

			if (Player.mount.Active || Player.mount.Cart || (Config.BossRushDashCurse && CalamityWorldPreTrailer.bossRushActive))
			{
				Player.dashDelay = 60;
				dashInactive = false;
				dashMod = 0;
			}

			if (silvaCountdown > 0 && hasSilvaEffect && silvaSet)
			{
				if (Player.lifeRegen < 0)
					Player.lifeRegen = 0;
			}
		}
		#endregion

		public override void PostUpdateMiscEffects()
		{
			Player player = Main.LocalPlayer;
			Point point = player.Center.ToTileCoordinates();
			bool aboveGround = point.Y > Main.maxTilesY - 320;
			bool overworld = player.ZoneOverworldHeight && (point.X < 380 || point.X > Main.maxTilesX - 380);
			if (Main.netMode != NetmodeID.Server)
			{
				bool useNebula = NPC.AnyNPCs(Mod.Find<ModNPC>("DevourerofGodsHead").Type);
				player.ManageSpecialBiomeVisuals("CalamityModClassicPreTrailer:DevourerofGodsHead", useNebula);
				bool useNebulaS = NPC.AnyNPCs(Mod.Find<ModNPC>("DevourerofGodsHeadS").Type);
				player.ManageSpecialBiomeVisuals("CalamityModClassicPreTrailer:DevourerofGodsHeadS", useNebulaS);
				bool useBrimstone = NPC.AnyNPCs(Mod.Find<ModNPC>("CalamitasRun3").Type);
				player.ManageSpecialBiomeVisuals("CalamityModClassicPreTrailer:CalamitasRun3", useBrimstone);
				bool usePlague = NPC.AnyNPCs(Mod.Find<ModNPC>("PlaguebringerGoliath").Type);
				player.ManageSpecialBiomeVisuals("CalamityModClassicPreTrailer:PlaguebringerGoliath", usePlague);
				bool useFire = NPC.AnyNPCs(Mod.Find<ModNPC>("Yharon").Type);
				player.ManageSpecialBiomeVisuals("CalamityModClassicPreTrailer:Yharon", useFire);
				player.ManageSpecialBiomeVisuals("HeatDistortion", Main.UseHeatDistortion && (useFire ||
					player.GetModPlayer<CalamityPlayerPreTrailer>().trippy ||
					(aboveGround || ((double)point.Y < Main.worldSurface && player.ZoneDesert && !overworld &&
					                 !Main.raining && !Filters.Scene["Sandstorm"].IsActive()))));
				bool useWater = NPC.AnyNPCs(Mod.Find<ModNPC>("Leviathan").Type);
				player.ManageSpecialBiomeVisuals("CalamityModClassicPreTrailer:Leviathan", useWater);
				bool useHoly = NPC.AnyNPCs(Mod.Find<ModNPC>("Providence").Type);
				player.ManageSpecialBiomeVisuals("CalamityModClassicPreTrailer:Providence", useHoly);
				bool useSBrimstone = NPC.AnyNPCs(Mod.Find<ModNPC>("SupremeCalamitas").Type);
				player.ManageSpecialBiomeVisuals("CalamityModClassicPreTrailer:SupremeCalamitas", useSBrimstone);
				bool inAstral = player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAstral;
				player.ManageSpecialBiomeVisuals("CalamityModClassicPreTrailer:Astral", inAstral);
			}
			if (Config.ExpertDebuffDurationReduction)
			{
				var copy = Main.RegisteredGameModes[GameModeID.Expert];
				copy.DebuffTimeMultiplier = 1f;
				Main.RegisteredGameModes[GameModeID.Expert] = copy;
			}

			areThereAnyDamnBosses = CalamityGlobalNPC.AnyBossNPCS();

			#region RevengeanceEffects
			if (CalamityWorldPreTrailer.revenge)
			{
				if (Player.lifeSteal > (CalamityWorldPreTrailer.death ? 30f : 40f))
				{
					Player.lifeSteal = (CalamityWorldPreTrailer.death ? 30f : 40f);
				}
				if (Player.whoAmI == Main.myPlayer)
				{
					if (Player.onHitDodge)
					{
						for (int l = 0; l < 22; l++)
						{
							int hasBuff = Player.buffType[l];
							if (Player.buffTime[l] > 360 && hasBuff == BuffID.ShadowDodge)
							{
								Player.buffTime[l] = 360;
							}
						}
					}
					if (Player.immuneTime > (tarraThrowing ? 300 : 120))
					{
						Player.immuneTime = (tarraThrowing ? 300 : 120);
					}
					if (Config.AdrenalineAndRage)
					{
						int stressGain = 0;
						if (rageMode)
						{
							stressGain = -2000;
						}
						else
						{
							if (draedonsHeart)
							{
								if (draedonsStressGain)
								{
									stressGain += 60;
								}
							}
							else if (heartOfDarkness)
							{
								stressGain += 30;
							}
						}
						stressCD++;
						if (stressCD >= 60)
						{
							stressCD = 0;
							stress += stressGain;
							if (stress < 0)
							{
								stress = 0;
							}
							if (stress >= stressMax)
							{
								if (playFullRageSound)
								{
									playFullRageSound = false;
									SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/FullRage"), Player.position);
								}
								stress = stressMax;
							}
							else
							{
								playFullRageSound = true;
							}
						}
						stressLevel500 = stress >= 9800;
						if (stressLevel500 && !hAttack)
						{
							int heartAttackChance = ((draedonsHeart || heartOfDarkness) ? 2000 : 10000);
							if (Main.rand.Next(heartAttackChance) == 0)
							{
								Player.AddBuff(Mod.Find<ModBuff>("HeartAttack").Type, 18000);
							}
						}
						if (adrenaline >= adrenalineMax)
						{
							adrenalineMaxTimer--;
							if (adrenalineMaxTimer <= 0)
							{
								if (playAdrenalineBurnoutSound)
								{
									playAdrenalineBurnoutSound = false;
									SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/AdrenalineBurnout1"), Player.position);
								}
								adrenalineDmgDown--;
								if (adrenalineDmgDown < 0)
								{
									if (playFullAdrenalineBurnoutSound)
									{
										playFullAdrenalineBurnoutSound = false;
										SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/AdrenalineBurnout2"), Player.position);
									}
									adrenalineDmgDown = 0;
								}
								adrenalineMaxTimer = 0;
							}
						}
						else if (!adrenalineMode && adrenaline <= 0)
						{
							playAdrenalineBurnoutSound = true;
							playFullAdrenalineBurnoutSound = true;
							adrenalineDmgDown = 600;
							adrenalineMaxTimer = 300;
							adrenalineDmgMult = 1f;
						}
						adrenalineDmgMult = 0.1f * (float)(adrenalineDmgDown / 60);
						if (adrenalineDmgMult < 0.33f)
							adrenalineDmgMult = 0.33f;
						int adrenalineGain = 0;
						bool SCalAlive = NPC.AnyNPCs(Mod.Find<ModNPC>("SupremeCalamitas").Type);
						if (adrenalineMode)
						{
							adrenalineGain = (SCalAlive ? -10000 : -2000);
						}
						else
						{
							if (Main.wofNPCIndex >= 0 && Player.position.Y < (float)((Main.maxTilesY - 200) * 16)) // >
							{
								adrenaline = 0;
							}
							else if (areThereAnyDamnBosses || CalamityWorldPreTrailer.DoGSecondStageCountdown > 0)
							{
								int adrenalineTickBoost = 0 +
									(adrenalineBoostOne ? 63 : 0) + //286
									(adrenalineBoostTwo ? 115 : 0) + //401
									(adrenalineBoostThree ? 100 : 0); //501
								adrenalineGain = 223 + adrenalineTickBoost; //pre-slime god = 45, pre-astrum deus = 35, pre-polterghast = 25, post-polter = 20
							}
							else
							{
								adrenaline = 0;
							}
						}
						adrenalineCD++;
						if (adrenalineCD >= (SCalAlive ? 135 : 60))
						{
							adrenalineCD = 0;
							adrenaline += adrenalineGain;
							if (adrenaline < 0)
							{
								adrenaline = 0;
							}
							if (adrenaline >= adrenalineMax)
							{
								if (playFullAdrenalineSound)
								{
									playFullAdrenalineSound = false;
									SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/FullAdrenaline"), Player.position);
								}
								adrenaline = adrenalineMax;
							}
							else
							{
								playFullAdrenalineSound = true;
							}
						}
					}
				}
			}
			else
			{
				if (Player.whoAmI == Main.myPlayer)
				{
					stressCD++;
					if (stressCD >= 60)
					{
						stressCD = 0;
						stress += -30;
						if (stress < 0)
						{
							stress = 0;
						}
					}
					adrenalineCD++;
					if (adrenalineCD >= 60)
					{
						adrenalineCD = 0;
						adrenaline += -30;
						if (adrenaline < 0)
						{
							adrenaline = 0;
						}
					}
				}
			}
			if (Player.whoAmI == Main.myPlayer && Main.netMode == 1)
			{
				packetTimer++;
				if (packetTimer == 60)
				{
					packetTimer = 0;
					StressPacket(false);
					AdrenalinePacket(false);
				}
			}
			#endregion

			#region StressTiedEffects
			if (stressPills)
			{
				Player.statDefense += 8;
				AllDamageBoost(0.08f);
			}
			if (laudanum)
			{
				Player.statDefense += 6;
				AllDamageBoost(0.06f);
			}
			if (draedonsHeart)
			{
				AllDamageBoost(0.1f);
			}
			if (!stressLevel500 && Player.FindBuffIndex(Mod.Find<ModBuff>("HeartAttack").Type) > -1) { Player.ClearBuff(Mod.Find<ModBuff>("HeartAttack").Type); }
			if (draedonsHeart && (double)Math.Abs(Player.velocity.X) < 0.05 && (double)Math.Abs(Player.velocity.Y) < 0.05 && Player.itemAnimation == 0)
			{
				Player.statDefense += 25;
			}
			if (hAttack)
			{
				if (heartOfDarkness || draedonsHeart)
				{
					AllDamageBoost(0.1f);
				}
				Player.statLifeMax2 += Player.statLifeMax / 5 / 20 * 5;
			}
			if (affliction || afflicted)
			{
				Player.lifeRegen += 1;
				Player.endurance += CalamityWorldPreTrailer.revenge ? 0.07f : 0.05f;
				Player.statDefense += CalamityWorldPreTrailer.revenge ? 20 : 15;
				float damageBoost = CalamityWorldPreTrailer.revenge ? 0.12f : 0.1f;
				AllDamageBoost(damageBoost);
				Player.statLifeMax2 += CalamityWorldPreTrailer.revenge ? (Player.statLifeMax / 5 / 20 * 10) : (Player.statLifeMax / 5 / 20 * 5);
			}
			#endregion

			#region MaxLifeAndManaBoosts
			Player.statLifeMax2 +=
				(mFruit ? 25 : 0) +
				(bOrange ? 25 : 0) +
				(eBerry ? 25 : 0) +
				(dFruit ? 25 : 0);
			if (silvaHitCounter > 0)
			{
				Player.statLifeMax2 -= silvaHitCounter * 100;
				if (Player.statLifeMax2 <= 400)
				{
					Player.statLifeMax2 = 400;
					if (silvaCountdown > 0)
					{
						if (Player.FindBuffIndex(Mod.Find<ModBuff>("SilvaRevival").Type) > -1) { Player.ClearBuff(Mod.Find<ModBuff>("SilvaRevival").Type); }
						SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/SilvaDispel"), Player.position);
					}
					silvaCountdown = 0;
				}
			}
			Player.statManaMax2 +=
				(pHeart ? 50 : 0) +
				(eCore ? 50 : 0) +
				(cShard ? 50 : 0) +
				(starBeamRye ? 50 : 0);
			if (Main.netMode != 2 && Player.whoAmI == Main.myPlayer)
			{
				Asset<Texture2D> rain3 = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/Rain3");
				Asset<Texture2D> rainOriginal = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/RainOriginal");
				Asset<Texture2D> mana2 = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/Mana2");
				Asset<Texture2D> mana3 = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/Mana3");
				Asset<Texture2D> mana4 = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/Mana4");
				Asset<Texture2D> manaOriginal = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/ManaOriginal");
				Asset<Texture2D> carpetAuric = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/AuricCarpet");
				Asset<Texture2D> carpetOriginal = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/Carpet");
				int totalManaBoost =
					(pHeart ? 1 : 0) +
					(eCore ? 1 : 0) +
					(cShard ? 1 : 0);
				switch (totalManaBoost)
				{
					default:
						TextureAssets.Mana = manaOriginal;
						break;
					case 3:
						TextureAssets.Mana = mana4;
						break;
					case 2:
						TextureAssets.Mana = mana3;
						break;
					case 1:
						TextureAssets.Mana = mana2;
						break;
				}
				if (Main.bloodMoon) { TextureAssets.Rain = rainOriginal; }
				else if (Main.raining && ZoneSulphur) { TextureAssets.Rain = rain3; }
				else { TextureAssets.Rain = rainOriginal; }
				if (auricSet) { TextureAssets.FlyingCarpet = carpetAuric; }
				else { TextureAssets.FlyingCarpet = carpetOriginal; }
			}
			#endregion

			#region MiscEffects
			if (Config.ProficiencyEnabled)
			{
				GetExactLevelUp();
			}

			if (gainRageCooldown > 0)
				gainRageCooldown--;

			if (Player.nebulaLevelMana > 0 && Player.statMana < Player.statManaMax2)
			{
				int num = 12;
				nebulaManaNerfCounter += Player.nebulaLevelMana;
				if (nebulaManaNerfCounter >= num)
				{
					nebulaManaNerfCounter -= num;
					Player.statMana--;
					if (Player.statMana < 0)
					{
						Player.statMana = 0;
					}
				}
			}
			else
			{
				nebulaManaNerfCounter = 0;
			}
			if (Main.myPlayer == Player.whoAmI)
			{
				BossHealthBarManager.SHOULD_DRAW_SMALLTEXT_HEALTH = shouldDrawSmallText;
				/*if (player.chest != -1)
				{
					if (player.chest != -2)
					{
						trashManChest = -1;
					}
					if (trashManChest >= 0)
					{
						if (!Main.projectile[trashManChest].active || Main.projectile[trashManChest].type != mod.ProjectileType("DannyDevito"))
						{
							Main.PlaySound(SoundID.Item11, -1, -1);
							player.chest = -1;
							Recipe.FindRecipes();
						}
						else
						{
							int num16 = (int)(((double)player.position.X + (double)player.width * 0.5) / 16.0);
							int num17 = (int)(((double)player.position.Y + (double)player.height * 0.5) / 16.0);
							player.chestX = (int)Main.projectile[trashManChest].Center.X / 16;
							player.chestY = (int)Main.projectile[trashManChest].Center.Y / 16;
							if (num16 < player.chestX - Player.tileRangeX || num16 > player.chestX + Player.tileRangeX + 1 || num17 < player.chestY - Player.tileRangeY || num17 > player.chestY + Player.tileRangeY + 1)
							{
								if (player.chest != -1)
								{
									Main.PlaySound(SoundID.Item11, -1, -1);
								}
								player.chest = -1;
								Recipe.FindRecipes();
							}
						}
					}
				}
				else
				{
					trashManChest = -1;
				}*/
			}
			if (silvaSet || invincible || margarita)
			{
				foreach (int debuff in CalamityModClassicPreTrailer.debuffList)
					Player.buffImmune[debuff] = true;
			}
			if (aSparkRare)
			{
				Player.buffImmune[BuffID.Electrified] = true;
			}
			if (Config.ExpertChilledWaterRemoval)
			{
				if (Main.expertMode && Player.ZoneSnow && Player.wet && !Player.lavaWet && !Player.honeyWet && !Player.arcticDivingGear)
				{
					Player.buffImmune[BuffID.Chilled] = true;
					if (Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir))
					{
						if (Main.myPlayer == Player.whoAmI && !Player.gills && !Player.merman)
						{
							if (Player.breath > 0)
								Player.breath--;
						}
					}
				}
			}
			if (ZoneAbyss)
			{
				if (abyssalAmulet)
				{
					Player.statLifeMax2 += Player.statLifeMax2 / 5 / 20 * (lumenousAmulet ? 25 : 10);
				}
				if (Main.myPlayer == Player.whoAmI) //4200 total tiles small world
				{
					int breathLoss = 2;
					int lifeLossAtZeroBreath = 3;
					int lightStrength = 0 +
						((Player.lightOrb || Player.crimsonHeart || Player.magicLantern) ? 1 : 0) + //1
						(aquaticEmblem ? 1 : 0) + //2
						(Player.arcticDivingGear ? 1 : 0) + //3
						(jellyfishNecklace ? 1 : 0) + //4
						((Player.blueFairy || Player.greenFairy || Player.redFairy || Player.petFlagDD2Ghost) ? 2 : 0) + //6
						((shine || lumenousAmulet) ? 2 : 0) + //8
						((Player.wisp || Player.suspiciouslookingTentacle || sirenPet) ? 3 : 0); //11
					bool lightLevelOne = lightStrength > 0; //1+
					bool lightLevelTwo = lightStrength > 2; //3+
					bool lightLevelThree = lightStrength > 4; //5+
					bool lightLevelFour = lightStrength > 6; //7+
					if (ZoneAbyssLayer4) //3200 and below
					{
						breathLoss = 54;
						if (!lightLevelFour)
							Player.blind = true;
						if (!lightLevelThree)
							Player.headcovered = true;
						Player.bleed = true;
						lifeLossAtZeroBreath = 24;
						Player.statDefense -= (anechoicPlating ? 40 : 120);
					}
					else if (ZoneAbyssLayer3) //2700 to 3200
					{
						breathLoss = 18;
						if (!lightLevelThree)
							Player.blind = true;
						if (!lightLevelTwo)
							Player.headcovered = true;
						if (!abyssalDivingSuit)
							Player.bleed = true;
						lifeLossAtZeroBreath = 12;
						Player.statDefense -= (anechoicPlating ? 20 : 60);
					}
					else if (ZoneAbyssLayer2) //2100 to 2700
					{
						breathLoss = 6;
						if (!lightLevelTwo)
							Player.blind = true;
						if (!depthCharm)
							Player.bleed = true;
						lifeLossAtZeroBreath = 6;
						Player.statDefense -= (anechoicPlating ? 10 : 30);
					}
					else if (ZoneAbyssLayer1) //1500 to 2100
					{
						if (!lightLevelOne)
							Player.blind = true;
						Player.statDefense -= (anechoicPlating ? 5 : 15);
					}
					double breathLossMult = 1.0 -
						(Player.gills ? 0.2 : 0.0) - //0.8
						(Player.accDivingHelm ? 0.25 : 0.0) - //0.75
						(Player.arcticDivingGear ? 0.25 : 0.0) - //0.75
						(aquaticEmblem ? 0.25 : 0.0) - //0.75
						(Player.accMerman ? 0.3 : 0.0) - //0.7
						(victideSet ? 0.2 : 0.0) - //0.85
						(((sirenBoobs || sirenBoobsAlt) && NPC.downedBoss3) ? 0.3 : 0.0) - //0.7
						(abyssalDivingSuit ? 0.3 : 0.0); //0.7
					if (breathLossMult < 0.05)
					{
						breathLossMult = 0.05;
					}
					breathLoss = (int)((double)breathLoss * breathLossMult);
					int tick = 6;
					double tickMult = 1.0 +
						(Player.gills ? 4.0 : 0.0) + //5
						(Player.ignoreWater ? 5.0 : 0.0) + //10
						(Player.accDivingHelm ? 10.0 : 0.0) + //20
						(Player.arcticDivingGear ? 10.0 : 0.0) + //30
						(aquaticEmblem ? 10.0 : 0.0) + //40
						(Player.accMerman ? 15.0 : 0.0) + //55
						(victideSet ? 5.0 : 0.0) + //60
						(((sirenBoobs || sirenBoobsAlt) && NPC.downedBoss3) ? 15.0 : 0.0) + //75
						(abyssalDivingSuit ? 15.0 : 0.0); //90
					if (tickMult > 50.0)
					{
						tickMult = 50.0;
					}
					tick = (int)((double)tick * tickMult);
					abyssBreathCD++;
					if (Player.gills || Player.merman)
					{
						if (Player.breath > 0)
							Player.breath -= 3;
					}
					if (!Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir))
					{
						if (Player.statLife > 100)
						{
							if (Player.lifeRegen > 0)
							{
								Player.lifeRegen = 0;
							}
							Player.lifeRegenTime = 0;
							Player.lifeRegen -= 160;
						}
					}
					if (abyssBreathCD >= tick)
					{
						abyssBreathCD = 0;
						if (Player.breath > 0)
							Player.breath -= breathLoss;
						if (cDepth)
						{
							if (Player.breath > 0)
								Player.breath--;
						}
						if (Player.breath <= 0)
						{
							if (depthCharm)
							{
								lifeLossAtZeroBreath -= 3;
								if (abyssalDivingSuit)
									lifeLossAtZeroBreath -= 6;
							}
							if (lifeLossAtZeroBreath < 0)
							{
								lifeLossAtZeroBreath = 0;
							}
							Player.statLife -= lifeLossAtZeroBreath;
							if (Player.statLife <= 0)
							{
								KillPlayer();
							}
						}
					}
				}
			}
			else
			{
				abyssBreathCD = 0;
			}
			if ((Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir) && ironBoots) ||
				(aeroSet && !Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir)))
			{
				Player.maxFallSpeed = 15f;
			}
			if (Player.ZoneSkyHeight)
			{
				if (astrumDeusLore)
				{
					Player.moveSpeed += 0.3f;
				}
				if (astrumAureusLore)
				{
					Player.jumpSpeedBoost += 0.5f;
				}
			}
			if (omegaBlueSet) //should apply after rev caps, actually those are gone so AAAAA
			{
				//add tentacles
				if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("OmegaBlueTentacle").Type] < 6)
				{
					bool[] tentaclesPresent = new bool[6];
					for (int i = 0; i < 1000; i++)
					{
						Projectile projectile = Main.projectile[i];
						if (projectile.active && projectile.type == Mod.Find<ModProjectile>("OmegaBlueTentacle").Type && projectile.owner == Main.myPlayer && projectile.ai[1] >= 0f && projectile.ai[1] < 6f)
						{
							tentaclesPresent[(int)projectile.ai[1]] = true;
						}
					}
					for (int i = 0; i < 6; i++)
					{
						if (!tentaclesPresent[i])
						{
							float modifier = Player.GetDamage(DamageClass.Melee).Additive + Player.GetDamage(DamageClass.Magic).Additive + Player.GetDamage(DamageClass.Ranged).Additive +
								CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage + Player.GetDamage(DamageClass.Summon).Additive;
							modifier /= 5f;
							int damage = (int)(666 * modifier);
							Vector2 vel = new Vector2(Main.rand.Next(-13, 14), Main.rand.Next(-13, 14)) * 0.25f;
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center, vel, Mod.Find<ModProjectile>("OmegaBlueTentacle").Type, damage, 8f, Main.myPlayer, Main.rand.Next(120), i);
						}
					}
				}
				float damageUp = 0.1f;
				int critUp = 10;
				if (omegaBlueHentai)
				{
					damageUp *= 2f;
					critUp *= 2;
				}
				AllDamageBoost(damageUp);
				AllCritBoost(critUp);
			}
			if (!bOut)
			{
				if (gHealer)
				{
					if (healCounter > 0)
					{
						healCounter--;
					}
					if (healCounter <= 0)
					{
						healCounter = 300;
						if (Player.whoAmI == Main.myPlayer)
						{
							int healAmount = 5 +
								(gDefense ? 5 : 0) +
								(gOffense ? 5 : 0);
							Player.statLife += healAmount;
							Player.HealEffect(healAmount);
						}
					}
				}
				if (gDefense)
				{
					Player.moveSpeed += 0.1f +
						(gOffense ? 0.1f : 0f);
					Player.endurance += 0.025f +
						(gOffense ? 0.025f : 0f);
				}
				if (gOffense)
				{
					Player.GetDamage(DamageClass.Summon) += 0.1f +
						(gDefense ? 0.05f : 0f);
					Player.maxMinions++;
				}
			}
			if (draconicSurgeCooldown > 0)
				draconicSurgeCooldown--;
			if (revivifyTimer > 0)
				revivifyTimer--;
			if (fleshTotemCooldown > 0)
				fleshTotemCooldown--;
			if (astralStarRainCooldown > 0)
				astralStarRainCooldown--;
			if (bloodflareMageCooldown > 0)
				bloodflareMageCooldown--;
			if (tarraMageHealCooldown > 0)
				tarraMageHealCooldown--;
			if (ataxiaDmg > 0f)
				ataxiaDmg -= 1.5f;
			if (ataxiaDmg < 0f)
				ataxiaDmg = 0f;
			if (xerocDmg > 0f)
				xerocDmg -= 2f;
			if (xerocDmg < 0f)
				xerocDmg = 0f;
			if (godSlayerDmg > 0f)
				godSlayerDmg -= 2.5f;
			if (godSlayerDmg < 0f)
				godSlayerDmg = 0f;
			if (aBulwarkRareMeleeBoostTimer > 0)
				aBulwarkRareMeleeBoostTimer--;
			if (bossRushImmunityFrameCurseTimer > 0)
				bossRushImmunityFrameCurseTimer--;
			if (silvaCountdown > 0 && hasSilvaEffect && silvaSet)
			{
				Player.buffImmune[Mod.Find<ModBuff>("VulnerabilityHex").Type] = true;
				silvaCountdown--;
				if (silvaCountdown <= 0)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/SilvaDispel"), Player.position);
				}
				for (int j = 0; j < 2; j++)
				{
					int num = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 157, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 2f);
					Dust expr_A4_cp_0 = Main.dust[num];
					expr_A4_cp_0.position.X = expr_A4_cp_0.position.X + (float)Main.rand.Next(-20, 21);
					Dust expr_CB_cp_0 = Main.dust[num];
					expr_CB_cp_0.position.Y = expr_CB_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
					Main.dust[num].velocity *= 0.9f;
					Main.dust[num].noGravity = true;
					Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					}
				}
			}
			if (tarraMelee)
			{
				if (tarraDefense)
				{
					tarraDefenseTime--;
					if (tarraDefenseTime <= 0)
					{
						tarraDefenseTime = 600;
						tarraCooldown = 1800;
						tarraDefense = false;
					}
					for (int j = 0; j < 2; j++)
					{
						int num = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 157, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 2f);
						Dust expr_A4_cp_0 = Main.dust[num];
						expr_A4_cp_0.position.X = expr_A4_cp_0.position.X + (float)Main.rand.Next(-20, 21);
						Dust expr_CB_cp_0 = Main.dust[num];
						expr_CB_cp_0.position.Y = expr_CB_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
						Main.dust[num].velocity *= 0.9f;
						Main.dust[num].noGravity = true;
						Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
						Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
						}
					}
				}
				if (tarraCooldown > 0)
					tarraCooldown--;
			}
			if (tarraThrowing)
			{
				if (tarraThrowingCritTimer > 0)
				{
					tarraThrowingCritTimer--;
				}
				if (tarraThrowingCrits >= 25)
				{
					tarraThrowingCrits = 0;
					tarraThrowingCritTimer = 1800;
					Player.immune = true;
					Player.immuneTime = 300;
				}
				for (int l = 0; l < 22; l++)
				{
					int hasBuff = Player.buffType[l];
					bool shouldAffect = CalamityModClassicPreTrailer.debuffList.Contains(hasBuff);
					if (shouldAffect)
					{
						CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.1f;
					}
				}
			}
			if (bloodflareSet)
			{
				if (bloodflareHeartTimer > 0)
					bloodflareHeartTimer--;
				if (bloodflareManaTimer > 0)
					bloodflareManaTimer--;
			}
			if (bloodflareMelee)
			{
				if (bloodflareMeleeHits >= 15)
				{
					bloodflareMeleeHits = 0;
					bloodflareFrenzyTimer = 300;
				}
				if (bloodflareFrenzyTimer > 0)
				{
					bloodflareFrenzyTimer--;
					if (bloodflareFrenzyTimer <= 0)
					{
						bloodflareFrenzyCooldown = 1800;
					}
					Player.GetCritChance(DamageClass.Melee) += 25;
					Player.GetDamage(DamageClass.Melee) += 0.25f;
					for (int j = 0; j < 2; j++)
					{
						int num = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 5, 0f, 0f, 100, default(Color), 2f);
						Dust expr_A4_cp_0 = Main.dust[num];
						expr_A4_cp_0.position.X = expr_A4_cp_0.position.X + (float)Main.rand.Next(-20, 21);
						Dust expr_CB_cp_0 = Main.dust[num];
						expr_CB_cp_0.position.Y = expr_CB_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
						Main.dust[num].velocity *= 0.9f;
						Main.dust[num].noGravity = true;
						Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
						Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
						}
					}
				}
				if (bloodflareFrenzyCooldown > 0)
					bloodflareFrenzyCooldown--;
			}
			if (bloodflareRanged)
			{
				if (bloodflareRangedCooldown > 0)
					bloodflareRangedCooldown--;
			}
			if (Main.raining && ZoneSulphur)
			{
				if (Player.ZoneOverworldHeight || Player.ZoneSkyHeight)
					Player.AddBuff(Mod.Find<ModBuff>("Irradiated").Type, 2);
			}
			if (raiderTalisman)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += ((float)raiderStack / 250f) * 0.25f;
			}
			if (silvaCountdown <= 0 && hasSilvaEffect && silvaSummon)
			{
				Player.maxMinions += 2;
			}
			if (sDefense)
			{
				Player.statDefense += 5;
				Player.endurance += 0.05f;
			}
			if (absorber)
			{
				Player.moveSpeed += 0.12f;
				Player.jumpSpeedBoost += 1.2f;
				Player.statLifeMax2 += 20;
				Player.thorns = 0.5f;
				Player.endurance += 0.05f;
				if ((double)Math.Abs(Player.velocity.X) < 0.05 && (double)Math.Abs(Player.velocity.Y) < 0.05 && Player.itemAnimation == 0)
				{
					Player.lifeRegen += 2;
					Player.manaRegenBonus += 2;
				}
				if (Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir))
				{
					Player.statDefense += 5;
					Player.endurance += 0.05f;
					Player.moveSpeed += 0.2f;
				}
			}
			if (coreOfTheBloodGod)
			{
				Player.statLifeMax2 += Player.statLifeMax2 / 5 / 20 * 10;
			}
			if (bloodPact)
			{
				Player.statLifeMax2 += Player.statLifeMax2 / 5 / 20 * 100;
			}
			if (aAmpoule)
			{
				Lighting.AddLight((int)(Player.position.X + (float)(Player.width / 2)) / 16, (int)(Player.position.Y + (float)(Player.height / 2)) / 16, 1f, 1f, 0.6f);
				Player.endurance += 0.05f;
				Player.pickSpeed -= 0.25f;
				Player.buffImmune[70] = true;
				Player.buffImmune[47] = true;
				Player.buffImmune[46] = true;
				Player.buffImmune[44] = true;
				Player.buffImmune[20] = true;
				if (!Player.honey && Player.lifeRegen < 0)
				{
					Player.lifeRegen += 2;
					if (Player.lifeRegen > 0)
					{
						Player.lifeRegen = 0;
					}
				}
				Player.lifeRegenTime += 1;
				Player.lifeRegen += 2;
			}
			else if (cFreeze)
			{
				Lighting.AddLight((int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f), 0.3f, ((float)Main.DiscoG / 400f), 0.5f);
			}
			else if (sirenIce)
			{
				Lighting.AddLight((int)(Player.position.X + (float)(Player.width / 2)) / 16, (int)(Player.position.Y + (float)(Player.height / 2)) / 16, 0.35f, 1f, 1.25f);
			}
			else if (sirenBoobs || sirenBoobsAlt)
			{
				Lighting.AddLight((int)(Player.position.X + (float)(Player.width / 2)) / 16, (int)(Player.position.Y + (float)(Player.height / 2)) / 16, 1.5f, 1f, 0.1f);
			}
			else if (tarraSummon)
			{
				Lighting.AddLight((int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f), 0f, 3f, 0f);
			}
			if (cFreeze)
			{
				int num = Mod.Find<ModBuff>("GlacialState").Type;
				float num2 = 200f;
				int random = Main.rand.Next(5);
				if (Player.whoAmI == Main.myPlayer)
				{
					if (random == 0)
					{
						for (int l = 0; l < 200; l++)
						{
							NPC nPC = Main.npc[l];
							if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && !nPC.buffImmune[num] && Vector2.Distance(Player.Center, nPC.Center) <= num2)
							{
								if (nPC.FindBuffIndex(num) == -1)
								{
									nPC.AddBuff(num, 120, false);
								}
							}
						}
					}
				}
			}
			if (invincible || lol)
			{
				Player.thorns = 0f;
				Player.turtleThorns = false;
			}
			if (Player.vortexStealthActive)
			{
				Player.GetDamage(DamageClass.Ranged) -= (1f - Player.stealth) * 0.4f; //change 80 to 40
				Player.GetCritChance(DamageClass.Ranged) -= (int)((1f - Player.stealth) * 5f); //change 20 to 15
			}
			if (Player.inventory[Player.selectedItem].type != Mod.Find<ModItem>("Ataraxia").Type)
			{
				ataraxiaDamageBoost = 0f;
				ataraxiaDamageBoostCancelTimer = 1200;
			}
			else
			{
				if (ataraxiaDamageBoostCancelTimer > 0)
					ataraxiaDamageBoostCancelTimer--;
				if (ataraxiaDamageBoostCancelTimer <= 0)
				{
					if (ataraxiaDamageBoost > 0f)
						ataraxiaDamageBoost -= 0.01f;
					if (ataraxiaDamageBoost < 0f)
						ataraxiaDamageBoost = 0f;
				}
				if (ataraxiaDamageBoostCooldown > 0)
					ataraxiaDamageBoostCooldown--;
				if (ataraxiaDamageBoost > 0.3f)
					ataraxiaDamageBoost = 0.3f;
			}
			if (projRefRareLifeRegenCounter > 0)
			{
				projRefRareLifeRegenCounter--;
				Player.lifeRegenTime += 2;
				Player.lifeRegen += 2;
			}
			if (desertScourgeLore)
			{
				Player.statDefense += 10;
			}
			if (skeletronPrimeLore)
			{
				Player.GetArmorPenetration(DamageClass.Generic) += 5;
			}
			if (destroyerLore)
			{
				Player.pickSpeed -= 0.1f;
			}
			if (golemLore)
			{
				if ((double)Math.Abs(Player.velocity.X) < 0.05 && (double)Math.Abs(Player.velocity.Y) < 0.05 && Player.itemAnimation == 0)
				{
					Player.statDefense += 10;
				}
			}
			if (aquaticScourgeLore && Player.wellFed)
			{
				Player.statDefense += 1;
				Player.GetCritChance(DamageClass.Melee) += 1;
				Player.GetDamage(DamageClass.Melee) += 0.025f;
				Player.GetAttackSpeed(DamageClass.Melee) += 0.025f;
				Player.GetCritChance(DamageClass.Magic) += 1;
				Player.GetDamage(DamageClass.Magic) += 0.025f;
				Player.GetCritChance(DamageClass.Ranged) += 1;
				Player.GetDamage(DamageClass.Ranged) += 0.025f;
				Player.GetCritChance(DamageClass.Throwing) += 1;
				Player.GetDamage(DamageClass.Throwing) += 0.025f;
				Player.GetDamage(DamageClass.Summon) += 0.025f;
				Player.GetKnockback(DamageClass.Summon).Base += 0.25f;
				Player.moveSpeed += 0.1f;
			}
			if (eaterOfWorldsLore)
			{
				int damage = 20;
				float knockBack = 2f;
				if (Main.rand.Next(15) == 0)
				{
					int num = 0;
					for (int i = 0; i < 1000; i++)
					{
						if (Main.projectile[i].active && Main.projectile[i].owner == Player.whoAmI && Main.projectile[i].type == Mod.Find<ModProjectile>("TheDeadlyMicrobeProjectile").Type)
						{
							num++;
						}
					}
					if (Main.rand.Next(15) >= num && num < 6)
					{
						int num2 = 50;
						int num3 = 24;
						int num4 = 90;
						for (int j = 0; j < num2; j++)
						{
							int num5 = Main.rand.Next(200 - j * 2, 400 + j * 2);
							Vector2 center = Player.Center;
							center.X += (float)Main.rand.Next(-num5, num5 + 1);
							center.Y += (float)Main.rand.Next(-num5, num5 + 1);
							if (!Collision.SolidCollision(center, num3, num3) && !Collision.WetCollision(center, num3, num3))
							{
								center.X += (float)(num3 / 2);
								center.Y += (float)(num3 / 2);
								if (Collision.CanHit(new Vector2(Player.Center.X, Player.position.Y), 1, 1, center, 1, 1) || Collision.CanHit(new Vector2(Player.Center.X, Player.position.Y - 50f), 1, 1, center, 1, 1))
								{
									int num6 = (int)center.X / 16;
									int num7 = (int)center.Y / 16;
									bool flag = false;
									if (Main.rand.Next(3) == 0 && Main.tile[num6, num7] != null && Main.tile[num6, num7].WallType > 0)
									{
										flag = true;
									}
									else
									{
										center.X -= (float)(num4 / 2);
										center.Y -= (float)(num4 / 2);
										if (Collision.SolidCollision(center, num4, num4))
										{
											center.X += (float)(num4 / 2);
											center.Y += (float)(num4 / 2);
											flag = true;
										}
									}
									if (flag)
									{
										for (int k = 0; k < 1000; k++)
										{
											if (Main.projectile[k].active && Main.projectile[k].owner == Player.whoAmI && Main.projectile[k].type == Mod.Find<ModProjectile>("TheDeadlyMicrobeProjectile").Type && (center - Main.projectile[k].Center).Length() < 48f)
											{
												flag = false;
												break;
											}
										}
										if (flag && Main.myPlayer == Player.whoAmI)
										{
											Projectile.NewProjectile(Entity.GetSource_FromThis(null), center.X, center.Y, 0f, 0f, Mod.Find<ModProjectile>("TheDeadlyMicrobeProjectile").Type, damage, knockBack, Player.whoAmI, 0f, 0f);
										}
									}
								}
							}
						}
					}
				}
			}
			if (calcium)
			{
				Player.noFallDmg = true;
			}
			if (skeletronLore)
			{
				AllDamageBoost(0.05f);
				AllCritBoost(5);
				Player.statDefense += 10;
			}
			if (ceaselessHunger)
			{
				for (int j = 0; j < 400; j++)
				{
					if (Main.item[j].active && Main.item[j].noGrabDelay == 0 && Main.item[j].playerIndexTheItemIsReservedFor == Player.whoAmI)
					{
						int num = Main.maxTilesX;
						if (new Rectangle((int)Player.position.X - num, (int)Player.position.Y - num, Player.width + num * 2, Player.height + num * 2).Intersects(new Rectangle((int)Main.item[j].position.X, (int)Main.item[j].position.Y, Main.item[j].width, Main.item[j].height)))
						{
							Main.item[j].beingGrabbed = true;
							if ((double)Player.position.X + (double)Player.width * 0.5 > (double)Main.item[j].position.X + (double)Main.item[j].width * 0.5)
							{
								if (Main.item[j].velocity.X < 40f + Player.velocity.X)
								{
									Item item = Main.item[j];
									item.velocity.X = item.velocity.X + 4.5f;
								}
								if (Main.item[j].velocity.X < 0f)
								{
									Item item = Main.item[j];
									item.velocity.X = item.velocity.X + 4.5f * 0.75f;
								}
							}
							else
							{
								if (Main.item[j].velocity.X > -40f + Player.velocity.X)
								{
									Item item = Main.item[j];
									item.velocity.X = item.velocity.X - 4.5f;
								}
								if (Main.item[j].velocity.X > 0f)
								{
									Item item = Main.item[j];
									item.velocity.X = item.velocity.X - 4.5f * 0.75f;
								}
							}
							if ((double)Player.position.Y + (double)Player.height * 0.5 > (double)Main.item[j].position.Y + (double)Main.item[j].height * 0.5)
							{
								if (Main.item[j].velocity.Y < 40f)
								{
									Item item = Main.item[j];
									item.velocity.Y = item.velocity.Y + 4.5f;
								}
								if (Main.item[j].velocity.Y < 0f)
								{
									Item item = Main.item[j];
									item.velocity.Y = item.velocity.Y + 4.5f * 0.75f;
								}
							}
							else
							{
								if (Main.item[j].velocity.Y > -40f)
								{
									Item item = Main.item[j];
									item.velocity.Y = item.velocity.Y - 4.5f;
								}
								if (Main.item[j].velocity.Y > 0f)
								{
									Item item = Main.item[j];
									item.velocity.Y = item.velocity.Y - 4.5f * 0.75f;
								}
							}
						}
					}
				}
			}
			if (dukeFishronLore)
			{
				AllDamageBoost(0.05f);
				AllCritBoost(5);
				Player.moveSpeed += 0.15f;
			}
			if (lunaticCultistLore)
			{
				Player.endurance += 0.04f;
				Player.statDefense += 4;
				AllDamageBoost(0.04f);
				AllCritBoost(4);
				Player.GetKnockback(DamageClass.Summon).Base += 0.5f;
				Player.moveSpeed += 0.1f;
			}
			if (moonLordLore)
			{
				if (Player.gravDir == -1f && Player.gravControl2)
				{
					Player.endurance += 0.05f;
					Player.statDefense += 10;
					AllDamageBoost(0.1f);
					AllCritBoost(10);
					Player.GetKnockback(DamageClass.Summon).Base += 1.5f;
					Player.moveSpeed += 0.15f;
				}
			}
			if (leviathanAndSirenLore)
			{
				if (sirenBoobsPrevious || sirenBoobsAltPrevious)
				{
					Player.statLifeMax2 += Player.statLifeMax2 / 5 / 20 * 5;
				}
				if (sirenPet)
				{
					Player.spelunkerTimer += 1;
					if (Player.spelunkerTimer >= 10)
					{
						Player.spelunkerTimer = 0;
						int num65 = 30;
						int num66 = (int)Player.Center.X / 16;
						int num67 = (int)Player.Center.Y / 16;
						for (int num68 = num66 - num65; num68 <= num66 + num65; num68++)
						{
							for (int num69 = num67 - num65; num69 <= num67 + num65; num69++)
							{
								if (Main.rand.Next(4) == 0)
								{
									Vector2 vector = new Vector2((float)(num66 - num68), (float)(num67 - num69));
									if (vector.Length() < (float)num65 && num68 > 0 && num68 < Main.maxTilesX - 1 && num69 > 0 && num69 < Main.maxTilesY - 1 && Main.tile[num68, num69] != null && Main.tile[num68, num69].HasTile)
									{
										bool flag7 = false;
										if (Main.tile[num68, num69].TileType == 185 && Main.tile[num68, num69].TileFrameY == 18)
										{
											if (Main.tile[num68, num69].TileFrameX >= 576 && Main.tile[num68, num69].TileFrameX <= 882)
											{
												flag7 = true;
											}
										}
										else if (Main.tile[num68, num69].TileType == 186 && Main.tile[num68, num69].TileFrameX >= 864 && Main.tile[num68, num69].TileFrameX <= 1170)
										{
											flag7 = true;
										}
										if (flag7 || Main.tileSpelunker[(int)Main.tile[num68, num69].TileType] || (Main.tileAlch[(int)Main.tile[num68, num69].TileType] && Main.tile[num68, num69].TileType != 82))
										{
											int num70 = Dust.NewDust(new Vector2((float)(num68 * 16), (float)(num69 * 16)), 16, 16, 204, 0f, 0f, 150, default(Color), 0.3f);
											Main.dust[num70].fadeIn = 0.75f;
											Main.dust[num70].velocity *= 0.1f;
											Main.dust[num70].noLight = true;
										}
									}
								}
							}
						}
					}
				}
			}
			#endregion

			#region StandingStillEffects
			if (rogueStealthMax > 0f)
			{
				if (rogueStealth >= rogueStealthMax) //full stealth does more damage
				{
					if (playRogueStealthSound)
					{
						playRogueStealthSound = false;
						SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/RogueStealth"), Player.position);
					}
					//The first projectile thrown will inherit this damage, not the crit. Crit is calculated on enemy hits and is not tied to the projectile
					//The moment you use any item/weapon the crit multiplier from stealth will decrease, meaning you will never get the optimal boost
					//To remedy this, make stealth increase crit by a larger amount so that it means more
					//If not you risk the crit boost feeling negligible
					CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += rogueStealth * 1f; //100% max damage boost, 160% with Auric
				}
				else
				{
					playRogueStealthSound = true;
					CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += rogueStealth * 0.75f; //0% to 75% damage boost, 120% with Auric
				}
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += (int)(rogueStealth * 30f); //0% to 30% crit boost, 48% with Auric
				Player.moveSpeed += rogueStealth * 0.05f;
				Player.aggro -= (int)((rogueStealth / rogueStealthMax) * 900f);
				if (Player.itemAnimation > 0)
				{
					if (rogueStealth > 0f)
					{
						rogueStealth -= rogueStealthMax * 0.006f;
						if (rogueStealth < 0f)
							rogueStealth = 0f;
					}
				}
				else if ((((double)Math.Abs(Player.velocity.X) < 0.1 && (double)Math.Abs(Player.velocity.Y) < 0.1) || (penumbra && (!Main.dayTime || Main.eclipse))) && !Player.mount.Active)
				{
					if (rogueStealth < rogueStealthMax)
					{
						rogueStealth += rogueStealthMax * (Main.eclipse ? 0.012f : 0.006f); //180 ticks, 3 seconds to reach full stealth
						if (rogueStealth > rogueStealthMax)
							rogueStealth = rogueStealthMax;
					}
				}
			}
			else
				rogueStealth = 0f;

			if (trinketOfChi)
			{
				if (trinketOfChiBuff)
				{
					AllDamageBoost(0.5f);
					if (Player.itemAnimation > 0)
						chiBuffTimer = 0;
				}
				if ((double)Math.Abs(Player.velocity.X) < 0.1 && (double)Math.Abs(Player.velocity.Y) < 0.1 && !Player.mount.Active)
				{
					if (chiBuffTimer < 120)
						chiBuffTimer++;
					else
						Player.AddBuff(Mod.Find<ModBuff>("ChiBuff").Type, 6);
				}
				else
					chiBuffTimer--;
			}
			else
				chiBuffTimer = 0;

			if (aquaticEmblem)
			{
				if ((Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir) && Player.wet && !Player.lavaWet && !Player.honeyWet) &&
					!Player.mount.Active)
				{
					if (aquaticBoost > 0f)
					{
						aquaticBoost -= 0.0002f; //0.015
						if ((double)aquaticBoost <= 0.0)
						{
							aquaticBoost = 0f;
							if (Main.netMode == 1)
								NetMessage.SendData(84, -1, -1, null, Player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
				else
				{
					aquaticBoost += 0.0002f;
					if (aquaticBoost > 1f)
						aquaticBoost = 1f;
					if (Player.mount.Active)
						aquaticBoost = 1f;
				}
				Player.statDefense += (int)((1f - aquaticBoost) * 30f);
				Player.moveSpeed -= (1f - aquaticBoost) * 0.1f;
			}
			else
				aquaticBoost = 1f;

			if (auricBoost)
			{
				if (Player.itemAnimation > 0)
				{
					modStealthTimer = 5;
				}
				if ((double)Math.Abs(Player.velocity.X) < 0.1 && (double)Math.Abs(Player.velocity.Y) < 0.1 && !Player.mount.Active)
				{
					if (modStealthTimer == 0 && modStealth > 0f)
					{
						modStealth -= 0.015f;
						if ((double)modStealth <= 0.0)
						{
							modStealth = 0f;
							if (Main.netMode == 1)
								NetMessage.SendData(84, -1, -1, null, Player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
				else
				{
					float num27 = Math.Abs(Player.velocity.X) + Math.Abs(Player.velocity.Y);
					modStealth += num27 * 0.0075f;
					if (modStealth > 1f)
						modStealth = 1f;
					if (Player.mount.Active)
						modStealth = 1f;
				}
				float damageBoost = (1f - modStealth) * 0.2f;
				AllDamageBoost(damageBoost);
				int critBoost = (int)((1f - modStealth) * 10f);
				AllCritBoost(critBoost);
				if (modStealthTimer > 0)
					modStealthTimer--;
			}
			else if (pAmulet)
			{
				if (Player.itemAnimation > 0)
				{
					modStealthTimer = 5;
				}
				if ((double)Math.Abs(Player.velocity.X) < 0.1 && (double)Math.Abs(Player.velocity.Y) < 0.1 && !Player.mount.Active)
				{
					if (modStealthTimer == 0 && modStealth > 0f)
					{
						modStealth -= 0.015f;
						if ((double)modStealth <= 0.0)
						{
							modStealth = 0f;
							if (Main.netMode == 1)
								NetMessage.SendData(84, -1, -1, null, Player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
				else
				{
					float num27 = Math.Abs(Player.velocity.X) + Math.Abs(Player.velocity.Y);
					modStealth += num27 * 0.0075f;
					if (modStealth > 1f)
						modStealth = 1f;
					if (Player.mount.Active)
						modStealth = 1f;
				}
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += (1f - modStealth) * 0.2f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += (int)((1f - modStealth) * 10f);
				Player.aggro -= (int)((1f - modStealth) * 750f);
				if (modStealthTimer > 0)
					modStealthTimer--;
			}
			else
				modStealth = 1f;
			#endregion

			#region ElysianAegis
			if (elysianAegis)
			{
				bool flag14 = false;
				if (elysianGuard)
				{
					float num29 = shieldInvinc;
					shieldInvinc -= 0.08f;
					if (shieldInvinc < 0f)
					{
						shieldInvinc = 0f;
					}
					else
					{
						flag14 = true;
					}
					if (shieldInvinc == 0f && num29 != shieldInvinc && Main.netMode == 1)
					{
						NetMessage.SendData(84, -1, -1, null, Player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
					float damageBoost = (5f - shieldInvinc) * 0.03f;
					AllDamageBoost(damageBoost);
					int critBoost = (int)((5f - shieldInvinc) * 2f);
					AllCritBoost(critBoost);
					Player.aggro += (int)((5f - shieldInvinc) * 220f);
					Player.statDefense += (int)((5f - shieldInvinc) * 4f);
					Player.moveSpeed *= 0.85f;
					if (Player.mount.Active)
					{
						elysianGuard = false;
					}
				}
				else
				{
					float num30 = shieldInvinc;
					shieldInvinc += 0.08f;
					if (shieldInvinc > 5f)
					{
						shieldInvinc = 5f;
					}
					else
					{
						flag14 = true;
					}
					if (shieldInvinc == 5f && num30 != shieldInvinc && Main.netMode == 1)
					{
						NetMessage.SendData(84, -1, -1, null, Player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (flag14)
				{
					if (Main.rand.Next(2) == 0)
					{
						Vector2 vector = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						Dust dust = Main.dust[Dust.NewDust(Player.Center - vector * 30f, 0, 0, 244, 0f, 0f, 0, default(Color), 1f)];
						dust.noGravity = true;
						dust.position = Player.Center - vector * (float)Main.rand.Next(5, 11);
						dust.velocity = vector.RotatedBy(1.5707963705062866, default(Vector2)) * 4f;
						dust.scale = 0.5f + Main.rand.NextFloat();
						dust.fadeIn = 0.5f;
					}
					if (Main.rand.Next(2) == 0)
					{
						Vector2 vector2 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						Dust dust2 = Main.dust[Dust.NewDust(Player.Center - vector2 * 30f, 0, 0, 246, 0f, 0f, 0, default(Color), 1f)];
						dust2.noGravity = true;
						dust2.position = Player.Center - vector2 * 12f;
						dust2.velocity = vector2.RotatedBy(-1.5707963705062866, default(Vector2)) * 2f;
						dust2.scale = 0.5f + Main.rand.NextFloat();
						dust2.fadeIn = 0.5f;
					}
				}
			}
			else
			{
				elysianGuard = false;
			}
			#endregion

			#region OtherBuffs
			if (gravityNormalizer)
			{
				Player.buffImmune[BuffID.VortexDebuff] = true;
				float x = (float)(Main.maxTilesX / 4200);
				x *= x;
				float spaceGravityMult = (float)((double)(Player.position.Y / 16f - (60f + 10f * x)) / (Main.worldSurface / 6.0));
				if (spaceGravityMult < 1f)
				{
					Player.gravity = Player.defaultGravity;
					if (Player.wet)
					{
						if (Player.honeyWet)
							Player.gravity = 0.1f;
						else if (Player.merman)
							Player.gravity = 0.3f;
						else
							Player.gravity = 0.2f;
					}
				}
			}
			if (astralInjection)
			{
				if (Player.statMana < Player.statManaMax2)
				{
					Player.statMana += 3;
				}
				if (Player.statMana > Player.statManaMax2)
				{
					Player.statMana = Player.statManaMax2;
				}
			}
			if (armorCrumbling)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 5;
				Player.GetCritChance(DamageClass.Melee) += 5;
			}
			if (armorShattering)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.08f;
				Player.GetDamage(DamageClass.Melee) += 0.08f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 8;
				Player.GetCritChance(DamageClass.Melee) += 8;
			}
			if (holyWrath)
			{
				AllDamageBoost(0.12f);
				Player.moveSpeed += 0.05f;
			}
			if (profanedRage)
			{
				AllCritBoost(12);
				Player.moveSpeed += 0.05f;
			}
			if (irradiated)
			{
				Player.statDefense -= 10;
				AllDamageBoost(0.05f);
				Player.GetKnockback(DamageClass.Summon).Base += 0.5f;
				Player.moveSpeed += 0.05f;
			}
			if (rRage)
			{
				AllDamageBoost(0.05f);
				Player.moveSpeed += 0.05f;
			}
			if (xRage)
			{
				AllDamageBoost(0.1f);
			}
			if (xWrath)
			{
				AllCritBoost(5);
			}
			if (godSlayerCooldown)
			{
				AllDamageBoost(0.1f);
			}
			if (graxDefense)
			{
				Player.statDefense += 15;
				Player.endurance += 0.05f;
				Player.GetDamage(DamageClass.Melee) += 0.1f;
			}
			if (sMeleeBoost)
			{
				AllDamageBoost(0.1f);
				AllCritBoost(5);
			}
			if (tFury)
			{
				Player.GetDamage(DamageClass.Melee) += 0.3f;
				Player.GetCritChance(DamageClass.Melee) += 10;
			}
			if (yPower)
			{
				Player.endurance += 0.05f;
				Player.statDefense += 5;
				AllDamageBoost(0.06f);
				AllCritBoost(2);
				Player.GetKnockback(DamageClass.Summon).Base += 1f;
				Player.moveSpeed += 0.15f;
			}
			if (tScale)
			{
				Player.endurance += 0.05f;
				Player.statDefense += 5;
				Player.kbBuff = true;
			}
			if (darkSunRing)
			{
				Player.maxMinions += 2;
				AllDamageBoost(0.12f);
				Player.GetKnockback(DamageClass.Summon).Base += 1.2f;
				Player.pickSpeed -= 0.15f;
				if (Main.dayTime)
				{
					Player.lifeRegen += 3;
				}
				else
				{
					Player.statDefense += 30;
				}
			}
			if (eGauntlet)
			{
				Player.longInvince = true;
				Player.kbGlove = true;
				Player.GetDamage(DamageClass.Melee) += 0.15f;
				Player.GetCritChance(DamageClass.Melee) += 5;
				Player.lavaMax += 240;
			}
			if (fabsolVodka)
			{
				alcoholPoisonLevel++;
				AllDamageBoost(0.08f);
				Player.statDefense -= 20;
			}
			if (vodka)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen -= 1;
				Player.statDefense -= 4;
				AllDamageBoost(0.06f);
				AllCritBoost(2);
			}
			if (redWine)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen -= 1;
			}
			if (grapeBeer)
			{
				alcoholPoisonLevel++;
				Player.statDefense -= 2;
				Player.moveSpeed -= 0.05f;
			}
			if (moonshine)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen -= 1;
				Player.statDefense += 10;
				Player.endurance += 0.05f;
			}
			if (rum)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen += 2;
				Player.moveSpeed += 0.1f;
				Player.statDefense -= 8;
			}
			if (fireball)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen -= 1;
			}
			if (whiskey)
			{
				alcoholPoisonLevel++;
				Player.statDefense -= 8;
				AllDamageBoost(0.04f);
				AllCritBoost(2);
			}
			if (everclear)
			{
				alcoholPoisonLevel += 2;
				Player.lifeRegen -= 10;
				Player.statDefense -= 40;
				AllDamageBoost(0.25f);
			}
			if (bloodyMary)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen -= 2;
				if (Main.bloodMoon)
				{
					Player.statDefense -= 6;
					AllDamageBoost(0.15f);
					AllCritBoost(7);
					Player.moveSpeed += 0.15f;
				}
			}
			if (tequila)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen -= 1;
				if (Main.dayTime)
				{
					Player.statDefense += 5;
					AllDamageBoost(0.03f);
					AllCritBoost(2);
					Player.endurance += 0.03f;
				}
			}
			if (tequilaSunrise)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen -= 1;
				if (Main.dayTime)
				{
					Player.statDefense += 15;
					AllDamageBoost(0.07f);
					AllCritBoost(3);
					Player.endurance += 0.07f;
				}
			}
			if (screwdriver)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen -= 1;
			}
			if (caribbeanRum)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen += 2;
				Player.moveSpeed += 0.2f;
				Player.statDefense -= 12;
			}
			if (cinnamonRoll)
			{
				alcoholPoisonLevel++;
				Player.statDefense -= 12;
				Player.manaRegenDelay--;
				Player.manaRegenBonus += 10;
			}
			if (margarita)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen -= 1;
				Player.statDefense -= 6;
			}
			if (starBeamRye)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen -= 1;
				Player.statDefense -= 6;
				Player.GetDamage(DamageClass.Magic) += 0.08f;
				Player.manaCost *= 0.9f;
			}
			if (moscowMule)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen -= 2;
				AllDamageBoost(0.09f);
				AllCritBoost(3);
			}
			if (whiteWine)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen -= 1;
				Player.statDefense -= 6;
				Player.GetDamage(DamageClass.Magic) += 0.1f;
			}
			if (evergreenGin)
			{
				alcoholPoisonLevel++;
				Player.lifeRegen -= 1;
				Player.endurance += 0.05f;
			}
			if (alcoholPoisonLevel > 3)
			{
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 3 * alcoholPoisonLevel;
			}
			if (giantPearl)
			{
				if (Main.netMode != 1)
				{
					for (int m = 0; m < 200; m++)
					{
						if (Main.npc[m].active && !Main.npc[m].friendly)
						{
							float distance = (Main.npc[m].Center - Player.Center).Length();
							if (distance < 120f)
								Main.npc[m].AddBuff(Mod.Find<ModBuff>("PearlAura").Type, 2, false);
						}
					}
				}
			}
			if (normalityRelocator)
			{
				Player.moveSpeed += 0.1f;
				Player.maxFallSpeed *= 1.1f;
			}
			if (CalamityModClassicPreTrailer.scopedWeaponList.Contains(Player.inventory[Player.selectedItem].type))
			{
				Player.scope = true;
			}
			if (harpyRing)
			{
				if (Player.wingTimeMax > 0)
					Player.wingTimeMax = (int)((double)Player.wingTimeMax * 1.25);
				Player.moveSpeed += 0.2f;
			}
			if (blueCandle)
			{
				if (Player.wingTimeMax > 0)
					Player.wingTimeMax = (int)((double)Player.wingTimeMax * 1.1);
				Player.moveSpeed += 0.15f;
			}
			if (plaguebringerGoliathLore)
			{
				if (Player.wingTimeMax > 0)
					Player.wingTimeMax = (int)((double)Player.wingTimeMax * 1.25);
			}
			if (soaring)
			{
				if (Player.wingTimeMax > 0)
					Player.wingTimeMax = (int)((double)Player.wingTimeMax * 1.1);
			}
			if (draconicSurge)
			{
				if (Player.wingTimeMax > 0)
					Player.wingTimeMax = (int)((double)Player.wingTimeMax * 1.35);
			}
			if (bounding)
			{
				Player.jumpSpeedBoost += 0.5f;
				Player.jumpHeight += 10;
				Player.extraFall += 25;
			}
			if (community)
			{
				float floatTypeBoost = 0.01f +
					(NPC.downedSlimeKing ? 0.01f : 0f) +
					(NPC.downedBoss1 ? 0.01f : 0f) +
					(NPC.downedBoss2 ? 0.01f : 0f) +
					(NPC.downedQueenBee ? 0.01f : 0f) + //0.05
					(NPC.downedBoss3 ? 0.01f : 0f) +
					(Main.hardMode ? 0.01f : 0f) +
					(NPC.downedMechBossAny ? 0.01f : 0f) +
					(NPC.downedPlantBoss ? 0.01f : 0f) +
					(NPC.downedGolemBoss ? 0.01f : 0f) + //0.1
					(NPC.downedFishron ? 0.01f : 0f) +
					(NPC.downedAncientCultist ? 0.01f : 0f) +
					(NPC.downedMoonlord ? 0.01f : 0f) +
					(CalamityWorldPreTrailer.downedProvidence ? 0.02f : 0f) + //0.15
					(CalamityWorldPreTrailer.downedDoG ? 0.02f : 0f) + //0.17
					(CalamityWorldPreTrailer.downedYharon ? 0.03f : 0f); //0.2
				int integerTypeBoost = (int)(floatTypeBoost * 50f);
				int critBoost = integerTypeBoost / 2;
				int regenBoost = 1 + (integerTypeBoost / 5);
				float damageBoost = floatTypeBoost * 0.5f;
				Player.endurance += (floatTypeBoost * 0.25f);
				Player.statDefense += integerTypeBoost;
				AllDamageBoost(damageBoost);
				AllCritBoost(critBoost);
				Player.GetKnockback(DamageClass.Summon).Base += floatTypeBoost;
				Player.moveSpeed += floatTypeBoost;
				Player.statLifeMax2 += Player.statLifeMax / 5 / 20 * integerTypeBoost;
				bool lesserEffect = false;
				for (int l = 0; l < 22; l++)
				{
					int hasBuff = Player.buffType[l];
					bool shouldAffect = CalamityModClassicPreTrailer.alcoholList.Contains(hasBuff);
					if (shouldAffect)
						lesserEffect = true;
				}
				if (Player.lifeRegen < 0)
					Player.lifeRegen += lesserEffect ? 1 : regenBoost;
				if (Player.wingTimeMax > 0)
					Player.wingTimeMax = (int)((double)Player.wingTimeMax * 1.15);
			}
			if (ravagerLore)
			{
				if (Player.wingTimeMax > 0)
					Player.wingTimeMax = (int)((double)Player.wingTimeMax * 0.5);
				AllDamageBoost(0.05f);
			}
			if (corrEffigy)
			{
				Player.moveSpeed += 0.15f;
				AllCritBoost(10);
			}
			if (crimEffigy)
			{
				AllDamageBoost(0.15f);
				Player.statDefense += 10;
				Player.statLifeMax2 = (int)((double)Player.statLifeMax2 * 0.8);
			}
			if (badgeOfBraveryRare)
			{
				Player.GetDamage(DamageClass.Melee) += 0.2f;
				Player.statLifeMax2 = (int)((double)Player.statLifeMax2 * 0.75);
			}
			if (regenator)
			{
				Player.statLifeMax2 = (int)((double)Player.statLifeMax2 * 0.5);
				Player.lifeRegenTime += 8;
				Player.lifeRegen += 16;
			}
			if (calamitasLore)
			{
				Player.statLifeMax2 = (int)((double)Player.statLifeMax2 * 0.75);
				Player.maxMinions += 2;
			}
			if (pinkCandle)
			{
				if (pinkCandleTimer > 0) { pinkCandleTimer--; } // 1 per second
				if (pinkCandleTimer <= 0)
				{
					if (Player.statLife < Player.statLifeMax2)
					{
						float regen = (float)Player.statLifeMax2 * 0.004f;
						Player.statLife += (int)regen;
					}
					pinkCandleTimer = 60;
				}
			}
			if (rBrain)
			{
				if (Player.statLife <= (int)((double)Player.statLifeMax2 * 0.75))
					AllDamageBoost(0.1f);
				if (Player.statLife <= (int)((double)Player.statLifeMax2 * 0.5))
					Player.moveSpeed -= 0.05f;
			}
			if (bloodyWormTooth)
			{
				if (Player.statLife < (int)((double)Player.statLifeMax2 * 0.5))
				{
					Player.GetDamage(DamageClass.Melee) += 0.1f;
					Player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
					Player.endurance += 0.1f;
				}
				else
				{
					Player.GetDamage(DamageClass.Melee) += 0.05f;
					Player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
					Player.endurance += 0.05f;
				}
			}
			if (rampartOfDeities)
			{
				Player.panic = true;
				Player.GetArmorPenetration(DamageClass.Generic) += 50;
				Player.manaMagnet = true;
				Player.magicCuffs = true;
				if (Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir))
				{
					Lighting.AddLight((int)Player.Center.X / 16, (int)Player.Center.Y / 16, 1.35f, 0.3f, 0.9f);
				}
				Player.noKnockback = true;
				if (Player.statLife > (int)((double)Player.statLifeMax2 * 0.25))
				{
					Player.hasPaladinShield = true;
					if (Player.whoAmI != Main.myPlayer && Player.miscCounter % 10 == 0)
					{
						int myPlayer = Main.myPlayer;
						if (Main.player[myPlayer].team == Player.team && Player.team != 0)
						{
							float arg = Player.position.X - Main.player[myPlayer].position.X;
							float num3 = Player.position.Y - Main.player[myPlayer].position.Y;
							if ((float)Math.Sqrt((double)(arg * arg + num3 * num3)) < 800f)
							{
								Main.player[myPlayer].AddBuff(43, 20, true);
							}
						}
					}
				}
				if (Player.statLife <= (int)((double)Player.statLifeMax2 * 0.5))
				{
					Player.AddBuff(62, 5, true);
				}
				if (Player.statLife <= (int)((double)Player.statLifeMax2 * 0.15))
				{
					Player.endurance += 0.05f;
				}
			}
			else if (fBulwark)
			{
				Player.noKnockback = true;
				if (Player.statLife > (int)((double)Player.statLifeMax2 * 0.25))
				{
					Player.hasPaladinShield = true;
					if (Player.whoAmI != Main.myPlayer && Player.miscCounter % 10 == 0)
					{
						int myPlayer = Main.myPlayer;
						if (Main.player[myPlayer].team == Player.team && Player.team != 0)
						{
							float arg = Player.position.X - Main.player[myPlayer].position.X;
							float num3 = Player.position.Y - Main.player[myPlayer].position.Y;
							if ((float)Math.Sqrt((double)(arg * arg + num3 * num3)) < 800f)
							{
								Main.player[myPlayer].AddBuff(43, 20, true);
							}
						}
					}
				}
				if (Player.statLife <= (int)((double)Player.statLifeMax2 * 0.5))
				{
					Player.AddBuff(62, 5, true);
				}
				if (Player.statLife <= (int)((double)Player.statLifeMax2 * 0.15))
				{
					Player.endurance += 0.05f;
				}
			}
			if (frostFlare)
			{
				Player.resistCold = true;
				Player.buffImmune[44] = true;
				Player.buffImmune[46] = true;
				Player.buffImmune[47] = true;
				if (Player.statLife > (int)((double)Player.statLifeMax2 * 0.75))
				{
					AllDamageBoost(0.1f);
				}
				if (Player.statLife < (int)((double)Player.statLifeMax2 * 0.25))
				{
					Player.statDefense += 10;
				}
			}
			if (vexation)
			{
				if (Player.statLife < (int)((double)Player.statLifeMax2 * 0.5))
				{
					AllDamageBoost(0.15f);
				}
			}
			if (ataxiaBlaze)
			{
				if (Player.statLife <= (int)((double)Player.statLifeMax2 * 0.5))
				{
					Player.AddBuff(BuffID.Inferno, 2);
				}
			}
			if (bloodflareThrowing)
			{
				if (Player.statLife > (int)((double)Player.statLifeMax2 * 0.8))
				{
					CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 5;
					Player.statDefense += 30;
				}
				else
				{
					CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.1f;
				}
			}
			if (bloodflareSummon)
			{
				if (Player.statLife >= (int)((double)Player.statLifeMax2 * 0.9))
				{
					Player.GetDamage(DamageClass.Summon) += 0.1f;
				}
				else if (Player.statLife <= (int)((double)Player.statLifeMax2 * 0.5))
				{
					Player.statDefense += 20;
					Player.lifeRegen += 2;
				}
				if (bloodflareSummonTimer > 0) { bloodflareSummonTimer--; }
				if (Player.whoAmI == Main.myPlayer && bloodflareSummonTimer <= 0)
				{
					bloodflareSummonTimer = 900;
					for (int I = 0; I < 3; I++)
					{
						float ai1 = (float)(I * 120);
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X + (float)(Math.Sin(I * 120) * 550), Player.Center.Y + (float)(Math.Cos(I * 120) * 550), 0f, 0f,
							Mod.Find<ModProjectile>("GhostlyMine").Type, (int)((auricSet ? 15000f : 5000f) * player.GetDamage(DamageClass.Summon).Multiplicative), 1f, Player.whoAmI, ai1, 0f);
					}
				}
			}
			if (yInsignia)
			{
				Player.longInvince = true;
				Player.kbGlove = true;
				Player.GetDamage(DamageClass.Melee) += 0.05f;
				Player.lavaMax += 240;
				if (Player.statLife <= (int)((double)Player.statLifeMax2 * 0.5))
				{
					AllDamageBoost(0.1f);
				}
			}
			if (reaperToothNecklace)
			{
				AllDamageBoost(0.25f);
				Player.statDefense /= 2;
			}
			if (deepDiver)
			{
				AllDamageBoost(0.15f);
				Player.statDefense += (int)((double)Player.statDefense * 0.15);
				Player.moveSpeed += 0.15f;
			}
			if (coreOfTheBloodGod)
			{
				Player.endurance += 0.05f;
				AllDamageBoost(0.07f);
				if (Player.statDefense < 100)
				{
					AllDamageBoost(0.15f);
				}
			}
			else if (bloodflareCore)
			{
				if (Player.statDefense < 100)
				{
					AllDamageBoost(0.15f);
				}
				if (Player.statLife <= (int)((double)Player.statLifeMax2 * 0.15))
				{
					Player.endurance += 0.1f;
					AllDamageBoost(0.2f);
				}
				else if (Player.statLife <= (int)((double)Player.statLifeMax2 * 0.5))
				{
					Player.endurance += 0.05f;
					AllDamageBoost(0.1f);
				}
			}
			if (godSlayerThrowing)
			{
				if (Player.statLife >= Player.statLifeMax2)
				{
					CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 10;
					CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.1f;
					CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingVelocity += 0.1f;
				}
			}
			if (tarraSummon)
			{
				int lifeCounter = 0;
				float num2 = 300f;
				bool flag = lifeCounter % 60 == 0;
				int num3 = 200;
				if (Player.whoAmI == Main.myPlayer)
				{
					for (int l = 0; l < 200; l++)
					{
						NPC nPC = Main.npc[l];
						if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Player.Center, nPC.Center) <= num2)
						{
							if (flag)
							{
								nPC.StrikeNPC(nPC.CalculateHitInfo(num3, 0));
								if (Main.netMode != 0)
								{
									NetMessage.SendData(28, -1, -1, null, l, (float)num3, 0f, 0f, 0, 0, 0);
								}
							}
						}
					}
				}
				lifeCounter++;
				if (lifeCounter >= 180)
				{
					lifeCounter = 0;
				}
				if (Player.statLife >= Player.statLifeMax2)
				{
					Player.GetDamage(DamageClass.Summon) += 0.1f;
					Player.maxMinions += 2;
				}
			}
			if (brimstoneElementalLore && Player.inferno)
			{
				int num = Mod.Find<ModBuff>("BrimstoneFlames").Type;
				float num2 = 300f;
				bool flag = Player.infernoCounter % 30 == 0;
				int damage = 50;
				if (Player.whoAmI == Main.myPlayer)
				{
					for (int l = 0; l < 200; l++)
					{
						NPC nPC = Main.npc[l];
						if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Player.Center, nPC.Center) <= num2)
						{
							if (nPC.FindBuffIndex(num) == -1 && !nPC.buffImmune[num])
							{
								nPC.AddBuff(num, 120, false);
							}
							if (flag)
							{
								Player.ApplyDamageToNPC(nPC, damage, 0f, 0, false);
							}
						}
					}
				}
			}
			if (plaguebringerGoliathLore)
			{
				Player.lifeRegen /= 2;
			}
			#endregion

			#region LimitsAndOtherShit
			if (Player.GetAttackSpeed(DamageClass.Melee) < 0.5f)
			{
				Player.GetAttackSpeed(DamageClass.Melee) = 0.5f;
			}
			if (auricSet && silvaMelee)
			{
				double multiplier = (double)Player.statLife / (double)Player.statLifeMax2;
				Player.GetDamage(DamageClass.Melee) += (float)(multiplier * 0.2); //ranges from 1.2 times to 1 times
			}
			if (Player.endurance > defEndurance) //0.33
			{
				float damageReductionAboveCap = Player.endurance - defEndurance; //0.6 - 0.33 = 0.27
				Player.statDefense += (int)((double)damageReductionAboveCap * 100.0);
				Player.endurance = defEndurance + (damageReductionAboveCap * 0.1f); //0.33 + (0.27 * 0.1) = 0.357
			}
			if (vHex)
			{
				Player.endurance -= 0.3f;
			}
			if (irradiated)
			{
				Player.endurance -= 0.1f;
			}
			if (corrEffigy)
			{
				Player.endurance -= 0.2f;
			}
			if (CalamityWorldPreTrailer.revenge)
			{
				if (Player.statLife < Player.statLifeMax2)
				{
					bool noLifeRegenCap = ((Player.shinyStone || draedonsHeart || cFreeze || shadeRegen || photosynthesis) &&
						(double)Math.Abs(Player.velocity.X) < 0.05 && (double)Math.Abs(Player.velocity.Y) < 0.05 && Player.itemAnimation == 0);
					if (!noLifeRegenCap)
					{
						//Max HP = 400
						//350 HP = 1 - 0.875 * 10 = 1.25 = 1
						//100 HP = 1 - 0.25 * 10 = 7.5 = 7
						//200 HP = 1 - 0.5 * 10 = 5
						int lifeRegenScale = (int)((1f - ((float)Player.statLife / (float)Player.statLifeMax2)) * 10f); //9 to 0 (1% HP to 100%)
						if (Player.lifeRegen > lifeRegenScale)
						{
							double lifeRegenScalar = (double)(1f + ((float)Player.statLife / (float)Player.statLifeMax2)); //1 to 2 (1% HP to 100%)
							int defLifeRegen = (int)((double)Player.lifeRegen / lifeRegenScalar);
							Player.lifeRegen = defLifeRegen;
						}
					}
				}
			}
			if (CalamityWorldPreTrailer.bossRushActive)
			{
				if (Config.BossRushHealthCurse)
				{
					if (Player.lifeRegen > 0)
						Player.lifeRegen = 0;

					Player.lifeRegenTime = 0;

					if (Player.lifeRegenCount > 0)
						Player.lifeRegenCount = 0;
				}
			}
			if (Config.ProficiencyEnabled)
			{
				GetStatBonuses();
			}
			if (dArtifact)
			{
				AllDamageBoost(0.25f);
			}
			if (trippy)
			{
				AllDamageBoost(0.5f);
			}
			if (eArtifact)
			{
				Player.manaCost *= 0.85f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.15f;
				Player.maxMinions += 2;
			}
			if (gArtifact)
			{
				Player.maxMinions += 8;
				if (Player.whoAmI == Main.myPlayer)
				{
					if (Player.FindBuffIndex(Mod.Find<ModBuff>("AngryChicken").Type) == -1)
					{
						Player.AddBuff(Mod.Find<ModBuff>("AngryChicken").Type, 3600, true);
					}
					if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("AngryChicken").Type] < 2)
					{
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("AngryChicken").Type, (int)(232f * player.GetDamage(DamageClass.Summon).Multiplicative), 2f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (pArtifact)
			{
				if (Player.whoAmI == Main.myPlayer)
				{
					if (Player.FindBuffIndex(Mod.Find<ModBuff>("GuardianHealer").Type) == -1)
					{
						Player.AddBuff(Mod.Find<ModBuff>("GuardianHealer").Type, 3600, true);
					}
					if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("MiniGuardianHealer").Type] < 1)
					{
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, -6f, Mod.Find<ModProjectile>("MiniGuardianHealer").Type, 0, 0f, Main.myPlayer, 0f, 0f);
					}
					float baseDamage = 100f +
						(CalamityWorldPreTrailer.downedDoG ? 100f : 0f) +
						(CalamityWorldPreTrailer.downedYharon ? 100f : 0f);
					if (Player.maxMinions >= 8)
					{
						if (Player.FindBuffIndex(Mod.Find<ModBuff>("GuardianDefense").Type) == -1)
						{
							Player.AddBuff(Mod.Find<ModBuff>("GuardianDefense").Type, 3600, true);
						}
						if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("MiniGuardianDefense").Type] < 1)
						{
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, -3f, Mod.Find<ModProjectile>("MiniGuardianDefense").Type, (int)(baseDamage * player.GetDamage(DamageClass.Summon).Multiplicative), 1f, Main.myPlayer, 0f, 0f);
						}
					}
					if (tarraSummon || bloodflareSummon || godSlayerSummon || silvaSummon || dsSetBonus || omegaBlueSet)
					{
						if (Player.FindBuffIndex(Mod.Find<ModBuff>("GuardianOffense").Type) == -1)
						{
							Player.AddBuff(Mod.Find<ModBuff>("GuardianOffense").Type, 3600, true);
						}
						if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("MiniGuardianAttack").Type] < 1)
						{
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("MiniGuardianAttack").Type, (int)(baseDamage * player.GetDamage(DamageClass.Summon).Multiplicative), 1f, Main.myPlayer, 0f, 0f);
						}
					}
				}
			}
			if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("BigBustyRose").Type] > 1 || Player.ownedProjectileCounts[Mod.Find<ModProjectile>("SirenLure").Type] > 1 ||
				Player.ownedProjectileCounts[Mod.Find<ModProjectile>("DrewsSandyWaifu").Type] > 1 || Player.ownedProjectileCounts[Mod.Find<ModProjectile>("SandyWaifu").Type] > 1 ||
				Player.ownedProjectileCounts[Mod.Find<ModProjectile>("CloudyWaifu").Type] > 1 || Player.ownedProjectileCounts[Mod.Find<ModProjectile>("FungalClump").Type] > 1)
			{
				for (int projIndex = 0; projIndex < 1000; projIndex++)
				{
					if (Main.projectile[projIndex].active && Main.projectile[projIndex].owner == Player.whoAmI)
					{
						if (Main.projectile[projIndex].type == Mod.Find<ModProjectile>("BigBustyRose").Type || Main.projectile[projIndex].type == Mod.Find<ModProjectile>("SirenLure").Type ||
							Main.projectile[projIndex].type == Mod.Find<ModProjectile>("DrewsSandyWaifu").Type || Main.projectile[projIndex].type == Mod.Find<ModProjectile>("SandyWaifu").Type ||
							Main.projectile[projIndex].type == Mod.Find<ModProjectile>("CloudyWaifu").Type || Main.projectile[projIndex].type == Mod.Find<ModProjectile>("FungalClump").Type)
						{
							Main.projectile[projIndex].Kill();
						}
					}
				}
			}
			if (marked || reaperToothNecklace)
			{
				if (Player.endurance > 0f)
					Player.endurance *= 0.5f;
			}
			if (yharonLore && !CalamityWorldPreTrailer.defiled)
			{
				if (Player.wingTimeMax < 50000)
				{
					Player.wingTimeMax = 50000;
				}
			}
			#endregion
		}

		public override void PostUpdateRunSpeeds()
		{
			#region SpeedBoosts
			float runAccMult = 1f +
				((hAttack && !Main.dayTime) ? 0.05f : 0f) +
				((stressPills || laudanum || draedonsHeart) ? 0.05f : 0f) +
				((abyssalDivingSuit && Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir)) ? 0.05f : 0f) +
				(sirenWaterBuff ? 0.15f : 0f) +
				((frostFlare && Player.statLife < (int)((double)Player.statLifeMax2 * 0.25)) ? 0.15f : 0f) +
				(shadowSpeed ? 0.5f : 0f) +
				(auricSet ? 0.1f : 0f) +
				(silvaSet ? 0.05f : 0f) +
				(cTracers ? 0.1f : 0f) +
				(eTracers ? 0.05f : 0f) +
				(blueCandle ? 0.05f : 0f) +
				((deepDiver && Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir)) ? 0.15f : 0f) +
				(rogueStealthMax > 0f ? (rogueStealth >= rogueStealthMax ? rogueStealth * 0.05f : rogueStealth * 0.025f) : 0f);

			float runSpeedMult = 1f +
				((hAttack && !Main.dayTime) ? 0.05f : 0f) +
				((stressPills || laudanum || draedonsHeart) ? 0.05f : 0f) +
				((abyssalDivingSuit && Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir)) ? 0.05f : 0f) +
				(sirenWaterBuff ? 0.15f : 0f) +
				((frostFlare && Player.statLife < (int)((double)Player.statLifeMax2 * 0.25)) ? 0.15f : 0f) +
				(shadowSpeed ? 0.5f : 0f) +
				(auricSet ? 0.1f : 0f) +
				(silvaSet ? 0.05f : 0f) +
				(cTracers ? 0.1f : 0f) +
				(eTracers ? 0.05f : 0f) +
				((deepDiver && Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir)) ? 0.15f : 0f) +
				(rogueStealthMax > 0f ? (rogueStealth >= rogueStealthMax ? rogueStealth * 0.05f : rogueStealth * 0.025f) : 0f);

			if (abyssalDivingSuit && !Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir))
			{
				runAccMult *= 0.4f;
				runSpeedMult *= 0.4f;
			}
			if (horror)
			{
				runAccMult *= 0.85f;
				runSpeedMult *= 0.85f;
			}
			if (fabledTortoise)
			{
				runAccMult *= 0.5f;
				runSpeedMult *= 0.5f;
			}
			if (elysianGuard)
			{
				runAccMult *= 0.5f;
				runSpeedMult *= 0.5f;
			}
			if (Player.powerrun && CalamityWorldPreTrailer.revenge)
			{
				runSpeedMult *= 0.6666667f;
			}

			Player.runAcceleration *= runAccMult;
			Player.maxRunSpeed *= runSpeedMult;
			#endregion

			#region DashEffects
			if (Player.pulley && dashMod > 0)
			{
				ModDashMovement();
			}
			else if (Player.grappling[0] == -1 && !Player.tongued)
			{
				ModHorizontalMovement();
				if (dashMod > 0)
				{
					ModDashMovement();
				}
				if (pAmulet && modStealth < 1f)
				{
					float num43 = Player.maxRunSpeed / 2f * (1f - modStealth);
					Player.maxRunSpeed -= num43;
					Player.accRunSpeed = Player.maxRunSpeed;
				}
			}
			#endregion
		}
		#endregion

		#region PreKill
		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (invincible && Player.inventory[Player.selectedItem].type != Mod.Find<ModItem>("ColdheartIcicle").Type)
			{
				if (Player.statLife <= 0)
				{
					Player.statLife = 1;
				}
				return false;
			}
			if (hInferno)
			{
				for (int x = 0; x < 200; x++)
				{
					if (Main.npc[x].active && Main.npc[x].type == Mod.Find<ModNPC>("Providence").Type)
					{
						Main.npc[x].active = false;
					}
				}
			}
			if (nCore && Main.rand.Next(10) == 0)
			{
				SoundEngine.PlaySound(SoundID.Item67, Player.position);
				for (int j = 0; j < 25; j++)
				{
					int num = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 173, 0f, 0f, 100, default(Color), 2f);
					Dust expr_A4_cp_0 = Main.dust[num];
					expr_A4_cp_0.position.X = expr_A4_cp_0.position.X + (float)Main.rand.Next(-20, 21);
					Dust expr_CB_cp_0 = Main.dust[num];
					expr_CB_cp_0.position.Y = expr_CB_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
					Main.dust[num].velocity *= 0.9f;
					Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					}
				}
				Player.statLife += 100;
				Player.HealEffect(100);
				if (Player.statLife > Player.statLifeMax2)
				{
					Player.statLife = Player.statLifeMax2;
				}
				return false;
			}
			if (godSlayer && !godSlayerCooldown)
			{
				SoundEngine.PlaySound(SoundID.Item67, Player.position);
				for (int j = 0; j < 50; j++)
				{
					int num = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 173, 0f, 0f, 100, default(Color), 2f);
					Dust expr_A4_cp_0 = Main.dust[num];
					expr_A4_cp_0.position.X = expr_A4_cp_0.position.X + (float)Main.rand.Next(-20, 21);
					Dust expr_CB_cp_0 = Main.dust[num];
					expr_CB_cp_0.position.Y = expr_CB_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
					Main.dust[num].velocity *= 0.9f;
					Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					}
				}
				int heal = draconicSurge ? Player.statLifeMax2 : 150;
				Player.statLife += heal;
				Player.HealEffect(heal);
				if (Player.statLife > Player.statLifeMax2)
				{
					Player.statLife = Player.statLifeMax2;
				}
				if (Player.FindBuffIndex(Mod.Find<ModBuff>("DraconicSurgeBuff").Type) > -1)
				{
					Player.ClearBuff(Mod.Find<ModBuff>("DraconicSurgeBuff").Type);
					draconicSurgeCooldown = 1800;
				}
				Player.AddBuff(Mod.Find<ModBuff>("GodSlayerCooldown").Type, 2700);
				return false;
			}
			if (silvaSet && silvaCountdown > 0)
			{
				if (hasSilvaEffect)
				{
					silvaHitCounter++;
				}
				if (Player.FindBuffIndex(Mod.Find<ModBuff>("SilvaRevival").Type) == -1)
				{
					SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/SilvaActivation"), Player.position);
					Player.AddBuff(Mod.Find<ModBuff>("SilvaRevival").Type, 600);
					if (draconicSurge)
					{
						Player.statLife += Player.statLifeMax2;
						Player.HealEffect(Player.statLifeMax2);
						if (Player.statLife > Player.statLifeMax2)
						{
							Player.statLife = Player.statLifeMax2;
						}
						if (Player.FindBuffIndex(Mod.Find<ModBuff>("DraconicSurgeBuff").Type) > -1)
						{
							Player.ClearBuff(Mod.Find<ModBuff>("DraconicSurgeBuff").Type);
							draconicSurgeCooldown = 1800;
						}
					}
				}
				hasSilvaEffect = true;
				if (Player.statLife < 1)
				{
					Player.statLife = 1;
				}
				return false;
			}
			if (permafrostsConcoction && Player.FindBuffIndex(Mod.Find<ModBuff>("ConcoctionCooldown").Type) == -1)
			{
				Player.AddBuff(Mod.Find<ModBuff>("ConcoctionCooldown").Type, 10800);
				Player.AddBuff(Mod.Find<ModBuff>("Encased").Type, 300);
				Player.statLife = Player.statLifeMax2 * 3 / 10;
				SoundEngine.PlaySound(SoundID.Item92, Player.position);
				for (int i = 0; i < 60; i++)
				{
					int d = Dust.NewDust(Player.position, Player.width, Player.height, 88, 0f, 0f, 0, default(Color), 2.5f);
					Main.dust[d].noGravity = true;
					Main.dust[d].velocity *= 5f;
				}
				return false;
			}
			if (bBlood && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(Player.name + " became a blood geyser.");
			}
			if ((bFlames || aFlames) && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was consumed by the black flames.");
			}
			if (pFlames && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(Player.name + "'s flesh was melted by the plague.");
			}
			if ((hFlames || hInferno) && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(Player.name + " fell prey to their sins.");
			}
			if (NPC.AnyNPCs(Mod.Find<ModNPC>("SupremeCalamitas").Type))
			{
				if (sCalDeathCount < 51)
				{
					sCalDeathCount++;
				}
			}
			deathCount++;
			if (Player.whoAmI == Main.myPlayer && Main.netMode == 1)
			{
				DeathPacket(false);
			}
			if (CalamityWorldPreTrailer.ironHeart && areThereAnyDamnBosses)
			{
				KillPlayer();
				return false;
			}
			return true;
		}
		#endregion

		#region UseTimeMult
		public override float UseTimeMultiplier(Item item)
		{
			if (silvaRanged)
			{
				if (item.CountsAsClass(DamageClass.Ranged) && !item.GetGlobalItem<CalamityGlobalItem>().rogue && item.useTime > 3)
					return (auricSet ? 1.2f : 1.1f);
			}
			if (silvaThrowing)
			{
				if (Player.statLife > (int)((double)Player.statLifeMax2 * 0.5) &&
					item.GetGlobalItem<CalamityGlobalItem>().rogue && item.useTime > 3)
					return 1.1f;
			}
			return 1f;
		}
		#endregion

		#region GetWeaponDamageAndKB
		
		public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
		{
			bool isTrueMelee = item.CountsAsClass(DamageClass.Melee) && (item.shoot == 0 || (item.noMelee && item.noUseGraphic && item.useStyle == 5 && !CalamityModClassicPreTrailer.trueMeleeBoostExceptionList.Contains(item.type)));
			if (isTrueMelee)
			{
				double damageMult = 1.0 +
					(dodgeScarf ? 0.2 : 0.0) +
					((aBulwarkRare && aBulwarkRareMeleeBoostTimer > 0) ? 2.0 : 0.0) +
					(DoGLore ? 0.5 : 0.0) +
					(fungalSymbiote ? 0.25 : 0.0);

				damage *= (float)damageMult;
			}
			if (flamethrowerBoost && item.CountsAsClass(DamageClass.Ranged) && item.useAmmo == 23)
			{
				damage *= 1.25f;
			}
			if ((cinnamonRoll && CalamityModClassicPreTrailer.fireWeaponList.Contains(item.type)) || (evergreenGin && CalamityModClassicPreTrailer.natureWeaponList.Contains(item.type)))
			{
				damage *= 1.15f;
			}
			if (fireball && CalamityModClassicPreTrailer.fireWeaponList.Contains(item.type))
			{
				damage *= 1.1f;
			}
			if (theBee && Player.statLife >= Player.statLifeMax2)
			{
				if (item.CountsAsClass(DamageClass.Melee) || item.CountsAsClass(DamageClass.Ranged) || item.CountsAsClass(DamageClass.Magic) || item.GetGlobalItem<CalamityGlobalItem>().rogue)
				{
					double useTimeBeeMultiplier = (double)(item.useTime * item.useAnimation) / 3600.0; //28 * 28 = 784 is average so that equals 784 / 3600 = 0.217777 = 21.7% boost
					if (useTimeBeeMultiplier > 1.5)
						useTimeBeeMultiplier = 1.5;
					theBeeDamage = (int)((double)item.damage * useTimeBeeMultiplier);
				}
			}
			if (!theBee)
			{
				theBeeDamage = 0;
			}
		}

		public override void ModifyWeaponKnockback(Item item, ref StatModifier knockback)
		{
			if (auricBoost)
			{
				knockback *= 1f + (1f - modStealth) * 0.5f;
			}
			if (whiskey)
			{
				knockback *= 1.04f;
			}
			if (tequila && Main.dayTime)
			{
				knockback *= 1.03f;
			}
			if (tequilaSunrise && Main.dayTime)
			{
				knockback *= 1.07f;
			}
			if (moscowMule)
			{
				knockback *= 1.09f;
			}
		}
		#endregion

		#region MeleeEffects
		public override void MeleeEffects(Item item, Rectangle hitbox)
		{
			bool isTrueMelee = item.CountsAsClass(DamageClass.Melee) && (item.shoot == 0 || (item.noMelee && item.noUseGraphic && item.useStyle == 5 && !CalamityModClassicPreTrailer.trueMeleeBoostExceptionList.Contains(item.type)));
			if (isTrueMelee)
			{
				if (fungalSymbiote && Player.whoAmI == Main.myPlayer)
				{
					if ((Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.1) ||
						Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.3) ||
						Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.5) ||
						Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.7) ||
						Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.9)))
					{
						float num339 = 0f;
						float num340 = 0f;
						float num341 = 0f;
						float num342 = 0f;
						if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.9))
						{
							num339 = -7f;
						}
						if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.7))
						{
							num339 = -6f;
							num340 = 2f;
						}
						if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.5))
						{
							num339 = -4f;
							num340 = 4f;
						}
						if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.3))
						{
							num339 = -2f;
							num340 = 6f;
						}
						if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.1))
						{
							num340 = 7f;
						}
						if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.7))
						{
							num342 = 26f;
						}
						if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.3))
						{
							num342 -= 4f;
							num341 -= 20f;
						}
						if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.1))
						{
							num341 += 6f;
						}
						if (Player.direction == -1)
						{
							if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.9))
							{
								num342 -= 8f;
							}
							if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.7))
							{
								num342 -= 6f;
							}
						}
						num339 *= 1.5f;
						num340 *= 1.5f;
						num342 *= (float)Player.direction;
						num341 *= Player.gravDir;
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), (float)(hitbox.X + hitbox.Width / 2) + num342, (float)(hitbox.Y + hitbox.Height / 2) + num341, (float)Player.direction * num340, num339 * Player.gravDir, ProjectileID.Mushroom, (int)((float)item.damage * 0.25f * Player.GetDamage(DamageClass.Melee).Base), 0f, Player.whoAmI, 0f, 0f);
					}
				}
				if (aWeapon)
				{
					if (Main.rand.Next(3) == 0)
					{
						int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<BrimstoneFlame>(), Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
					}
				}
				if (aChicken)
				{
					if (Main.rand.Next(3) == 0)
					{
						int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 244, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
					}
				}
				if (eGauntlet)
				{
					if (Main.rand.Next(3) == 0)
					{
						int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 66, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.25f);
						Main.dust[num280].noGravity = true;
					}
				}
				if (cryogenSoul)
				{
					if (Main.rand.Next(3) == 0)
					{
						int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 67, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
					}
				}
				if (xerocSet)
				{
					if (Main.rand.Next(3) == 0)
					{
						int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 58, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 1.25f);
					}
				}
				if (reaverBlast)
				{
					if (Main.rand.Next(3) == 0)
					{
						int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 74, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
					}
				}
				if (dsSetBonus)
				{
					if (Main.rand.Next(3) == 0)
					{
						int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 27, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 2.5f);
					}
				}
			}
		}
		#endregion

		#region OnHitNPC
		public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
		{
			if (omegaBlueChestplate)
				target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 240);

			if (eGauntlet)
			{
				target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 120, false);
				target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120, false);
				target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 120, false);
				target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 120, false);
				if (Main.rand.Next(5) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 120, false);
				}
				target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 120, false);
				target.AddBuff(BuffID.Poisoned, 120, false);
				target.AddBuff(BuffID.OnFire, 120, false);
				target.AddBuff(BuffID.CursedInferno, 120, false);
				target.AddBuff(BuffID.Frostburn, 120, false);
				target.AddBuff(BuffID.Ichor, 120, false);
				target.AddBuff(BuffID.Venom, 120, false);
			}
			if (aWeapon)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 360, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 240, false);
				}
				else
				{
					target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 120, false);
				}
			}
			if (abyssalAmulet)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 360, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 240, false);
				}
				else
				{
					target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 120, false);
				}
			}
			if (dsSetBonus)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("DemonFlames").Type, 360, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("DemonFlames").Type, 240, false);
				}
				else
				{
					target.AddBuff(Mod.Find<ModBuff>("DemonFlames").Type, 120, false);
				}
			}
			if (cryogenSoul || frostFlare)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(44, 360, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(44, 240, false);
				}
				else
				{
					target.AddBuff(44, 120, false);
				}
			}
			if (yInsignia)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 360, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 240, false);
				}
				else
				{
					target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120, false);
				}
			}
			if (ataxiaFire)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(BuffID.OnFire, 720, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(BuffID.OnFire, 480, false);
				}
				else
				{
					target.AddBuff(BuffID.OnFire, 240, false);
				}
			}
			if (alchFlask)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 360, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 240, false);
				}
				else
				{
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 120, false);
				}
			}
			if (armorCrumbling || armorShattering)
			{
				if (item.CountsAsClass(DamageClass.Melee) || item.GetGlobalItem<CalamityGlobalItem>().rogue)
				{
					if (Main.rand.Next(4) == 0)
					{
						target.AddBuff(Mod.Find<ModBuff>("ArmorCrunch").Type, 360, false);
					}
					else if (Main.rand.Next(2) == 0)
					{
						target.AddBuff(Mod.Find<ModBuff>("ArmorCrunch").Type, 240, false);
					}
					else
					{
						target.AddBuff(Mod.Find<ModBuff>("ArmorCrunch").Type, 120, false);
					}
				}
			}
			if (holyWrath)
			{
				target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 600, false);
			}
		}
		#endregion

		#region OnHitNPCWithProj
		public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
		{
			if (omegaBlueChestplate && proj.friendly && !target.friendly)
				target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 240);

			if (proj.CountsAsClass(DamageClass.Melee) && silvaMelee && Main.rand.Next(4) == 0)
				target.AddBuff(Mod.Find<ModBuff>("SilvaStun").Type, 20);

			if (abyssalAmulet)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 360, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 240, false);
				}
				else
				{
					target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 120, false);
				}
			}
			if (dsSetBonus)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("DemonFlames").Type, 360, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("DemonFlames").Type, 240, false);
				}
				else
				{
					target.AddBuff(Mod.Find<ModBuff>("DemonFlames").Type, 120, false);
				}
			}
			if (uberBees && (proj.type == 566 || proj.type == 181 || proj.type == 189))
			{
				target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 360);
			}
			else if (alchFlask)
			{
				if (Main.rand.Next(4) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 360, false);
				}
				else if (Main.rand.Next(2) == 0)
				{
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 240, false);
				}
				else
				{
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 120, false);
				}
			}
			if (proj.CountsAsClass(DamageClass.Melee))
			{
				if (eGauntlet)
				{
					target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 120, false);
					target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120, false);
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 120, false);
					target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 120, false);
					if (Main.rand.Next(5) == 0)
					{
						target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 120, false);
					}
					target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 120, false);
					target.AddBuff(BuffID.Poisoned, 120, false);
					target.AddBuff(BuffID.OnFire, 120, false);
					target.AddBuff(BuffID.CursedInferno, 120, false);
					target.AddBuff(BuffID.Frostburn, 120, false);
					target.AddBuff(BuffID.Ichor, 120, false);
					target.AddBuff(BuffID.Venom, 120, false);
				}
				if (aWeapon)
				{
					if (Main.rand.Next(4) == 0)
					{
						target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 360, false);
					}
					else if (Main.rand.Next(2) == 0)
					{
						target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 240, false);
					}
					else
					{
						target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 120, false);
					}
				}
				if (cryogenSoul || frostFlare)
				{
					if (Main.rand.Next(4) == 0)
					{
						target.AddBuff(44, 360, false);
					}
					else if (Main.rand.Next(2) == 0)
					{
						target.AddBuff(44, 240, false);
					}
					else
					{
						target.AddBuff(44, 120, false);
					}
				}
				if (yInsignia)
				{
					if (Main.rand.Next(4) == 0)
					{
						target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 360, false);
					}
					else if (Main.rand.Next(2) == 0)
					{
						target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 240, false);
					}
					else
					{
						target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120, false);
					}
				}
				if (ataxiaFire)
				{
					if (Main.rand.Next(4) == 0)
					{
						target.AddBuff(BuffID.OnFire, 720, false);
					}
					else if (Main.rand.Next(2) == 0)
					{
						target.AddBuff(BuffID.OnFire, 480, false);
					}
					else
					{
						target.AddBuff(BuffID.OnFire, 240, false);
					}
				}
			}
			if (armorCrumbling || armorShattering)
			{
				if (proj.CountsAsClass(DamageClass.Melee) || proj.GetGlobalProjectile<CalamityGlobalProjectile>().rogue)
				{
					if (Main.rand.Next(4) == 0)
					{
						target.AddBuff(Mod.Find<ModBuff>("ArmorCrunch").Type, 360, false);
					}
					else if (Main.rand.Next(2) == 0)
					{
						target.AddBuff(Mod.Find<ModBuff>("ArmorCrunch").Type, 240, false);
					}
					else
					{
						target.AddBuff(Mod.Find<ModBuff>("ArmorCrunch").Type, 120, false);
					}
				}
			}
			if (perforatorLore)
			{
				target.AddBuff(BuffID.Ichor, 450);
			}
			if (hiveMindLore)
			{
				target.AddBuff(BuffID.CursedInferno, 450);
			}
			if (holyWrath)
			{
				target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 600, false);
			}
			else if (providenceLore)
			{
				target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 450, false);
			}
		}
		#endregion

		#region ModifyHitNPC
		public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Item, consider using ModifyHitNPC instead */
		{
			#region MultiplierBoosts
			bool crit = modifiers.CritDamage.Base > 1;
			double damageMult = 1.0;
			if (silvaMelee && Main.rand.Next(4) == 0 && item.CountsAsClass(DamageClass.Melee))
			{
				damageMult += 4.0;
			}
			if (enraged && !Config.BossRushXerocCurse)
			{
				damageMult += 1.25;
			}
			if (CalamityWorldPreTrailer.revenge && Config.AdrenalineAndRage)
			{
				bool DHorHoD = (draedonsHeart || heartOfDarkness);
				if (rageMode && adrenalineMode)
				{
					if (item.CountsAsClass(DamageClass.Melee))
					{
						damageMult += (CalamityWorldPreTrailer.death ? (DHorHoD ? 8.9 : 8.0) : (DHorHoD ? 2.3 : 2.0));
					}
				}
				else if (rageMode)
				{
					if (item.CountsAsClass(DamageClass.Melee))
					{
						double rageDamageBoost = 0.0 +
							(rageBoostOne ? (CalamityWorldPreTrailer.death ? 0.6 : 0.15) : 0.0) + //3.6 or 1.65
							(rageBoostTwo ? (CalamityWorldPreTrailer.death ? 0.6 : 0.15) : 0.0) + //4.2 or 1.8
							(rageBoostThree ? (CalamityWorldPreTrailer.death ? 0.6 : 0.15) : 0.0); //4.8 or 1.95
						double rageDamage = (CalamityWorldPreTrailer.death ? (DHorHoD ? 2.3 : 2.0) : (DHorHoD ? 0.65 : 0.5)) + rageDamageBoost;
						damageMult += rageDamage;
					}
				}
				else if (adrenalineMode)
				{
					if (item.CountsAsClass(DamageClass.Melee))
					{
						double damageMultAdr = (CalamityWorldPreTrailer.death ? 6.0 : 1.5) * (double)adrenalineDmgMult;
						damageMult += damageMultAdr;
					}
				}
			}
			modifiers.FinalDamage *= (float)damageMult;
			#endregion

			if (yharonLore)
				modifiers.FinalDamage *= 0.75f;

			if ((target.damage > 5 || target.boss) && Player.whoAmI == Main.myPlayer && !target.SpawnedFromStatue)
			{
				if (item.type == Mod.Find<ModItem>("Ataraxia").Type)
				{
					ataraxiaDamageBoostCancelTimer = 1200;
					if (ataraxiaDamageBoostCooldown <= 0)
					{
						ataraxiaDamageBoostCooldown = 90;
						if (ataraxiaDamageBoost < 0.3f)
							ataraxiaDamageBoost += 0.01f;
					}
				}
				if (item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
				{
					if (ataxiaGeyser)
					{
						if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("ChaosGeyser").Type] < 3)
						{
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ChaosGeyser").Type, (int)((double)item.damage * 0.15), 2f, Player.whoAmI, 0f, 0f);
						}
					}
					if (unstablePrism && crit)
					{
						for (int num252 = 0; num252 < 3; num252++)
						{
							Vector2 value15 = new Vector2((float)Main.rand.Next(-50, 51), (float)Main.rand.Next(-50, 51));
							while (value15.X == 0f && value15.Y == 0f)
							{
								value15 = new Vector2((float)Main.rand.Next(-50, 51), (float)Main.rand.Next(-50, 51));
							}
							value15.Normalize();
							value15 *= (float)Main.rand.Next(30, 61) * 0.1f;
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, value15.X, value15.Y, Mod.Find<ModProjectile>("UnstableSpark").Type, (int)((double)item.damage * 0.15), 0f, Player.whoAmI, 0f, 0f);
						}
					}
				}
				if (astralStarRain && crit && astralStarRainCooldown <= 0)
				{
					astralStarRainCooldown = 60;
					for (int n = 0; n < 3; n++)
					{
						float x = target.position.X + (float)Main.rand.Next(-400, 400);
						float y = target.position.Y - (float)Main.rand.Next(500, 800);
						Vector2 vector = new Vector2(x, y);
						float num13 = target.position.X + (float)(target.width / 2) - vector.X;
						float num14 = target.position.Y + (float)(target.height / 2) - vector.Y;
						num13 += (float)Main.rand.Next(-100, 101);
						float num15 = 25f;
						int projectileType = Main.rand.Next(3);
						if (projectileType == 0)
						{
							projectileType = Mod.Find<ModProjectile>("AstralStar").Type;
						}
						else if (projectileType == 1)
						{
							projectileType = 92;
						}
						else
						{
							projectileType = 12;
						}
						float num16 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
						num16 = num15 / num16;
						num13 *= num16;
						num14 *= num16;
						int num17 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), x, y, num13, num14, projectileType, 75, 5f, Player.whoAmI, 0f, 0f);
					}
				}
				if (bloodflareMelee && item.CountsAsClass(DamageClass.Melee))
				{
					if (bloodflareMeleeHits < 15 && bloodflareFrenzyTimer <= 0 && bloodflareFrenzyCooldown <= 0)
					{
						bloodflareMeleeHits++;
					}
					if (Player.whoAmI == Main.myPlayer)
					{
						int healAmount = (Main.rand.Next(3) + 1);
						Player.statLife += healAmount;
						Player.HealEffect(healAmount);
					}
				}
				if (Config.ProficiencyEnabled)
				{
					if (gainLevelCooldown <= 0)
					{
						gainLevelCooldown = 120;
						if (item.CountsAsClass(DamageClass.Melee) && meleeLevel <= 12500)
						{
							if (!Main.hardMode && meleeLevel >= 1500)
							{
								gainLevelCooldown = 1200; //20 seconds
							}
							if (!NPC.downedMoonlord && meleeLevel >= 5500)
							{
								gainLevelCooldown = 2400; //40 seconds
							}
							meleeLevel++;
							if (fasterMeleeLevel && meleeLevel % 100 != 0 && Main.rand.Next(10) == 0) //add only to non-multiples of 100
								meleeLevel++;
							shootFireworksLevelUpMelee = true;
							if (Main.netMode == 1)
							{
								LevelPacket(false, 0);
							}
						}
					}
				}
				if (CalamityWorldPreTrailer.revenge && Config.AdrenalineAndRage)
				{
					if (item.CountsAsClass(DamageClass.Melee))
					{
						int stressGain = (int)((double)modifiers.SourceDamage.Base * 0.1);
						int stressMaxGain = 10;
						if (stressGain < 1)
						{
							stressGain = 1;
						}
						if (stressGain > stressMaxGain)
						{
							stressGain = stressMaxGain;
						}
						stress += stressGain;
						if (stress >= stressMax)
						{
							stress = stressMax;
						}
					}
				}
			}
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Projectile, consider using ModifyHitNPC instead */
		{
			bool crit = modifiers.CritDamage.Base > 1;
			bool isTrueMelee = proj.GetGlobalProjectile<CalamityGlobalProjectile>().trueMelee;
			bool isSummon = proj.minion || proj.sentry || CalamityModClassicPreTrailer.projectileMinionList.Contains(proj.type);
			bool hasClassType = proj.CountsAsClass(DamageClass.Melee) || proj.CountsAsClass(DamageClass.Ranged) || proj.CountsAsClass(DamageClass.Magic) || isSummon || proj.GetGlobalProjectile<CalamityGlobalProjectile>().rogue;

			if (proj.GetGlobalProjectile<CalamityGlobalProjectile>().rogue)
			{
				crit = (Main.rand.Next(1, 101) < CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit);
			}

			#region MultiplierBoosts
			double damageMult = 1.0;
			if (isSummon)
			{
				if ((Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Summon) &&
					!Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Melee) &&
					!Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Ranged) &&
					!Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Magic) &&
					!Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Throwing)) ||
					Player.inventory[Player.selectedItem].hammer > 0 ||
					Player.inventory[Player.selectedItem].pick > 0 ||
					Player.inventory[Player.selectedItem].axe > 0)
				{
					damageMult += 0.1;
				}
			}
			if (screwdriver)
			{
				if (proj.penetrate > 1 || proj.penetrate == -1)
					damageMult += 0.1;
			}
			if (sPower)
			{
				if (isSummon)
					damageMult += 0.1;
			}
			if (providenceLore && hasClassType)
			{
				damageMult += 0.05;
			}
			if (silvaMelee && Main.rand.Next(4) == 0 && isTrueMelee)
			{
				damageMult += 4.0;
			}
			if (enraged && !Config.BossRushXerocCurse)
			{
				damageMult += 1.25;
			}
			if (auricSet)
			{
				if (silvaThrowing && proj.GetGlobalProjectile<CalamityGlobalProjectile>().rogue &&
					crit && Player.statLife > (int)((double)Player.statLifeMax2 * 0.5))
				{
					damageMult += 0.25;
				}
				if (silvaMelee && proj.CountsAsClass(DamageClass.Melee))
				{
					double multiplier = (double)Player.statLife / (double)Player.statLifeMax2;
					damageMult += multiplier * 0.2;
				}
			}
			if (godSlayerRanged && crit && proj.CountsAsClass(DamageClass.Ranged))
			{
				int randomChance = 100 - (int)Player.GetCritChance(DamageClass.Ranged); //100 min to 15 max with cap

				if (randomChance < 15)
					randomChance = 15;
				if (Main.rand.Next(randomChance) == 0)
					damageMult += 1.0;
			}
			if (silvaCountdown > 0 && hasSilvaEffect && silvaRanged && proj.CountsAsClass(DamageClass.Ranged))
			{
				damageMult += 0.4;
			}
			if (silvaCountdown <= 0 && hasSilvaEffect && silvaThrowing && proj.GetGlobalProjectile<CalamityGlobalProjectile>().rogue)
			{
				damageMult += 0.1;
			}
			if (silvaCountdown <= 0 && hasSilvaEffect && silvaMage && proj.CountsAsClass(DamageClass.Magic))
			{
				damageMult += 0.1;
			}
			if (silvaCountdown <= 0 && hasSilvaEffect && silvaSummon && isSummon)
			{
				damageMult += 0.1;
			}
			if (proj.type == Mod.Find<ModProjectile>("FrostsparkBullet").Type)
			{
				if (target.buffImmune[Mod.Find<ModBuff>("GlacialState").Type])
					damageMult += 0.2;
			}
			if (CalamityWorldPreTrailer.revenge)
			{
				bool DHorHoD = (draedonsHeart || heartOfDarkness);
				if (rageMode && adrenalineMode)
				{
					if (hasClassType)
					{
						damageMult += (CalamityWorldPreTrailer.death ? (DHorHoD ? 8.9 : 8.0) : (DHorHoD ? 2.3 : 2.0));
					}
				}
				else if (rageMode)
				{
					if (hasClassType)
					{
						double rageDamageBoost = 0.0 +
							(rageBoostOne ? (CalamityWorldPreTrailer.death ? 0.6 : 0.15) : 0.0) + //3.6 or 1.65
							(rageBoostTwo ? (CalamityWorldPreTrailer.death ? 0.6 : 0.15) : 0.0) + //4.2 or 1.8
							(rageBoostThree ? (CalamityWorldPreTrailer.death ? 0.6 : 0.15) : 0.0); //4.8 or 1.95
						double rageDamage = (CalamityWorldPreTrailer.death ? (DHorHoD ? 2.3 : 2.0) : (DHorHoD ? 0.65 : 0.5)) + rageDamageBoost;
						damageMult += rageDamage;
					}
				}
				else if (adrenalineMode)
				{
					if (hasClassType)
					{
						double damageMultAdr = (CalamityWorldPreTrailer.death ? 6.0 : 1.5) * (double)adrenalineDmgMult;
						damageMult += damageMultAdr;
					}
				}
			}
			modifiers.SourceDamage *= (float)damageMult;
			#endregion

			#region AdditiveBoosts
			if (theBee && !isSummon)
			{
				modifiers.FinalDamage += theBeeDamage;
			}
			if (proj.type == Mod.Find<ModProjectile>("AcidBullet").Type)
			{
				int defenseAdd = (int)((double)target.defense * 0.1); //100 defense * 0.1 = 10
				modifiers.FinalDamage += defenseAdd;
			}
			if (uberBees && (proj.type == ProjectileID.GiantBee || proj.type == ProjectileID.Bee || proj.type == ProjectileID.Wasp || proj.type == Mod.Find<ModProjectile>("PlagueBee").Type))
			{
				if (Player.inventory[Player.selectedItem].type == Mod.Find<ModItem>("TheSwarmer").Type || Player.inventory[Player.selectedItem].type == Mod.Find<ModItem>("PlagueKeeper").Type ||
					Player.inventory[Player.selectedItem].type == Mod.Find<ModItem>("Plaguenade").Type)
				{
					modifiers.FinalDamage += Main.rand.Next(10, 21);
				}
				else
				{
					modifiers.FinalDamage += Main.rand.Next(70, 101);
					proj.penetrate = 1;
				}
			}
			#endregion

			#region MultiplicativeReductions
			if (isSummon)
			{
				if (!Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Summon) &&
					(Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Melee) ||
					Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Ranged) ||
					Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Magic) ||
					Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Throwing)) &&
					Player.inventory[Player.selectedItem].hammer == 0 &&
					Player.inventory[Player.selectedItem].pick == 0 &&
					Player.inventory[Player.selectedItem].axe == 0)
				{
					modifiers.FinalDamage *= 0.6f;
				}
			}
			if (proj.CountsAsClass(DamageClass.Ranged))
			{
				switch (proj.type)
				{
					case ProjectileID.CrystalShard:
						modifiers.FinalDamage *= 0.6f;
						break;
					case ProjectileID.ChlorophyteBullet:
						modifiers.FinalDamage *= 0.8f;
						break;
					case ProjectileID.HallowStar:
						modifiers.FinalDamage *= 0.7f;
						break;
				}
				if (proj.type == Mod.Find<ModProjectile>("VeriumBullet").Type)
					modifiers.FinalDamage *= 0.8f;
			}
			if (yharonLore)
				modifiers.FinalDamage *= 0.75f;
			#endregion

			if (tarraMage && crit && proj.CountsAsClass(DamageClass.Magic))
			{
				tarraCrits++;
			}
			if (tarraThrowing && tarraThrowingCritTimer <= 0 && tarraThrowingCrits < 25 && crit &&
				proj.GetGlobalProjectile<CalamityGlobalProjectile>().rogue)
			{
				tarraThrowingCrits++;
			}

			if ((target.damage > 5 || target.boss) && Player.whoAmI == Main.myPlayer && !target.SpawnedFromStatue)
			{
				if (theBeeDamage > 0 && (proj.CountsAsClass(DamageClass.Melee) || proj.CountsAsClass(DamageClass.Ranged) || proj.CountsAsClass(DamageClass.Magic) || proj.GetGlobalProjectile<CalamityGlobalProjectile>().rogue))
				{
					SoundEngine.PlaySound(SoundID.Item110, proj.position);
				}
				if (unstablePrism && crit)
				{
					for (int num252 = 0; num252 < 3; num252++)
					{
						Vector2 value15 = new Vector2((float)Main.rand.Next(-50, 51), (float)Main.rand.Next(-50, 51));
						while (value15.X == 0f && value15.Y == 0f)
						{
							value15 = new Vector2((float)Main.rand.Next(-50, 51), (float)Main.rand.Next(-50, 51));
						}
						value15.Normalize();
						value15 *= (float)Main.rand.Next(30, 61) * 0.1f;
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), proj.oldPosition.X + (float)(proj.width / 2), proj.oldPosition.Y + (float)(proj.height / 2), value15.X, value15.Y, Mod.Find<ModProjectile>("UnstableSpark").Type, (int)((double)modifiers.SourceDamage.Base * 0.15), 0f, Player.whoAmI, 0f, 0f);
					}
				}
				if (astralStarRain && crit && astralStarRainCooldown <= 0)
				{
					astralStarRainCooldown = 60;
					for (int n = 0; n < 3; n++)
					{
						float x = target.position.X + (float)Main.rand.Next(-400, 400);
						float y = target.position.Y - (float)Main.rand.Next(500, 800);
						Vector2 vector = new Vector2(x, y);
						float num13 = target.position.X + (float)(target.width / 2) - vector.X;
						float num14 = target.position.Y + (float)(target.height / 2) - vector.Y;
						num13 += (float)Main.rand.Next(-100, 101);
						float num15 = 25f;
						int projectileType = Main.rand.Next(3);
						if (projectileType == 0)
						{
							projectileType = Mod.Find<ModProjectile>("AstralStar").Type;
						}
						else if (projectileType == 1)
						{
							projectileType = 92;
						}
						else
						{
							projectileType = 12;
						}
						float num16 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
						num16 = num15 / num16;
						num13 *= num16;
						num14 *= num16;
						int num17 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), x, y, num13, num14, projectileType, 75, 5f, Player.whoAmI, 0f, 0f);
					}
				}
				if (tarraRanged && crit && proj.CountsAsClass(DamageClass.Ranged))
				{
					int num251 = Main.rand.Next(2, 4);
					for (int num252 = 0; num252 < num251; num252++)
					{
						Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
						while (value15.X == 0f && value15.Y == 0f)
						{
							value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
						}
						value15.Normalize();
						value15 *= (float)Main.rand.Next(70, 101) * 0.1f;
						int FUCKYOU = Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.position.X + (float)(target.width / 2), target.position.Y + (float)(target.height / 2),
							value15.X, value15.Y, 206, (int)((double)modifiers.SourceDamage.Base * 0.25), 0f, Player.whoAmI, 0f, 0f);
						Main.projectile[FUCKYOU].netUpdate = true;
					}
				}
				if (bloodflareThrowing && proj.GetGlobalProjectile<CalamityGlobalProjectile>().rogue && crit && Main.rand.Next(2) == 0)
				{
					if (target.canGhostHeal)
					{
						float num11 = 0.03f;
						num11 -= (float)proj.numHits * 0.015f;
						if (num11 < 0f)
						{
							num11 = 0f;
						}
						float num12 = (float)proj.damage * num11;
						if (num12 < 0f)
						{
							num12 = 0f;
						}
						if (Player.lifeSteal > 0f)
						{
							Player.statLife += 1;
							Player.HealEffect(1);
							Player.lifeSteal -= num12 * 2f;
						}
					}
				}
				if (bloodflareMage && bloodflareMageCooldown <= 0 && crit && proj.CountsAsClass(DamageClass.Magic))
				{
					bloodflareMageCooldown = 120;
					for (int i = 0; i < 3; i++)
					{
						Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
						while (value15.X == 0f && value15.Y == 0f)
						{
							value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
						}
						value15.Normalize();
						value15 *= (float)Main.rand.Next(70, 101) * 0.1f;
						int fire = Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.position.X + (float)(target.width / 2), target.position.Y + (float)(target.height / 2),
							value15.X, value15.Y, 15, (int)((double)modifiers.SourceDamage.Base * 0.5), 0f, Player.whoAmI, 0f, 0f);
						Main.projectile[fire].netUpdate = true;
					}
				}
				if (bloodflareMelee && isTrueMelee)
				{
					if (bloodflareMeleeHits < 15 && bloodflareFrenzyTimer <= 0 && bloodflareFrenzyCooldown <= 0)
					{
						bloodflareMeleeHits++;
					}
					if (Player.whoAmI == Main.myPlayer)
					{
						int healAmount = (Main.rand.Next(3) + 1);
						Player.statLife += healAmount;
						Player.HealEffect(healAmount);
					}
				}
				if (proj.type == Mod.Find<ModProjectile>("AtaraxiaMain").Type || proj.type == Mod.Find<ModProjectile>("AtaraxiaHoming").Type ||
					proj.type == Mod.Find<ModProjectile>("AtaraxiaSplit").Type || proj.type == Mod.Find<ModProjectile>("AtaraxiaSide").Type)
				{
					ataraxiaDamageBoostCancelTimer = 1200;
					if (proj.type == Mod.Find<ModProjectile>("AtaraxiaMain").Type)
					{
						if (ataraxiaDamageBoost < 0.3f)
							ataraxiaDamageBoost += 0.0025f;
					}
				}
				if (Config.ProficiencyEnabled)
				{
					if (gainLevelCooldown <= 0) //max is 12501 to avoid setting off fireworks forever
					{
						gainLevelCooldown = 120; //2 seconds
						if (proj.CountsAsClass(DamageClass.Melee) && meleeLevel <= 12500)
						{
							if (!Main.hardMode && meleeLevel >= 1500)
							{
								gainLevelCooldown = 1200; //20 seconds
							}
							if (!NPC.downedMoonlord && meleeLevel >= 5500)
							{
								gainLevelCooldown = 2400; //40 seconds
							}
							meleeLevel++;
							if (fasterMeleeLevel && meleeLevel % 100 != 0 && Main.rand.Next(10) == 0) //add only to non-multiples of 100
								meleeLevel++;
							shootFireworksLevelUpMelee = true;
							if (Main.netMode == 1)
							{
								LevelPacket(false, 0);
							}
						}
						else if (proj.CountsAsClass(DamageClass.Ranged) && rangedLevel <= 12500)
						{
							if (!Main.hardMode && rangedLevel >= 1500)
							{
								gainLevelCooldown = 1200; //20 seconds
							}
							if (!NPC.downedMoonlord && rangedLevel >= 5500)
							{
								gainLevelCooldown = 2400; //40 seconds
							}
							rangedLevel++;
							if (fasterRangedLevel && rangedLevel % 100 != 0 && Main.rand.Next(10) == 0) //add only to non-multiples of 100
								rangedLevel++;
							shootFireworksLevelUpRanged = true;
							if (Main.netMode == 1)
							{
								LevelPacket(false, 1);
							}
						}
						else if (proj.CountsAsClass(DamageClass.Magic) && magicLevel <= 12500)
						{
							if (!Main.hardMode && magicLevel >= 1500)
							{
								gainLevelCooldown = 1200; //20 seconds
							}
							if (!NPC.downedMoonlord && magicLevel >= 5500)
							{
								gainLevelCooldown = 2400; //40 seconds
							}
							magicLevel++;
							if (fasterMagicLevel && magicLevel % 100 != 0 && Main.rand.Next(10) == 0) //add only to non-multiples of 100
								magicLevel++;
							shootFireworksLevelUpMagic = true;
							if (Main.netMode == 1)
							{
								LevelPacket(false, 2);
							}
						}
						else if (isSummon && summonLevel <= 12500)
						{
							if (!Main.hardMode && summonLevel >= 1500)
							{
								gainLevelCooldown = 1200; //20 seconds
							}
							if (!NPC.downedMoonlord && summonLevel >= 5500)
							{
								gainLevelCooldown = 2400; //40 seconds
							}
							summonLevel++;
							if (fasterSummonLevel && summonLevel % 100 != 0 && Main.rand.Next(10) == 0) //add only to non-multiples of 100
								summonLevel++;
							shootFireworksLevelUpSummon = true;
							if (Main.netMode == 1)
							{
								LevelPacket(false, 3);
							}
						}
						else if (proj.GetGlobalProjectile<CalamityGlobalProjectile>().rogue && rogueLevel <= 12500)
						{
							if (!Main.hardMode && rogueLevel >= 1500)
							{
								gainLevelCooldown = 1200; //20 seconds
							}
							if (!NPC.downedMoonlord && rogueLevel >= 5500)
							{
								gainLevelCooldown = 2400; //40 seconds
							}
							rogueLevel++;
							if (fasterRogueLevel && rogueLevel % 100 != 0 && Main.rand.Next(10) == 0) //add only to non-multiples of 100
								rogueLevel++;
							shootFireworksLevelUpRogue = true;
							if (Main.netMode == 1)
							{
								LevelPacket(false, 4);
							}
						}
					}
				}
				if (raiderTalisman && raiderStack < 250 && proj.GetGlobalProjectile<CalamityGlobalProjectile>().rogue && crit)
				{
					raiderStack++;
				}
				if (CalamityWorldPreTrailer.revenge && Config.AdrenalineAndRage)
				{
					if (isTrueMelee)
					{
						int stressGain = (int)((double)target.damage * 0.1);
						int stressMaxGain = 10;
						if (stressGain < 1)
						{
							stressGain = 1;
						}
						if (stressGain > stressMaxGain)
						{
							stressGain = stressMaxGain;
						}
						stress += stressGain;
						if (stress >= stressMax)
						{
							stress = stressMax;
						}
					}
				}
			}
		}
		#endregion

		#region ModifyHitByNPC
		public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
		{
			if (triumph)
			{
				double HPMultiplier = 0.25 * (1.0 - ((double)npc.life / (double)npc.lifeMax));
				int damageReduction = (int)((double)npc.damage * HPMultiplier);
				npc.damage -= damageReduction;
			}
			if (aSparkRare)
			{
				if (npc.type == NPCID.BlueJellyfish || npc.type == NPCID.PinkJellyfish || npc.type == NPCID.GreenJellyfish ||
					npc.type == NPCID.FungoFish || npc.type == NPCID.BloodJelly || npc.type == NPCID.AngryNimbus || npc.type == NPCID.GigaZapper ||
					npc.type == NPCID.MartianTurret || npc.type == Mod.Find<ModNPC>("StormlionCharger").Type)
					npc.damage /= 2;
			}
			if (fleshTotem && fleshTotemCooldown <= 0)
			{
				fleshTotemCooldown = 1200; //20 seconds
				npc.damage /= 2;
			}
			if (tarraDefense && tarraMelee)
			{
				npc.damage /= 2;
			}
			if (bloodflareMelee && bloodflareFrenzyTimer > 0)
			{
				npc.damage /= 2;
			}
			if (silvaMelee && silvaCountdown <= 0 && hasSilvaEffect)
			{
				npc.damage = (int)((double)npc.damage * 0.8);
			}
			if (aBulwarkRare)
			{
				aBulwarkRareMeleeBoostTimer += 3 * npc.damage;
				if (aBulwarkRareMeleeBoostTimer > 900)
					aBulwarkRareMeleeBoostTimer = 900;
			}
			if (Player.whoAmI == Main.myPlayer && gainRageCooldown <= 0)
			{
				if (CalamityWorldPreTrailer.revenge && Config.AdrenalineAndRage && !npc.SpawnedFromStatue)
				{
					gainRageCooldown = 60;
					int stressGain = npc.damage * (profanedRage ? 3 : 2);
					int stressMaxGain = 2500;
					if (stressGain < 1)
					{
						stressGain = 1;
					}
					if (stressGain > stressMaxGain)
					{
						stressGain = stressMaxGain;
					}
					stress += stressGain;
					if (stress >= stressMax)
					{
						stress = stressMax;
					}
				}
			}
		}
		#endregion

		#region ModifyHitByProj
		public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
		{
			if (projRefRare)
			{
				if (proj.type == projTypeJustHitBy)
					proj.damage = (int)((double)proj.damage * 0.85);
			}
			if (aSparkRare)
			{
				if (proj.type == ProjectileID.MartianTurretBolt || proj.type == ProjectileID.GigaZapperSpear || proj.type == ProjectileID.CultistBossLightningOrbArc ||
					proj.type == ProjectileID.BulletSnowman || proj.type == ProjectileID.BulletDeadeye || proj.type == ProjectileID.SniperBullet)
					proj.damage /= 2;
			}
			if (proj.type == ProjectileID.Nail)
			{
				proj.damage = (int)((double)proj.damage * 0.75);
			}
			if (beeResist)
			{
				if (CalamityModClassicPreTrailer.beeProjectileList.Contains(proj.type))
					proj.damage = (int)((double)proj.damage * 0.75);
			}
			if (Main.hardMode && Main.expertMode && !CalamityWorldPreTrailer.spawnedHardBoss && proj.active && !proj.friendly && proj.hostile && proj.damage > 0)
			{
				if (CalamityModClassicPreTrailer.hardModeNerfList.Contains(proj.type))
					proj.damage = (int)((double)proj.damage * 0.75);
			}
			if (CalamityWorldPreTrailer.revenge)
			{
				if (CalamityModClassicPreTrailer.revengeanceProjectileBuffList.Contains(proj.type))
					proj.damage = (int)((double)proj.damage * 1.25);
			}
			if (Player.whoAmI == Main.myPlayer && gainRageCooldown <= 0)
			{
				if (CalamityWorldPreTrailer.revenge && Config.AdrenalineAndRage && !CalamityModClassicPreTrailer.trapProjectileList.Contains(proj.type))
				{
					gainRageCooldown = 60;
					int stressGain = proj.damage * (profanedRage ? 3 : 2);
					int stressMaxGain = 2500;
					if (stressGain < 1)
					{
						stressGain = 1;
					}
					if (stressGain > stressMaxGain)
					{
						stressGain = stressMaxGain;
					}
					stress += stressGain;
					if (stress >= stressMax)
					{
						stress = stressMax;
					}
				}
			}
		}
		#endregion

		#region OnHit
		public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				if (npc.type == NPCID.SkeletronPrime || npc.type == NPCID.PrimeVice || npc.type == NPCID.PrimeSaw)
				{
					Player.AddBuff(BuffID.Bleeding, 300);
				}
				else if (npc.type == NPCID.Spazmatism && npc.ai[0] != 1f && npc.ai[0] != 2f && npc.ai[0] != 0f)
				{
					Player.AddBuff(BuffID.Bleeding, 300);
				}
				else if (npc.type == NPCID.Plantera && npc.life < npc.lifeMax / 2)
				{
					Player.AddBuff(BuffID.Poisoned, 180);
					Player.AddBuff(BuffID.Bleeding, 300);
				}
				else if (npc.type == NPCID.PlanterasTentacle)
				{
					Player.AddBuff(BuffID.Poisoned, 90);
					Player.AddBuff(BuffID.Bleeding, 150);
				}
				else if (npc.type == NPCID.Golem || npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight ||
					npc.type == NPCID.GolemHead || npc.type == NPCID.GolemHeadFree)
				{
					Player.AddBuff(BuffID.BrokenArmor, 180);
				}
			}
		}

		public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge && proj.hostile)
			{
				if (proj.type == ProjectileID.FrostBeam && !Player.frozen && !gState)
				{
					Player.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 120);
				}
				else if (proj.type == ProjectileID.DeathLaser)
				{
					Player.AddBuff(BuffID.OnFire, 180);
				}
				else if (proj.type == ProjectileID.Skull)
				{
					Player.AddBuff(BuffID.Weak, 120);
				}
				else if (proj.type == ProjectileID.ThornBall)
				{
					Player.AddBuff(BuffID.Poisoned, 180);
				}
				else if (proj.type == ProjectileID.CultistBossIceMist)
				{
					Player.AddBuff(BuffID.Frozen, 60);
					Player.AddBuff(BuffID.Chilled, 120);
					Player.AddBuff(BuffID.Frostburn, 240);
				}
				else if (proj.type == ProjectileID.CultistBossLightningOrbArc)
				{
					Player.AddBuff(BuffID.Electrified, 120);
				}
			}
			if (projRef && proj.active && !proj.friendly && proj.hostile && proj.damage > 0 && Main.rand.Next(20) == 0)
			{
				Player.statLife += proj.damage;
				Player.HealEffect(proj.damage);
				proj.hostile = false;
				proj.friendly = true;
				proj.velocity.X = -proj.velocity.X;
				proj.velocity.Y = -proj.velocity.Y;
			}
			if (projRefRare && proj.active && !proj.friendly && proj.hostile && proj.damage > 0 && Main.rand.Next(2) == 0)
			{
				proj.hostile = false;
				proj.friendly = true;
				proj.velocity.X = -proj.velocity.X * 2f;
				proj.velocity.Y = -proj.velocity.Y * 2f;
				proj.damage *= 10;
				projRefRareLifeRegenCounter = 120;
				projTypeJustHitBy = proj.type;
			}
			if (aSparkRare && proj.active && !proj.friendly && proj.hostile && proj.damage > 0)
			{
				if (proj.type == ProjectileID.BulletSnowman || proj.type == ProjectileID.BulletDeadeye || proj.type == ProjectileID.SniperBullet)
				{
					proj.hostile = false;
					proj.friendly = true;
					proj.velocity.X = -proj.velocity.X;
					proj.velocity.Y = -proj.velocity.Y;
					proj.damage *= 8;
				}
			}
			if (daedalusReflect && proj.active && !proj.friendly && proj.hostile && proj.damage > 0 && Main.rand.Next(3) == 0)
			{
				int healAmt = proj.damage / 5;
				Player.statLife += healAmt;
				Player.HealEffect(healAmt);
				proj.hostile = false;
				proj.friendly = true;
				proj.velocity.X = -proj.velocity.X;
				proj.velocity.Y = -proj.velocity.Y;
			}
		}
		#endregion

		#region Fishing
		public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
		{
			int power = attempt.playerFishingConditions.BaitPower + attempt.playerFishingConditions.PolePower;
			Point point = Player.Center.ToTileCoordinates();
			bool water = !attempt.inHoney && !attempt.inLava;
			bool abyssPosX = false;
			if (CalamityWorldPreTrailer.abyssSide)
			{
				if (point.X < 380)
				{
					abyssPosX = true;
				}
			}
			else
			{
				if (point.X > Main.maxTilesX - 380)
				{
					abyssPosX = true;
				}
			}
			if (itemDrop == ItemID.OldShoe || itemDrop == ItemID.FishingSeaweed || itemDrop == ItemID.TinCan)
			{
				if (abyssPosX && water && power < 40)
				{
					itemDrop = Mod.Find<ModItem>("PlantyMush").Type;
				}
				return;
			}
			/*if (abyssPosX && liquidType == 0 && (bait.type == ItemID.GoldWorm || bait.type == ItemID.GoldGrasshopper || bait.type == ItemID.GoldButterfly) && power > 150)
            {
                if (Main.netMode != 1)
                {
                    CalamityGlobalNPC.OldDukeSpawn(player.whoAmI, mod.NPCType("OldDuke"));
                }
                else
                {
                    NetMessage.SendData(61, -1, -1, null, player.whoAmI, (float)mod.NPCType("OldDuke"), 0f, 0f, 0, 0, 0);
                }
                switch (Main.rand.Next(4))
                {
                    case 0: itemDrop = mod.ItemType("IronBoots"); break; //movement acc
                    case 1: itemDrop = mod.ItemType("DepthCharm"); break; //regen acc
                    case 2: itemDrop = mod.ItemType("AnechoicPlating"); break; //defense acc
                    case 3: itemDrop = mod.ItemType("StrangeOrb"); break; //light pet
                }
                return;
            }*/
			if (power >= 20)
			{
				if (power >= 40)
				{
					if (abyssPosX && water && Main.rand.Next(15) == 0 && power < 80)
					{
						itemDrop = Mod.Find<ModItem>("PlantyMush").Type;
					}
					if (power >= 60)
					{
						if (Player.FindBuffIndex(BuffID.Gills) > -1 && NPC.downedPlantBoss && water && Main.rand.Next(25) == 0 && power < 160)
						{
							itemDrop = Mod.Find<ModItem>("Floodtide").Type;
						}
						if (abyssPosX && water && Main.rand.Next(25) == 0 && power < 160)
						{
							itemDrop = Mod.Find<ModItem>("AlluringBait").Type;
						}
						if (power >= 80)
						{
							if (abyssPosX && Main.hardMode && water && Main.rand.Next(15) == 0 && power < 210)
							{
								switch (Main.rand.Next(4))
								{
									case 0: itemDrop = Mod.Find<ModItem>("IronBoots").Type; break; //movement acc
									case 1: itemDrop = Mod.Find<ModItem>("DepthCharm").Type; break; //regen acc
									case 2: itemDrop = Mod.Find<ModItem>("AnechoicPlating").Type; break; //defense acc
									case 3: itemDrop = Mod.Find<ModItem>("StrangeOrb").Type; break; //light pet
								}
							}
							if (power >= 110)
							{
								if (abyssPosX && water && Main.rand.Next(25) == 0 && power < 240)
								{
									itemDrop = Mod.Find<ModItem>("AbyssalAmulet").Type;
								}
							}
						}
					}
				}
			}
		}
		#endregion

		#region FrameEffects
		public override void FrameEffects()
		{
			if ((snowmanPower || snowmanForce) && !snowmanHide)
			{
				Player.legs = EquipLoader.GetEquipSlot(Mod, "Popo", EquipType.Legs);
				Player.body = EquipLoader.GetEquipSlot(Mod, "Popo", EquipType.Body);
				Player.head = EquipLoader.GetEquipSlot(Mod, snowmanNoseless ? "PopoNoseless" : "Popo", EquipType.Head);
				Player.face = -1;
			}
			else if ((abyssalDivingSuitPower || abyssalDivingSuitForce) && !abyssalDivingSuitHide)
			{
				Player.legs = EquipLoader.GetEquipSlot(Mod, "AbyssalDivingSuit", EquipType.Legs);
				Player.body = EquipLoader.GetEquipSlot(Mod, "AbyssalDivingSuit", EquipType.Body);
				Player.head = EquipLoader.GetEquipSlot(Mod, "AbyssalDivingSuit", EquipType.Head);
			}
			else if ((sirenBoobsPower || sirenBoobsForce) && !sirenBoobsHide)
			{
				Player.legs = EquipLoader.GetEquipSlot(Mod, "SirensHeart", EquipType.Legs);
				Player.body = EquipLoader.GetEquipSlot(Mod, "SirensHeart", EquipType.Body);
				Player.head = EquipLoader.GetEquipSlot(Mod, "SirensHeart", EquipType.Head);
			}
			else if ((sirenBoobsAltPower || sirenBoobsAltForce) && !sirenBoobsAltHide)
			{
				Player.legs = EquipLoader.GetEquipSlot(Mod, "SirensHeartAlt", EquipType.Legs);
				Player.body = EquipLoader.GetEquipSlot(Mod, "SirensHeartAlt", EquipType.Body);
				Player.head = EquipLoader.GetEquipSlot(Mod, "SirensHeartAlt", EquipType.Head);
			}

			if (NOU)
				NOULOL();

			if (CalamityWorldPreTrailer.defiled)
				Defiled();

			if (weakPetrification)
				WeakPetrification();

			if (CalamityWorldPreTrailer.bossRushActive)
				BossRushMovementChanges();
		}
		#endregion

		#region NOULOL
		private void NOULOL()
		{
			Player.ResetEffects();
			Player.head = -1;
			Player.body = -1;
			Player.legs = -1;
			Player.handon = -1;
			Player.handoff = -1;
			Player.back = -1;
			Player.front = -1;
			Player.shoe = -1;
			Player.waist = -1;
			Player.shield = -1;
			Player.neck = -1;
			Player.face = -1;
			Player.balloon = -1;
			NOU = true;
		}

		private void WeakPetrification()
		{
			Player.GetJumpState(ExtraJump.CloudInABottle).Disable(); /* tModPorter Suggestion: Call Enable() if setting this to true, otherwise call Disable(). */
			Player.GetJumpState(ExtraJump.SandstormInABottle).Disable(); /* tModPorter Suggestion: Call Enable() if setting this to true, otherwise call Disable(). */
			Player.GetJumpState(ExtraJump.BlizzardInABottle).Disable(); /* tModPorter Suggestion: Call Enable() if setting this to true, otherwise call Disable(). */
			Player.rocketBoots = 0;
			Player.jumpBoost = false;
			Player.slowFall = false;
			Player.gravControl = false;
			Player.gravControl2 = false;
			Player.jumpSpeedBoost = 0f;
			Player.wingTimeMax = (int)((double)Player.wingTimeMax * 0.5);
			Player.balloon = -1;
			weakPetrification = true;
		}

		private void Defiled()
		{
			Player.wingTimeMax = 0;
		}

		private void BossRushMovementChanges()
		{
			switch (CalamityWorldPreTrailer.bossRushStage)
			{
				case 0:
				case 1:
				case 2:
				case 3:
				case 7:
				case 13:
				case 14:
				case 17:
				case 19:
				case 22:
					Player.wingTimeMax = 0;
					break;
				default:
					break;
			}
		}
		#endregion

		#region HurtMethods
		public override void ModifyHurt(ref Player.HurtModifiers modifiers)
		{
			if (CalamityWorldPreTrailer.armageddon || SCalLore || (CalamityWorldPreTrailer.bossRushActive && bossRushImmunityFrameCurseTimer > 0))
			{
				if (areThereAnyDamnBosses || SCalLore || (CalamityWorldPreTrailer.bossRushActive && bossRushImmunityFrameCurseTimer > 0))
				{
					if (SCalLore)
					{
						string key = "Go to hell.";
						Color messageColor = Color.Orange;
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
					}
					KillPlayer();
				}
			}

			if (lol || (invincible && Player.inventory[Player.selectedItem].type != Mod.Find<ModItem>("ColdheartIcicle").Type))
			{
			}
			if (godSlayerReflect && Main.rand.Next(50) == 0)
			{
			}

			if ((abyssalDivingSuitPower || abyssalDivingSuitForce) && !abyssalDivingSuitHide)
			{
				modifiers.DisableSound();
				SoundEngine.PlaySound(SoundID.NPCHit4, Player.Center); //metal hit noise
			}
			else if (((sirenBoobsPower || sirenBoobsForce) && !sirenBoobsHide) || ((sirenBoobsAltPower || sirenBoobsAltForce) && !sirenBoobsAltHide))
			{
				modifiers.DisableSound();
				SoundEngine.PlaySound(SoundID.FemaleHit, Player.Center); //female hit noise
			}

			#region MultiplierBoosts
			double damageMult = 1.0 +
				(dArtifact ? 0.25 : 0.0) +
				((Player.beetleDefense && Player.beetleOrbs > 0) ? (0.05 * (double)Player.beetleOrbs) : 0.0) +
				(enraged ? 0.25 : 0.0) +
				((CalamityWorldPreTrailer.defiled && Main.rand.Next(4) == 0) ? 0.5 : 0.0) +
				((bloodPact && Main.rand.Next(4) == 0) ? 1.5 : 0.0);

			if (CalamityWorldPreTrailer.revenge)
			{
				if (Player.chaosState)
					damageMult += 0.5;
				if (CalamityWorldPreTrailer.death)
					damageMult += 0.25;
				if (Player.ichor)
					damageMult += 0.25;
				else if (Player.onFire2)
					damageMult += 0.2;
			}

			if (CalamityWorldPreTrailer.bossRushActive)
			{
				switch (CalamityWorldPreTrailer.bossRushStage)
				{
					case 0:
						damageMult += 4.0; //Tier 1 Queen Bee values adjusted for a median of 250 damage
						break;
					case 1:
						damageMult += 3.0; //BoC
						break;
					case 2:
						damageMult += 3.0; //King Slime
						break;
					case 3:
						damageMult += 5.0; //EoC
						break;
					case 4:
						damageMult += 1.5; //Prime
						break;
					case 5:
						damageMult += 1.5; //Golem
						break;
					case 6:
						damageMult += 0.75; //Guardians
						break;
					case 7:
						damageMult += 4.0; //EoW
						break;
					case 8:
						damageMult += 1.0; //Tier 2 Astrageldon values adjusted for a median of 300 damage
						break;
					case 9:
						damageMult += 2.0; //Destroyer
						break;
					case 10:
						damageMult += 2.0; //Twins
						break;
					case 11:
						//Birb
						break;
					case 12:
						damageMult += 3.0; //WoF
						break;
					case 13:
						damageMult += 3.5; //Hive Mind
						break;
					case 14:
						damageMult += 3.0; //Skeletron
						break;
					case 15:
						damageMult += 0.25; //Storm Weaver
						break;
					case 16:
						damageMult += 2.0; //Tier 3 Aquatic Scourge values adjusted for a median of 350 damage
						break;
					case 17:
						damageMult += 5.0; //Desert Scourge
						break;
					case 18:
						damageMult += 2.5; //Cultist
						break;
					case 19:
						damageMult += 3.5; //Crabulon
						break;
					case 20:
						damageMult += 2.5; //Plantera
						break;
					case 21:
						damageMult += 1.0; //Void
						break;
					case 22:
						damageMult += 4.0; //Perfs
						break;
					case 23:
						damageMult += 3.0; //Cryogen
						break;
					case 24:
						damageMult += 3.0; //Tier 4 Brimstone Elemental values adjusted for a median of 400 damage
						break;
					case 25:
						damageMult += 1.0; //Signus
						break;
					case 26:
						damageMult += 1.5; //Ravager
						break;
					case 27:
						damageMult += 1.0; //Fishron
						break;
					case 28:
						damageMult += 1.5; //Moon Lord
						break;
					case 29:
						damageMult += 1.75; //Astrum Deus
						break;
					case 30:
						damageMult += 1.0; //Polter
						break;
					case 31:
						damageMult += 1.5; //Plaguebringer
						break;
					case 32:
						damageMult += 3.0; //Tier 5 Calamitas values adjusted for a median of 450 to 500 damage
						break;
					case 33:
						damageMult += 2.75; //Levi and Siren
						break;
					case 34:
						damageMult += 4.25; //Slime God
						break;
					case 35:
						damageMult += 1.5; //Providence
						break;
					case 36:
						//SCal
						break;
					case 37:
						damageMult += 0.1; //Yharon
						break;
					case 38:
						damageMult += 1.0; //DoG
						break;
				}
			}
			modifiers.FinalDamage *= (float)damageMult;
			#endregion

			if (CalamityWorldPreTrailer.revenge)
			{
				double defenseMult = Main.hardMode ? 0.75 : 0.5;
				double newDamage = (double)modifiers.SourceDamage.Base - ((double)Player.statDefense * defenseMult);
				double newDamageLimit = 5.0 + (Main.hardMode ? 5.0 : 0.0) + (NPC.downedPlantBoss ? 5.0 : 0.0) + (NPC.downedMoonlord ? 5.0 : 0.0); //5, 10, 15, 20
				if (newDamage < newDamageLimit)
				{
					newDamage = newDamageLimit;
				}
				modifiers.FinalDamage.Base = (int)newDamage;
			}

			#region MultiplicativeReductions
			if (trinketOfChiBuff)
			{
				modifiers.FinalDamage *= 0.85f;
			}
			if (purpleCandle)
			{
				modifiers.FinalDamage -= Player.statDefense * 0.05f;
			}
			if (abyssalDivingSuitPlates)
			{
				modifiers.FinalDamage *= 0.85f;
			}
			if (sirenIce)
			{
				modifiers.FinalDamage *= 0.85f;
			}
			if (CalamityWorldPreTrailer.revenge)
			{
				if (!CalamityWorldPreTrailer.downedBossAny)
					modifiers.FinalDamage *= 0.8f;
			}
			if (Player.mount.Active && (Player.mount.Type == Mod.Find<ModMount>("AngryDog").Type || Player.mount.Type == Mod.Find<ModMount>("OnyxExcavator").Type) && Math.Abs(Player.velocity.X) > Player.mount.RunSpeed / 2f)
			{
				modifiers.FinalDamage *= 0.9f;
			}
			#endregion

			if ((godSlayerDamage && modifiers.SourceDamage.Base <= 80) || modifiers.SourceDamage.Base < 1)
			{
				modifiers.SourceDamage.Base = 1f;
			}

			#region HealingEffects
			if (revivifyTimer > 0)
			{
				int healAmt = (int)modifiers.SourceDamage.Base / 20;
				Player.statLife += healAmt;
				Player.HealEffect(healAmt);
			}
			if (daedalusAbsorb && Main.rand.Next(10) == 0)
			{
				int healAmt = (int)modifiers.SourceDamage.Base / 2;
				Player.statLife += healAmt;
				Player.HealEffect(healAmt);
			}
			if (absorber)
			{
				int healAmt = (int)modifiers.SourceDamage.Base / 20;
				Player.statLife += healAmt;
				Player.HealEffect(healAmt);
			}
			#endregion
		}

		public override void OnHurt(Player.HurtInfo info)
		{
			if(info.PvP)
			{
				if (omegaBlueChestplate)
					Player.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 240);

				if (eGauntlet)
				{
					Player.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 120, false);
					Player.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120, false);
					Player.AddBuff(Mod.Find<ModBuff>("Plague").Type, 120, false);
					Player.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 120, false);
					if (Main.rand.Next(5) == 0)
					{
						Player.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 120, false);
					}
					Player.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 120, false);
					Player.AddBuff(BuffID.Poisoned, 120, false);
					Player.AddBuff(BuffID.OnFire, 120, false);
					Player.AddBuff(BuffID.CursedInferno, 120, false);
					Player.AddBuff(BuffID.Frostburn, 120, false);
					Player.AddBuff(BuffID.Ichor, 120, false);
					Player.AddBuff(BuffID.Venom, 120, false);
				}
				if (aWeapon)
				{
					if (Main.rand.Next(4) == 0)
					{
						Player.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 360, false);
					}
					else if (Main.rand.Next(2) == 0)
					{
						Player.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 240, false);
					}
					else
					{
						Player.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 120, false);
					}
				}
				if (abyssalAmulet)
				{
					if (Main.rand.Next(4) == 0)
					{
						Player.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 360, false);
					}
					else if (Main.rand.Next(2) == 0)
					{
						Player.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 240, false);
					}
					else
					{
						Player.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 120, false);
					}
				}
				if (cryogenSoul || frostFlare)
				{
					if (Main.rand.Next(4) == 0)
					{
						Player.AddBuff(44, 360, false);
					}
					else if (Main.rand.Next(2) == 0)
					{
						Player.AddBuff(44, 240, false);
					}
					else
					{
						Player.AddBuff(44, 120, false);
					}
				}
				if (yInsignia)
				{
					if (Main.rand.Next(4) == 0)
					{
						Player.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 360, false);
					}
					else if (Main.rand.Next(2) == 0)
					{
						Player.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 240, false);
					}
					else
					{
						Player.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120, false);
					}
				}
				if (ataxiaFire)
				{
					if (Main.rand.Next(4) == 0)
					{
						Player.AddBuff(BuffID.OnFire, 720, false);
					}
					else if (Main.rand.Next(2) == 0)
					{
						Player.AddBuff(BuffID.OnFire, 480, false);
					}
					else
					{
						Player.AddBuff(BuffID.OnFire, 240, false);
					}
				}
				if (alchFlask)
				{
					if (Main.rand.Next(4) == 0)
					{
						Player.AddBuff(Mod.Find<ModBuff>("Plague").Type, 360, false);
					}
					else if (Main.rand.Next(2) == 0)
					{
						Player.AddBuff(Mod.Find<ModBuff>("Plague").Type, 240, false);
					}
					else
					{
						Player.AddBuff(Mod.Find<ModBuff>("Plague").Type, 120, false);
					}
				}
				if (holyWrath)
				{
					Player.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 600, false);
				} 
			}
			modStealth = 1f;
			ataraxiaDamageBoost = 0f;
			if (Player.whoAmI == Main.myPlayer)
			{
				if (rageMode)
				{
					stress = 0;
					if (Player.FindBuffIndex(Mod.Find<ModBuff>("RageMode").Type) > -1) { Player.ClearBuff(Mod.Find<ModBuff>("RageMode").Type); }
				}
				if (amidiasBlessing)
				{
					Player.ClearBuff(Mod.Find<ModBuff>("AmidiasBlessing").Type);
					SoundEngine.PlaySound(SoundID.Item96, Player.position);
				}
				if (!adrenalineMode && adrenaline != adrenalineMax)
				{
					adrenaline = 0;
				}
				if ((gShell || fabledTortoise) && !Player.panic)
				{
					Player.AddBuff(Mod.Find<ModBuff>("ShellBoost").Type, 300);
				}
				if (abyssalDivingSuitPlates && info.Damage > 50)
				{
					abyssalDivingSuitPlateHits++;
					if (abyssalDivingSuitPlateHits >= 3)
					{
						SoundEngine.PlaySound(SoundID.NPCDeath14, Player.position);
						Player.AddBuff(Mod.Find<ModBuff>("AbyssalDivingSuitPlatesBroken").Type, 10830);
						for (int num621 = 0; num621 < 20; num621++)
						{
							int num622 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 31, 0f, 0f, 100, default(Color), 2f);
							Main.dust[num622].velocity *= 3f;
							if (Main.rand.Next(2) == 0)
							{
								Main.dust[num622].scale = 0.5f;
								Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
							}
						}
						for (int num623 = 0; num623 < 35; num623++)
						{
							int num624 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 6, 0f, 0f, 100, default(Color), 3f);
							Main.dust[num624].noGravity = true;
							Main.dust[num624].velocity *= 5f;
							num624 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 6, 0f, 0f, 100, default(Color), 2f);
							Main.dust[num624].velocity *= 2f;
						}
						for (int num625 = 0; num625 < 3; num625++)
						{
							float scaleFactor10 = 0.33f;
							if (num625 == 1)
							{
								scaleFactor10 = 0.66f;
							}
							if (num625 == 2)
							{
								scaleFactor10 = 1f;
							}
							int num626 = Gore.NewGore(Entity.GetSource_FromThis(null), new Vector2(Player.position.X + (float)(Player.width / 2) - 24f, Player.position.Y + (float)(Player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
							Main.gore[num626].velocity *= scaleFactor10;
							Gore expr_13AB6_cp_0 = Main.gore[num626];
							expr_13AB6_cp_0.velocity.X = expr_13AB6_cp_0.velocity.X + 1f;
							Gore expr_13AD6_cp_0 = Main.gore[num626];
							expr_13AD6_cp_0.velocity.Y = expr_13AD6_cp_0.velocity.Y + 1f;
							num626 = Gore.NewGore(Entity.GetSource_FromThis(null), new Vector2(Player.position.X + (float)(Player.width / 2) - 24f, Player.position.Y + (float)(Player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
							Main.gore[num626].velocity *= scaleFactor10;
							Gore expr_13B79_cp_0 = Main.gore[num626];
							expr_13B79_cp_0.velocity.X = expr_13B79_cp_0.velocity.X - 1f;
							Gore expr_13B99_cp_0 = Main.gore[num626];
							expr_13B99_cp_0.velocity.Y = expr_13B99_cp_0.velocity.Y + 1f;
							num626 = Gore.NewGore(Entity.GetSource_FromThis(null), new Vector2(Player.position.X + (float)(Player.width / 2) - 24f, Player.position.Y + (float)(Player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
							Main.gore[num626].velocity *= scaleFactor10;
							Gore expr_13C3C_cp_0 = Main.gore[num626];
							expr_13C3C_cp_0.velocity.X = expr_13C3C_cp_0.velocity.X + 1f;
							Gore expr_13C5C_cp_0 = Main.gore[num626];
							expr_13C5C_cp_0.velocity.Y = expr_13C5C_cp_0.velocity.Y - 1f;
							num626 = Gore.NewGore(Entity.GetSource_FromThis(null), new Vector2(Player.position.X + (float)(Player.width / 2) - 24f, Player.position.Y + (float)(Player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
							Main.gore[num626].velocity *= scaleFactor10;
							Gore expr_13CFF_cp_0 = Main.gore[num626];
							expr_13CFF_cp_0.velocity.X = expr_13CFF_cp_0.velocity.X - 1f;
							Gore expr_13D1F_cp_0 = Main.gore[num626];
							expr_13D1F_cp_0.velocity.Y = expr_13D1F_cp_0.velocity.Y - 1f;
						}
					}
				}
				if (sirenIce)
				{
					SoundEngine.PlaySound(SoundID.NPCDeath7, Player.position);
					Player.AddBuff(Mod.Find<ModBuff>("IceShieldBrokenBuff").Type, 1800);
					for (int num621 = 0; num621 < 10; num621++)
					{
						int num622 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 67, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num622].velocity *= 3f;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num622].scale = 0.5f;
							Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}
					for (int num623 = 0; num623 < 15; num623++)
					{
						int num624 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 67, 0f, 0f, 100, default(Color), 3f);
						Main.dust[num624].noGravity = true;
						Main.dust[num624].velocity *= 5f;
						num624 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 67, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num624].velocity *= 2f;
					}
				}
				if (tarraMelee)
				{
					if (Main.rand.Next(4) == 0)
					{
						Player.AddBuff(Mod.Find<ModBuff>("TarraLifeRegen").Type, Main.rand.Next(90, 180));
					}
				}
				else if (xerocSet)
				{
					Player.AddBuff(Mod.Find<ModBuff>("XerocRage").Type, 240);
					Player.AddBuff(Mod.Find<ModBuff>("XerocWrath").Type, 240);
				}
				else if (reaverBlast)
				{
					Player.AddBuff(Mod.Find<ModBuff>("ReaverRage").Type, 180);
				}
				if (fBarrier || ((sirenBoobs || sirenBoobsAlt) && NPC.downedBoss3))
				{
					SoundEngine.PlaySound(SoundID.Item27, Player.position);
					for (int m = 0; m < 200; m++)
					{
						if (Main.npc[m].active && !Main.npc[m].friendly)
						{
							float distance = (Main.npc[m].Center - Player.Center).Length();
							float num10 = (float)Main.rand.Next(200 + (int)info.Damage / 2, 301 + (int)info.Damage * 2);
							if (num10 > 500f)
							{
								num10 = 500f + (num10 - 500f) * 0.75f;
							}
							if (num10 > 700f)
							{
								num10 = 700f + (num10 - 700f) * 0.5f;
							}
							if (num10 > 900f)
							{
								num10 = 900f + (num10 - 900f) * 0.25f;
							}
							if (distance < num10)
							{
								float num11 = (float)Main.rand.Next(90 + (int)info.Damage / 3, 240 + (int)info.Damage / 2);
								Main.npc[m].AddBuff(Mod.Find<ModBuff>("GlacialState").Type, (int)num11, false);
							}
						}
					}
				}
				if (aBrain)
				{
					for (int m = 0; m < 200; m++)
					{
						if (Main.npc[m].active && !Main.npc[m].friendly)
						{
							float arg_67A_0 = (Main.npc[m].Center - Player.Center).Length();
							float num10 = (float)Main.rand.Next(200 + (int)info.Damage / 2, 301 + (int)info.Damage * 2);
							if (num10 > 500f)
							{
								num10 = 500f + (num10 - 500f) * 0.75f;
							}
							if (num10 > 700f)
							{
								num10 = 700f + (num10 - 700f) * 0.5f;
							}
							if (num10 > 900f)
							{
								num10 = 900f + (num10 - 900f) * 0.25f;
							}
							if (arg_67A_0 < num10)
							{
								float num11 = (float)Main.rand.Next(90 + (int)info.Damage / 3, 300 + (int)info.Damage / 2);
								Main.npc[m].AddBuff(31, (int)num11, false);
							}
						}
					}
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X + (float)Main.rand.Next(-40, 40), Player.Center.Y - (float)Main.rand.Next(20, 60), Player.velocity.X * 0.3f, Player.velocity.Y * 0.3f, 565, 0, 0f, Player.whoAmI, 0f, 0f);
				}
			}
			if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("Drataliornus").Type] != 0)
			{
				for (int i = 0; i < 1000; i++)
				{
					if (Main.projectile[i].active && Main.projectile[i].type == Mod.Find<ModProjectile>("Drataliornus").Type && Main.projectile[i].owner == Player.whoAmI)
					{
						Main.projectile[i].Kill();
						break;
					}
				}
				Player.AddBuff(Mod.Find<ModBuff>("Backfire").Type, 360);
				if (Player.wingTime > Player.wingTimeMax / 2)
					Player.wingTime = Player.wingTimeMax / 2;
			}
		}

		public override void PostHurt(Player.HurtInfo info)
		{
			if (pArtifact)
			{
				Player.AddBuff(Mod.Find<ModBuff>("BurntOut").Type, 300, true);
			}
			if (NPC.AnyNPCs(Mod.Find<ModNPC>("SupremeCalamitas").Type) && CalamityWorldPreTrailer.revenge)
			{
				if (info.Damage < 100)
				{
					KillPlayer();
					string key = "You aren't hurting as much as I'd like...are you cheating?";
					Color messageColor = Color.Orange;
					if (Main.netMode == 0)
					{
						Main.NewText(Language.GetTextValue(key), messageColor);
					}
					else if (Main.netMode == 2)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
					}
				}
			}
			bool hardMode = Main.hardMode;
			if (Player.whoAmI == Main.myPlayer)
			{
				if (cTracers && info.Damage > 200)
				{
					Player.immuneTime += 60;
				}
				if (godSlayerThrowing && info.Damage > 80)
				{
					Player.immuneTime += 30;
				}
				if (statigelSet && info.Damage > 100)
				{
					Player.immuneTime += 30;
				}
				if (dAmulet)
				{
					if (info.Damage == 1.0)
					{
						Player.immuneTime += 10;
					}
					else
					{
						Player.immuneTime += 20;
					}
				}
				if (fabsolVodka)
				{
					if (info.Damage == 1.0)
					{
						Player.immuneTime += 5;
					}
					else
					{
						Player.immuneTime += 10;
					}
				}
				if (CalamityWorldPreTrailer.bossRushActive && Config.BossRushImmunityFrameCurse)
				{
					bossRushImmunityFrameCurseTimer = 300 + Player.immuneTime;
				}
				if (info.Damage > 25)
				{
					if (aeroSet)
					{
						for (int n = 0; n < 4; n++)
						{
							float x = Player.position.X + (float)Main.rand.Next(-400, 400);
							float y = Player.position.Y - (float)Main.rand.Next(500, 800);
							Vector2 vector = new Vector2(x, y);
							float num13 = Player.position.X + (float)(Player.width / 2) - vector.X;
							float num14 = Player.position.Y + (float)(Player.height / 2) - vector.Y;
							num13 += (float)Main.rand.Next(-100, 101);
							int num15 = 20;
							float num16 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
							num16 = (float)num15 / num16;
							num13 *= num16;
							num14 *= num16;
							int num17 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), x, y, num13, num14, Mod.Find<ModProjectile>("StickyFeatherAero").Type, 20, 1f, Player.whoAmI, 0f, 0f);
						}
					}
				}
				if (aBulwark)
				{
					if (aBulwarkRare)
					{
						SoundEngine.PlaySound(SoundID.Item74, Player.position);
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("GodSlayerBlaze").Type, 25, 5f, Player.whoAmI, 0f, 0f);
					}
					int starAmt = aBulwarkRare ? 12 : 5;
					for (int n = 0; n < starAmt; n++)
					{
						float x = Player.position.X + (float)Main.rand.Next(-400, 400);
						float y = Player.position.Y - (float)Main.rand.Next(500, 800);
						Vector2 vector = new Vector2(x, y);
						float num13 = Player.position.X + (float)(Player.width / 2) - vector.X;
						float num14 = Player.position.Y + (float)(Player.height / 2) - vector.Y;
						num13 += (float)Main.rand.Next(-100, 101);
						int num15 = 29;
						float num16 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
						num16 = (float)num15 / num16;
						num13 *= num16;
						num14 *= num16;
						int num17 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), x, y, num13, num14, Mod.Find<ModProjectile>("AstralStar").Type, 320, 5f, Player.whoAmI, 0f, 0f);
					}
				}
				if (dAmulet)
				{
					for (int n = 0; n < 3; n++)
					{
						float x = Player.position.X + (float)Main.rand.Next(-400, 400);
						float y = Player.position.Y - (float)Main.rand.Next(500, 800);
						Vector2 vector = new Vector2(x, y);
						float num13 = Player.position.X + (float)(Player.width / 2) - vector.X;
						float num14 = Player.position.Y + (float)(Player.height / 2) - vector.Y;
						num13 += (float)Main.rand.Next(-100, 101);
						int num15 = 29;
						float num16 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
						num16 = (float)num15 / num16;
						num13 *= num16;
						num14 *= num16;
						int num17 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), x, y, num13, num14, 92, 130, 4f, Player.whoAmI, 0f, 0f);
						Main.projectile[num17].usesLocalNPCImmunity = true;
						Main.projectile[num17].localNPCHitCooldown = 5;
					}
				}
			}
			if (fCarapace)
			{
				if (info.Damage > 0)
				{
					SoundEngine.PlaySound(SoundID.NPCHit45, Player.position);
					float spread = 45f * 0.0174f;
					double startAngle = Math.Atan2(Player.velocity.X, Player.velocity.Y) - spread / 2;
					double deltaAngle = spread / 8f;
					double offsetAngle;
					int i;
					int fDamage = 56;
					if (Player.whoAmI == Main.myPlayer)
					{
						for (i = 0; i < 4; i++)
						{
							float xPos = Main.rand.Next(2) == 0 ? Player.Center.X + 100 : Player.Center.X - 100;
							Vector2 vector2 = new Vector2(xPos, Player.Center.Y + Main.rand.Next(-100, 101));
							offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
							int spore1 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), 590, fDamage, 1.25f, Player.whoAmI, 0f, 0f);
							int spore2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), 590, fDamage, 1.25f, Player.whoAmI, 0f, 0f);
							Main.projectile[spore1].timeLeft = 120;
							Main.projectile[spore2].timeLeft = 120;
						}
					}
				}
			}
			if (aSpark)
			{
				if (info.Damage > 0)
				{
					SoundEngine.PlaySound(SoundID.Item93, Player.position);
					float spread = 45f * 0.0174f;
					double startAngle = Math.Atan2(Player.velocity.X, Player.velocity.Y) - spread / 2;
					double deltaAngle = spread / 8f;
					double offsetAngle;
					int i;
					int sDamage = hardMode ? 36 : 6;
					if (aSparkRare)
						sDamage += hardMode ? 12 : 2;
					if (Player.whoAmI == Main.myPlayer)
					{
						for (i = 0; i < 4; i++)
						{
							offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
							int spark1 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("Spark").Type, sDamage, 1.25f, Player.whoAmI, 0f, 0f);
							int spark2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("Spark").Type, sDamage, 1.25f, Player.whoAmI, 0f, 0f);
							Main.projectile[spark1].timeLeft = 120;
							Main.projectile[spark2].timeLeft = 120;
						}
					}
				}
			}
			if (ataxiaBlaze && Main.rand.Next(5) == 0)
			{
				if (info.Damage > 0)
				{
					SoundEngine.PlaySound(SoundID.Item74, Player.position);
					int eDamage = 100;
					if (Player.whoAmI == Main.myPlayer)
					{
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ChaosBlaze").Type, eDamage, 1f, Player.whoAmI, 0f, 0f);
					}
				}
			}
			else if (daedalusShard)
			{
				if (info.Damage > 0)
				{
					SoundEngine.PlaySound(SoundID.Item27, Player.position);
					float spread = 45f * 0.0174f;
					double startAngle = Math.Atan2(Player.velocity.X, Player.velocity.Y) - spread / 2;
					double deltaAngle = spread / 8f;
					double offsetAngle;
					int i;
					int sDamage = 27;
					if (Player.whoAmI == Main.myPlayer)
					{
						for (i = 0; i < 8; i++)
						{
							float randomSpeed = (float)Main.rand.Next(1, 7);
							float randomSpeed2 = (float)Main.rand.Next(1, 7);
							offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
							int shard = Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f) + randomSpeed, 90, sDamage, 1f, Player.whoAmI, 0f, 0f);
							int shard2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f) + randomSpeed2, 90, sDamage, 1f, Player.whoAmI, 0f, 0f);
						}
					}
				}
			}
			else if (reaverSpore)
			{
				if (info.Damage > 0)
				{
					SoundEngine.PlaySound(SoundID.NPCHit1, Player.position);
					float spread = 45f * 0.0174f;
					double startAngle = Math.Atan2(Player.velocity.X, Player.velocity.Y) - spread / 2;
					double deltaAngle = spread / 8f;
					double offsetAngle;
					int i;
					int rDamage = 58;
					if (Player.whoAmI == Main.myPlayer)
					{
						for (i = 0; i < 4; i++)
						{
							float xPos = Main.rand.Next(2) == 0 ? Player.Center.X + 100 : Player.Center.X - 100;
							Vector2 vector2 = new Vector2(xPos, Player.Center.Y + Main.rand.Next(-100, 101));
							offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), 567, rDamage, 2f, Player.whoAmI, 0f, 0f);
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), 568, rDamage, 2f, Player.whoAmI, 0f, 0f);
						}
					}
				}
			}
			else if (godSlayerDamage)
			{
				if (info.Damage > 80)
				{
					SoundEngine.PlaySound(SoundID.Item73, Player.position);
					float spread = 45f * 0.0174f;
					double startAngle = Math.Atan2(Player.velocity.X, Player.velocity.Y) - spread / 2;
					double deltaAngle = spread / 8f;
					double offsetAngle;
					int i;
					if (Player.whoAmI == Main.myPlayer)
					{
						for (i = 0; i < 4; i++)
						{
							offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("GodKiller").Type, 900, 5f, Player.whoAmI, 0f, 0f);
							Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("GodKiller").Type, 900, 5f, Player.whoAmI, 0f, 0f);
						}
					}
				}
			}
			else if (godSlayerMage)
			{
				if (info.Damage > 0)
				{
					SoundEngine.PlaySound(SoundID.Item74, Player.position);
					if (Player.whoAmI == Main.myPlayer)
					{
						Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("GodSlayerBlaze").Type, (auricSet ? 2400 : 1200), 1f, Player.whoAmI, 0f, 0f);
					}
				}
			}
			else if (dsSetBonus)
			{
				if (Player.whoAmI == Main.myPlayer)
				{
					for (int l = 0; l < 2; l++)
					{
						float x = Player.position.X + (float)Main.rand.Next(-400, 400);
						float y = Player.position.Y - (float)Main.rand.Next(500, 800);
						Vector2 vector = new Vector2(x, y);
						float num15 = Player.position.X + (float)(Player.width / 2) - vector.X;
						float num16 = Player.position.Y + (float)(Player.height / 2) - vector.Y;
						num15 += (float)Main.rand.Next(-100, 101);
						int num17 = 22;
						float num18 = (float)Math.Sqrt((double)(num15 * num15 + num16 * num16));
						num18 = (float)num17 / num18;
						num15 *= num18;
						num16 *= num18;
						int num19 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), x, y, num15, num16, 294, 3000, 7f, Player.whoAmI, 0f, 0f);
						Main.projectile[num19].ai[1] = Player.position.Y;
					}
					for (int l = 0; l < 5; l++)
					{
						float x = Player.position.X + (float)Main.rand.Next(-400, 400);
						float y = Player.position.Y - (float)Main.rand.Next(500, 800);
						Vector2 vector = new Vector2(x, y);
						float num15 = Player.position.X + (float)(Player.width / 2) - vector.X;
						float num16 = Player.position.Y + (float)(Player.height / 2) - vector.Y;
						num15 += (float)Main.rand.Next(-100, 101);
						int num17 = 22;
						float num18 = (float)Math.Sqrt((double)(num15 * num15 + num16 * num16));
						num18 = (float)num17 / num18;
						num15 *= num18;
						num16 *= num18;
						int num19 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), x, y, num15, num16, 45, 5000, 7f, Player.whoAmI, 0f, 0f);
						Main.projectile[num19].ai[1] = Player.position.Y;
					}
				}
			}
		}
		#endregion

		#region KillPlayer
		public void KillPlayer()
		{
			deathCount++;
			if (Player.whoAmI == Main.myPlayer && Main.netMode == 1)
			{
				DeathPacket(false);
			}
			Player.lastDeathPostion = Player.Center;
			Player.lastDeathTime = DateTime.Now;
			Player.showLastDeath = true;
			bool specialDeath = CalamityWorldPreTrailer.ironHeart && areThereAnyDamnBosses;
			bool flag;
			int coinsOwned = (int)Utils.CoinsCount(out flag, Player.inventory, new int[0]);
			if (Main.myPlayer == Player.whoAmI)
			{
				Player.lostCoins = coinsOwned;
				Player.lostCoinString = Main.ValueToCoins(Player.lostCoins);
			}
			if (Main.myPlayer == Player.whoAmI)
			{
				Main.mapFullscreen = false;
			}
			if (Main.myPlayer == Player.whoAmI)
			{
				Player.trashItem.SetDefaults(0, false);
				if (specialDeath)
				{
					Player.difficulty = 2;
					Player.DropItems();
					Player.KillMeForGood();
				}
				else if (Player.difficulty == 0)
				{
					for (int i = 0; i < 59; i++)
					{
						if (Player.inventory[i].stack > 0 && ((Player.inventory[i].type >= 1522 && Player.inventory[i].type <= 1527) || Player.inventory[i].type == 3643))
						{
							int num = Item.NewItem(Entity.GetSource_FromThis(null), (int)Player.position.X, (int)Player.position.Y, Player.width, Player.height, Player.inventory[i].type, 1, false, 0, false, false);
							Main.item[num].netDefaults(Player.inventory[i].netID);
							Main.item[num].Prefix((int)Player.inventory[i].prefix);
							Main.item[num].stack = Player.inventory[i].stack;
							Main.item[num].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
							Main.item[num].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
							Main.item[num].noGrabDelay = 100;
							Main.item[num].favorited = false;
							Main.item[num].newAndShiny = false;
							if (Main.netMode == 1)
							{
								NetMessage.SendData(21, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
							}
							Player.inventory[i].SetDefaults(0, false);
						}
					}
				}
				else if (Player.difficulty == 1)
				{
					Player.DropItems();
				}
				else if (Player.difficulty == 2)
				{
					Player.DropItems();
					Player.KillMeForGood();
				}
			}
			if (specialDeath)
			{
				SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/IronHeartDeath"), Player.position);
			}
			else
			{
				SoundEngine.PlaySound(SoundID.PlayerKilled, Player.position);
			}
			Player.headVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			Player.bodyVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			Player.legVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			Player.headVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * 0);
			Player.bodyVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * 0);
			Player.legVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * 0);
			if (Player.stoned)
			{
				Player.headPosition = Vector2.Zero;
				Player.bodyPosition = Vector2.Zero;
				Player.legPosition = Vector2.Zero;
			}
			for (int j = 0; j < 100; j++)
			{
				Dust.NewDust(Player.position, Player.width, Player.height, (specialDeath ? 91 : 235), (float)(2 * 0), -2f, 0, default(Color), 1f);
			}
			Player.mount.Dismount(Player);
			Player.dead = true;
			Player.respawnTimer = 600;
			if (Main.expertMode)
			{
				Player.respawnTimer = (int)((double)Player.respawnTimer * 1.5);
			}
			Player.immuneAlpha = 0;
			Player.palladiumRegen = false;
			Player.iceBarrier = false;
			Player.crystalLeaf = false;
			PlayerDeathReason damageSource = PlayerDeathReason.ByOther(Player.Male ? 14 : 15);
			NetworkText deathText = damageSource.GetDeathText(Player.name);
			if (Main.netMode == 2)
			{
				ChatHelper.BroadcastChatMessage(deathText, new Color(225, 25, 25), -1);
			}
			else if (Main.netMode == 0)
			{
				Main.NewText(deathText.ToString(), new Color(225, 25, 25));
			}
			if (Main.netMode == 1 && Player.whoAmI == Main.myPlayer)
			{
				NetMessage.SendPlayerDeath(Player.whoAmI, damageSource, (int)1000.0, 0, false, -1, -1);
			}
			if (Player.whoAmI == Main.myPlayer && Player.difficulty == 0)
			{
				Player.DropCoins();
			}
			Player.DropTombstone(coinsOwned, deathText, 0);
			if (Player.whoAmI == Main.myPlayer)
			{
				try
				{
					WorldGen.saveToonWhilePlaying();
				}
				catch
				{
				}
			}
		}
		#endregion

		#region DashStuff
		
		public bool dashInactive;
		public void ModDashMovement()
		{
			if (dashMod == 6 && dashInactive && Player.whoAmI == Main.myPlayer)
			{
				Rectangle rectangle = new Rectangle((int)((double)Player.position.X + (double)Player.velocity.X * 0.5 - 4.0), (int)((double)Player.position.Y + (double)Player.velocity.Y * 0.5 - 4.0), Player.width + 8, Player.height + 8);
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].immune[Player.whoAmI] <= 0)
					{
						NPC nPC = Main.npc[i];
						Rectangle rect = nPC.getRect();
						if (rectangle.Intersects(rect) && (nPC.noTileCollide || Player.CanHit(nPC)))
						{
							float num = 50f * Player.GetDamage(DamageClass.Melee).Base;
							float num2 = 3f;
							bool crit = false;
							if (Player.kbGlove)
							{
								num2 *= 2f;
							}
							if (Player.kbBuff)
							{
								num2 *= 1.5f;
							}
							if (Main.rand.Next(100) < Player.GetCritChance(DamageClass.Melee))
							{
								crit = true;
							}
							int direction = Player.direction;
							if (Player.velocity.X < 0f)
							{
								direction = -1;
							}
							if (Player.velocity.X > 0f)
							{
								direction = 1;
							}
							if (Player.whoAmI == Main.myPlayer)
							{
								Player.ApplyDamageToNPC(nPC, (int)num, num2, direction, crit);
							}
							nPC.immune[Player.whoAmI] = 6;
							nPC.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 300);
							Player.immune = true;
							Player.immuneNoBlink = true;
							Player.immuneTime = 4;
						}
					}
				}
			}
			if (dashMod == 4 && dashInactive && Player.whoAmI == Main.myPlayer)
			{
				Rectangle rectangle = new Rectangle((int)((double)Player.position.X + (double)Player.velocity.X * 0.5 - 4.0), (int)((double)Player.position.Y + (double)Player.velocity.Y * 0.5 - 4.0), Player.width + 8, Player.height + 8);
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].immune[Player.whoAmI] <= 0)
					{
						NPC nPC = Main.npc[i];
						Rectangle rect = nPC.getRect();
						if (rectangle.Intersects(rect) && (nPC.noTileCollide || Player.CanHit(nPC)))
						{
							float num = 1500f * Player.GetDamage(DamageClass.Melee).Base;
							float num2 = 15f;
							bool crit = false;
							if (Player.kbGlove)
							{
								num2 *= 2f;
							}
							if (Player.kbBuff)
							{
								num2 *= 1.5f;
							}
							if (Main.rand.Next(100) < Player.GetCritChance(DamageClass.Melee))
							{
								crit = true;
							}
							int direction = Player.direction;
							if (Player.velocity.X < 0f)
							{
								direction = -1;
							}
							if (Player.velocity.X > 0f)
							{
								direction = 1;
							}
							if (Player.whoAmI == Main.myPlayer)
							{
								Player.ApplyDamageToNPC(nPC, (int)num, num2, direction, crit);
								int num6 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyExplosionSupreme").Type, 1000, 20f, Main.myPlayer, 0f, 0f);
								Main.projectile[num6].Kill();
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyEruption").Type, 780, 5f, Main.myPlayer, 0f, 0f);
							}
							nPC.immune[Player.whoAmI] = 6;
							nPC.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 300);
							Player.immune = true;
							Player.immuneNoBlink = true;
							Player.immuneTime = 4;
						}
					}
				}
			}
			if (dashMod == 3 && dashInactive && Player.whoAmI == Main.myPlayer)
			{
				Rectangle rectangle = new Rectangle((int)((double)Player.position.X + (double)Player.velocity.X * 0.5 - 4.0), (int)((double)Player.position.Y + (double)Player.velocity.Y * 0.5 - 4.0), Player.width + 8, Player.height + 8);
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].immune[Player.whoAmI] <= 0)
					{
						NPC nPC = Main.npc[i];
						Rectangle rect = nPC.getRect();
						if (rectangle.Intersects(rect) && (nPC.noTileCollide || Player.CanHit(nPC)))
						{
							float num = 500f * Player.GetDamage(DamageClass.Melee).Base;
							float num2 = 12f;
							bool crit = false;
							if (Player.kbGlove)
							{
								num2 *= 2f;
							}
							if (Player.kbBuff)
							{
								num2 *= 1.5f;
							}
							if (Main.rand.Next(100) < Player.GetCritChance(DamageClass.Melee))
							{
								crit = true;
							}
							int direction = Player.direction;
							if (Player.velocity.X < 0f)
							{
								direction = -1;
							}
							if (Player.velocity.X > 0f)
							{
								direction = 1;
							}
							if (Player.whoAmI == Main.myPlayer)
							{
								Player.ApplyDamageToNPC(nPC, (int)num, num2, direction, crit);
								int num6 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyExplosionSupreme").Type, 500, 15f, Main.myPlayer, 0f, 0f);
								Main.projectile[num6].Kill();
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyEruption").Type, 380, 5f, Main.myPlayer, 0f, 0f);
							}
							nPC.immune[Player.whoAmI] = 6;
							Player.immune = true;
							Player.immuneNoBlink = true;
							Player.immuneTime = 4;
						}
					}
				}
			}
			if (dashMod == 2 && dashInactive && Player.whoAmI == Main.myPlayer)
			{
				Rectangle rectangle = new Rectangle((int)((double)Player.position.X + (double)Player.velocity.X * 0.5 - 4.0), (int)((double)Player.position.Y + (double)Player.velocity.Y * 0.5 - 4.0), Player.width + 8, Player.height + 8);
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].immune[Player.whoAmI] <= 0)
					{
						NPC nPC = Main.npc[i];
						Rectangle rect = nPC.getRect();
						if (rectangle.Intersects(rect) && (nPC.noTileCollide || Player.CanHit(nPC)))
						{
							float num = 100f * Player.GetDamage(DamageClass.Melee).Base;
							float num2 = 9f;
							bool crit = false;
							if (Player.kbGlove)
							{
								num2 *= 2f;
							}
							if (Player.kbBuff)
							{
								num2 *= 1.5f;
							}
							if (Main.rand.Next(100) < Player.GetCritChance(DamageClass.Melee))
							{
								crit = true;
							}
							int direction = Player.direction;
							if (Player.velocity.X < 0f)
							{
								direction = -1;
							}
							if (Player.velocity.X > 0f)
							{
								direction = 1;
							}
							if (Player.whoAmI == Main.myPlayer)
							{
								Player.ApplyDamageToNPC(nPC, (int)num, num2, direction, crit);
								int num6 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyExplosion").Type, 100, 15f, Main.myPlayer, 0f, 0f);
								Main.projectile[num6].Kill();
							}
							nPC.immune[Player.whoAmI] = 6;
							Player.immune = true;
							Player.immuneNoBlink = true;
							Player.immuneTime = 4;
						}
					}
				}
			}
			if (dashMod == 1 && dashInactive && Player.whoAmI == Main.myPlayer)
			{
				Rectangle rectangle = new Rectangle((int)((double)Player.position.X + (double)Player.velocity.X * 0.5 - 4.0), (int)((double)Player.position.Y + (double)Player.velocity.Y * 0.5 - 4.0), Player.width + 8, Player.height + 8);
				for (int i = 0; i < 200; i++)
				{
					if ((Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && !Main.npc[i].townNPC && Main.npc[i].immune[Player.whoAmI] <= 0 && Main.npc[i].damage > 0))
					{
						NPC nPC = Main.npc[i];
						Rectangle rect = nPC.getRect();
						if (rectangle.Intersects(rect) && (nPC.noTileCollide || Player.CanHit(nPC)))
						{
							OnDodge();
							break;
						}
					}
				}
				for (int i = 0; i < 1000; i++)
				{
					if ((Main.projectile[i].active && !Main.projectile[i].friendly && Main.projectile[i].hostile && Main.projectile[i].damage > 0))
					{
						Projectile proj = Main.projectile[i];
						Rectangle rect = proj.getRect();
						if (rectangle.Intersects(rect))
						{
							OnDodge();
							break;
						}
					}
				}
			}
			if (Player.dashDelay > 0)
			{
				return;
			}
			if (dashInactive)
			{
				float num7 = 12f;
				float num8 = 0.985f;
				float num9 = Math.Max(Player.accRunSpeed, Player.maxRunSpeed);
				float num10 = 0.94f;
				int num11 = 20;
				if (dashMod == 1)
				{
					for (int k = 0; k < 2; k++)
					{
						int num12;
						if (Player.velocity.Y == 0f)
						{
							num12 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + (float)Player.height - 4f), Player.width, 8, 235, 0f, 0f, 100, default(Color), 1.4f);
						}
						else
						{
							num12 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + (float)(Player.height / 2) - 8f), Player.width, 16, 235, 0f, 0f, 100, default(Color), 1.4f);
						}
						Main.dust[num12].velocity *= 0.1f;
						Main.dust[num12].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
				}
				else if (dashMod == 2)
				{
					for (int m = 0; m < 4; m++)
					{
						int num14 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 4f), Player.width, Player.height - 8, 246, 0f, 0f, 100, default(Color), 2.75f);
						Main.dust[num14].velocity *= 0.1f;
						Main.dust[num14].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num14].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
						Main.dust[num14].noGravity = true;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num14].fadeIn = 0.5f;
						}
					}
				}
				else if (dashMod == 3)
				{
					for (int m = 0; m < 12; m++)
					{
						int num14 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 4f), Player.width, Player.height - 8, 244, 0f, 0f, 100, default(Color), 2.75f);
						Main.dust[num14].velocity *= 0.1f;
						Main.dust[num14].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num14].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
						Main.dust[num14].noGravity = true;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num14].fadeIn = 0.5f;
						}
					}
					num7 = 14f; //14
				}
				else if (dashMod == 4)
				{
					for (int m = 0; m < 24; m++)
					{
						int num14 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 4f), Player.width, Player.height - 8, 244, 0f, 0f, 100, default(Color), 2.75f);
						Main.dust[num14].velocity *= 0.1f;
						Main.dust[num14].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num14].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
						Main.dust[num14].noGravity = true;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num14].fadeIn = 0.5f;
						}
					}
					num7 = 16f; //14
				}
				else if (dashMod == 5)
				{
					for (int m = 0; m < 24; m++)
					{
						int num14 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 4f), Player.width, Player.height - 8, 33, 0f, 0f, 100, default(Color), 2.75f);
						Main.dust[num14].velocity *= 0.1f;
						Main.dust[num14].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num14].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
						Main.dust[num14].noGravity = true;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num14].fadeIn = 0.5f;
						}
					}
					num7 = 18f; //14
				}
				else if (dashMod == 6)
				{
					for (int m = 0; m < 24; m++)
					{
						int num14 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 4f), Player.width, Player.height - 8, 67, 0f, 0f, 100, default(Color), 1f);
						Main.dust[num14].velocity *= 0.1f;
						Main.dust[num14].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num14].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
						Main.dust[num14].noGravity = true;
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num14].fadeIn = 0.5f;
						}
					}
					num7 = 12.5f; //14
					}
				if (dashMod > 0)
				{
					Player.vortexStealthActive = false;
					if (Player.velocity.X > num7 || Player.velocity.X < -num7)
					{
						Player.velocity.X = Player.velocity.X * num8;
						return;
					}
					if (Player.velocity.X > num9 || Player.velocity.X < -num9)
					{
						Player.velocity.X = Player.velocity.X * num10;
						return;
					}
					Player.dashDelay = num11;
					dashInactive = false;
					if (Player.velocity.X < 0f)
					{
						Player.velocity.X = -num9;
						return;
					}
					if (Player.velocity.X > 0f)
					{
						Player.velocity.X = num9;
						return;
					}
				}
			}
			else if (dashMod > 0 && !Player.mount.Active)
			{
				if (dashMod == 1)
				{
					int num16 = 0;
					bool flag = false;
					if (dashTimeMod > 0)
					{
						dashTimeMod--;
					}
					if (dashTimeMod < 0)
					{
						dashTimeMod++;
					}
					if (Player.controlRight && Player.releaseRight)
					{
						if (dashTimeMod > 0)
						{
							num16 = 1;
							flag = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = 15;
						}
					}
					else if (Player.controlLeft && Player.releaseLeft)
					{
						if (dashTimeMod < 0)
						{
							num16 = -1;
							flag = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = -15;
						}
					}
					if (flag)
					{
						Player.velocity.X = 14.5f * (float)num16; //eoc dash amount
						Point point = (Player.Center + new Vector2((float)(num16 * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
						Point point2 = (Player.Center + new Vector2((float)(num16 * Player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
						{
							Player.velocity.X = Player.velocity.X / 2f;
						}
						dashInactive = true;
						for (int num17 = 0; num17 < 20; num17++)
						{
							int num18 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 235, 0f, 0f, 100, default(Color), 2f);
							Dust expr_CDB_cp_0 = Main.dust[num18];
							expr_CDB_cp_0.position.X = expr_CDB_cp_0.position.X + (float)Main.rand.Next(-5, 6);
							Dust expr_D02_cp_0 = Main.dust[num18];
							expr_D02_cp_0.position.Y = expr_D02_cp_0.position.Y + (float)Main.rand.Next(-5, 6);
							Main.dust[num18].velocity *= 0.2f;
							Main.dust[num18].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
							Main.dust[num18].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
						}
						return;
					}
				}
				else if (dashMod == 2)
				{
					int num23 = 0;
					bool flag3 = false;
					if (dashTimeMod > 0)
					{
						dashTimeMod--;
					}
					if (dashTimeMod < 0)
					{
						dashTimeMod++;
					}
					if (Player.controlRight && Player.releaseRight)
					{
						if (dashTimeMod > 0)
						{
							num23 = 1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = 15;
						}
					}
					else if (Player.controlLeft && Player.releaseLeft)
					{
						if (dashTimeMod < 0)
						{
							num23 = -1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = -15;
						}
					}
					if (flag3)
					{
						Player.velocity.X = 16.9f * (float)num23; //tabi dash amount
						Point point5 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
						Point point6 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point5.X, point5.Y) || WorldGen.SolidOrSlopedTile(point6.X, point6.Y))
						{
							Player.velocity.X = Player.velocity.X / 2f;
						}
						dashInactive = true;
						for (int num24 = 0; num24 < 20; num24++)
						{
							int num25 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 246, 0f, 0f, 100, default(Color), 3f);
							Dust expr_13AF_cp_0 = Main.dust[num25];
							expr_13AF_cp_0.position.X = expr_13AF_cp_0.position.X + (float)Main.rand.Next(-5, 6);
							Dust expr_13D6_cp_0 = Main.dust[num25];
							expr_13D6_cp_0.position.Y = expr_13D6_cp_0.position.Y + (float)Main.rand.Next(-5, 6);
							Main.dust[num25].velocity *= 0.2f;
							Main.dust[num25].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
							Main.dust[num25].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
							Main.dust[num25].noGravity = true;
							Main.dust[num25].fadeIn = 0.5f;
						}
					}
				}
				else if (dashMod == 3)
				{
					int num23 = 0;
					bool flag3 = false;
					if (dashTimeMod > 0)
					{
						dashTimeMod--;
					}
					if (dashTimeMod < 0)
					{
						dashTimeMod++;
					}
					if (Player.controlRight && Player.releaseRight)
					{
						if (dashTimeMod > 0)
						{
							num23 = 1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = 15;
						}
					}
					else if (Player.controlLeft && Player.releaseLeft)
					{
						if (dashTimeMod < 0)
						{
							num23 = -1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = -15;
						}
					}
					if (flag3)
					{
						Player.velocity.X = 21.9f * (float)num23; //solar dash amount
						Point point5 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
						Point point6 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point5.X, point5.Y) || WorldGen.SolidOrSlopedTile(point6.X, point6.Y))
						{
							Player.velocity.X = Player.velocity.X / 2f;
						}
						dashInactive = true;
						for (int num24 = 0; num24 < 40; num24++)
						{
							int num25 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 244, 0f, 0f, 100, default(Color), 3f);
							Dust expr_13AF_cp_0 = Main.dust[num25];
							expr_13AF_cp_0.position.X = expr_13AF_cp_0.position.X + (float)Main.rand.Next(-5, 6);
							Dust expr_13D6_cp_0 = Main.dust[num25];
							expr_13D6_cp_0.position.Y = expr_13D6_cp_0.position.Y + (float)Main.rand.Next(-5, 6);
							Main.dust[num25].velocity *= 0.2f;
							Main.dust[num25].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
							Main.dust[num25].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
							Main.dust[num25].noGravity = true;
							Main.dust[num25].fadeIn = 0.5f;
						}
					}
				}
				else if (dashMod == 4)
				{
					int num23 = 0;
					bool flag3 = false;
					if (dashTimeMod > 0)
					{
						dashTimeMod--;
					}
					if (dashTimeMod < 0)
					{
						dashTimeMod++;
					}
					if (Player.controlRight && Player.releaseRight)
					{
						if (dashTimeMod > 0)
						{
							num23 = 1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = 15;
						}
					}
					else if (Player.controlLeft && Player.releaseLeft)
					{
						if (dashTimeMod < 0)
						{
							num23 = -1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = -15;
						}
					}
					if (flag3)
					{
						Player.velocity.X = 23.9f * (float)num23; //slighty more powerful solar dash
						Point point5 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
						Point point6 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point5.X, point5.Y) || WorldGen.SolidOrSlopedTile(point6.X, point6.Y))
						{
							Player.velocity.X = Player.velocity.X / 2f;
						}
						dashInactive = true;
						for (int num24 = 0; num24 < 60; num24++)
						{
							int num25 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 244, 0f, 0f, 100, default(Color), 3f);
							Dust expr_13AF_cp_0 = Main.dust[num25];
							expr_13AF_cp_0.position.X = expr_13AF_cp_0.position.X + (float)Main.rand.Next(-5, 6);
							Dust expr_13D6_cp_0 = Main.dust[num25];
							expr_13D6_cp_0.position.Y = expr_13D6_cp_0.position.Y + (float)Main.rand.Next(-5, 6);
							Main.dust[num25].velocity *= 0.2f;
							Main.dust[num25].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
							Main.dust[num25].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
							Main.dust[num25].noGravity = true;
							Main.dust[num25].fadeIn = 0.5f;
						}
					}
				}
				else if (dashMod == 5)
				{
					int num23 = 0;
					bool flag3 = false;
					if (dashTimeMod > 0)
					{
						dashTimeMod--;
					}
					if (dashTimeMod < 0)
					{
						dashTimeMod++;
					}
					if (Player.controlRight && Player.releaseRight)
					{
						if (dashTimeMod > 0)
						{
							num23 = 1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = 15;
						}
					}
					else if (Player.controlLeft && Player.releaseLeft)
					{
						if (dashTimeMod < 0)
						{
							num23 = -1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = -15;
						}
					}
					if (flag3)
					{
						Player.velocity.X = 25.9f * (float)num23;
						Point point5 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
						Point point6 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point5.X, point5.Y) || WorldGen.SolidOrSlopedTile(point6.X, point6.Y))
						{
							Player.velocity.X = Player.velocity.X / 2f;
						}
						dashInactive = true;
						for (int num24 = 0; num24 < 60; num24++)
						{
							int num25 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 33, 0f, 0f, 100, default(Color), 3f);
							Dust expr_13AF_cp_0 = Main.dust[num25];
							expr_13AF_cp_0.position.X = expr_13AF_cp_0.position.X + (float)Main.rand.Next(-5, 6);
							Dust expr_13D6_cp_0 = Main.dust[num25];
							expr_13D6_cp_0.position.Y = expr_13D6_cp_0.position.Y + (float)Main.rand.Next(-5, 6);
							Main.dust[num25].velocity *= 0.2f;
							Main.dust[num25].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
							Main.dust[num25].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
							Main.dust[num25].noGravity = true;
							Main.dust[num25].fadeIn = 0.5f;
						}
					}
				}
				else if (dashMod == 6)
				{
					int num23 = 0;
					bool flag3 = false;
					if (dashTimeMod > 0)
					{
						dashTimeMod--;
					}
					if (dashTimeMod < 0)
					{
						dashTimeMod++;
					}
					if (Player.controlRight && Player.releaseRight)
					{
						if (dashTimeMod > 0)
						{
							num23 = 1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = 15;
						}
					}
					else if (Player.controlLeft && Player.releaseLeft)
					{
						if (dashTimeMod < 0)
						{
							num23 = -1;
							flag3 = true;
							dashTimeMod = 0;
						}
						else
						{
							dashTimeMod = -15;
						}
					}
					if (flag3)
					{
						Player.velocity.X = 15.7f * (float)num23;
						Point point5 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
						Point point6 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point5.X, point5.Y) || WorldGen.SolidOrSlopedTile(point6.X, point6.Y))
						{
							Player.velocity.X = Player.velocity.X / 2f;
						}
						dashInactive = true; 
						for (int num24 = 0; num24 < 60; num24++)
						{
							int num25 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 67, 0f, 0f, 100, default(Color), 1.25f);
							Dust expr_13AF_cp_0 = Main.dust[num25];
							expr_13AF_cp_0.position.X = expr_13AF_cp_0.position.X + (float)Main.rand.Next(-5, 6);
							Dust expr_13D6_cp_0 = Main.dust[num25];
							expr_13D6_cp_0.position.Y = expr_13D6_cp_0.position.Y + (float)Main.rand.Next(-5, 6);
							Main.dust[num25].velocity *= 0.2f;
							Main.dust[num25].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
							Main.dust[num25].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
							Main.dust[num25].noGravity = true;
							Main.dust[num25].fadeIn = 0.5f;
						}
					}
				}
			}
		}

		private void OnDodge()
		{
			if (Player.whoAmI == Main.myPlayer && dodgeScarf && !scarfCooldown)
			{
				Player.AddBuff(Mod.Find<ModBuff>("ScarfMeleeBoost").Type, Main.rand.Next(480, 561));
				Player.AddBuff(Mod.Find<ModBuff>("ScarfCooldown").Type, (Player.chaosState ? 1800 : 900));
				Player.immune = true;
				Player.immuneTime = 60;
				if (Player.longInvince)
				{
					Player.immuneTime += 40;
				}
				for (int k = 0; k < Player.hurtCooldowns.Length; k++)
				{
					Player.hurtCooldowns[k] = Player.immuneTime;
				}
				for (int j = 0; j < 100; j++)
				{
					int num = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 235, 0f, 0f, 100, default(Color), 2f);
					Dust expr_A4_cp_0 = Main.dust[num];
					expr_A4_cp_0.position.X = expr_A4_cp_0.position.X + (float)Main.rand.Next(-20, 21);
					Dust expr_CB_cp_0 = Main.dust[num];
					expr_CB_cp_0.position.Y = expr_CB_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
					Main.dust[num].velocity *= 0.4f;
					Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
						Main.dust[num].noGravity = true;
					}
				}
				if (Player.whoAmI == Main.myPlayer)
				{
					NetMessage.SendData(62, -1, -1, null, Player.whoAmI, 1f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		public void ModHorizontalMovement()
		{
			float num = (Player.accRunSpeed + Player.maxRunSpeed) / 2f;
			if (Player.controlLeft && Player.velocity.X > -Player.accRunSpeed && Player.dashDelay >= 0)
			{
				if (Player.velocity.X < -num && Player.velocity.Y == 0f && !Player.mount.Active)
				{
					int num3 = 0;
					if (Player.gravDir == -1f)
					{
						num3 -= Player.height;
					}
					if (dashMod == 1)
					{
						int num7 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num3), Player.width + 8, 4, 235, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 1.5f);
						Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
						Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
						Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 2)
					{
						int num7 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num3), Player.width + 8, 4, 246, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 2.5f);
						Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
						Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
						Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 3)
					{
						int num7 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num3), Player.width + 8, 4, 244, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 3f);
						Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
						Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
						Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 4)
					{
						int num7 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num3), Player.width + 8, 4, 244, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 3f);
						Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
						Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
						Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 5)
					{
						int num7 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num3), Player.width + 8, 4, 33, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 3f);
						Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
						Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
						Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 6)
					{
						int num7 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num3), Player.width + 8, 4, 67, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 1.25f);
						Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
						Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
						Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
				}
			}
			else if (Player.controlRight && Player.velocity.X < Player.accRunSpeed && Player.dashDelay >= 0)
			{
				if (Player.velocity.X > num && Player.velocity.Y == 0f && !Player.mount.Active)
				{
					int num8 = 0;
					if (Player.gravDir == -1f)
					{
						num8 -= Player.height;
					}
					if (dashMod == 1)
					{
						int num12 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num8), Player.width + 8, 4, 235, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 1.5f);
						Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
						Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 2)
					{
						int num12 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num8), Player.width + 8, 4, 246, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 2.5f);
						Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
						Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 3)
					{
						int num12 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num8), Player.width + 8, 4, 244, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 3f);
						Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
						Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 4)
					{
						int num12 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num8), Player.width + 8, 4, 244, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 3f);
						Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
						Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 5)
					{
						int num12 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num8), Player.width + 8, 4, 33, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 3f);
						Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
						Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 6)
					{
						int num12 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num8), Player.width + 8, 4, 67, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 1.25f);
						Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
						Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
				}
			}
			if (Player.mount.Active && Player.mount.Type == Mod.Find<ModMount>("Fab").Type && Math.Abs(Player.velocity.X) > Player.mount.DashSpeed - Player.mount.RunSpeed / 2f)
			{
				Rectangle rect = Player.getRect();
				if (Player.direction == 1)
				{
					rect.Offset(Player.width - 1, 0);
				}
				rect.Width = 2;
				rect.Inflate(6, 12);
				float damage = 800f * Player.GetDamage(DamageClass.Summon).Multiplicative;
				float knockback = 10f;
				int nPCImmuneTime = 30;
				int playerImmuneTime = 6;
				ModCollideWithNPCs(rect, damage, knockback, nPCImmuneTime, playerImmuneTime);
			}
			if (Player.mount.Active && Player.mount.Type == Mod.Find<ModMount>("AngryDog").Type && Math.Abs(Player.velocity.X) > Player.mount.RunSpeed / 2f)
			{
				Rectangle rect2 = Player.getRect();
				if (Player.direction == 1)
				{
					rect2.Offset(Player.width - 1, 0);
				}
				rect2.Width = 2;
				rect2.Inflate(6, 12);
				float damage2 = 50f * Player.GetDamage(DamageClass.Summon).Multiplicative;
				float knockback2 = 8f;
				int nPCImmuneTime2 = 30;
				int playerImmuneTime2 = 6;
				ModCollideWithNPCs(rect2, damage2, knockback2, nPCImmuneTime2, playerImmuneTime2);
			}
			if (Player.mount.Active && Player.mount.Type == Mod.Find<ModMount>("OnyxExcavator").Type && Math.Abs(Player.velocity.X) > Player.mount.RunSpeed / 2f)
			{
				Rectangle rect2 = Player.getRect();
				if (Player.direction == 1)
				{
					rect2.Offset(Player.width - 1, 0);
				}
				rect2.Width = 2;
				rect2.Inflate(6, 12);
				float damage2 = 25f * Player.GetDamage(DamageClass.Summon).Multiplicative;
				float knockback2 = 5f;
				int nPCImmuneTime2 = 30;
				int playerImmuneTime2 = 6;
				ModCollideWithNPCs(rect2, damage2, knockback2, nPCImmuneTime2, playerImmuneTime2);
			}
		}

		private int ModCollideWithNPCs(Rectangle myRect, float Damage, float Knockback, int NPCImmuneTime, int PlayerImmuneTime)
		{
			int num = 0;
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC.active && !nPC.dontTakeDamage && !nPC.friendly && nPC.immune[Player.whoAmI] == 0)
				{
					Rectangle rect = nPC.getRect();
					if (myRect.Intersects(rect) && (nPC.noTileCollide || Collision.CanHit(Player.position, Player.width, Player.height, nPC.position, nPC.width, nPC.height)))
					{
						int direction = Player.direction;
						if (Player.velocity.X < 0f)
						{
							direction = -1;
						}
						if (Player.velocity.X > 0f)
						{
							direction = 1;
						}
						if (Player.whoAmI == Main.myPlayer)
						{
							Player.ApplyDamageToNPC(nPC, (int)Damage, Knockback, direction, false);
						}
						nPC.immune[Player.whoAmI] = NPCImmuneTime;
						Player.immune = true;
						Player.immuneNoBlink = true;
						Player.immuneTime = PlayerImmuneTime;
						num++;
						break;
					}
				}
			}
			return num;
		}
		#endregion

		#region Drawing
		public static void AddPlayerLayer(List<PlayerDrawLayer> list, PlayerDrawLayer layer, PlayerDrawLayer parent, bool first)
		{
			int insertAt = -1;
			for (int m = 0; m < list.Count; m++)
			{
				PlayerDrawLayer dl = list[m];
				if (dl.Name.Equals(parent.Name)) { insertAt = m; break; }
			}
			if (insertAt == -1) list.Add(layer); else list.Insert(first ? insertAt : insertAt + 1, layer);
		}

		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			bool noRogueStealth = rogueStealth == 0f || Player.townNPCs > 2f;
			if (rogueStealth > 0f && rogueStealthMax > 0f && Player.townNPCs < 3f)
			{
				//A translucent orchid color, the rogue class color
				float colorValue = (rogueStealth / rogueStealthMax) * 0.9f; //0 to 0.9
				r *= 1f - (colorValue * 0.89f); //255 to 50
				g *= 1f - colorValue; //255 to 25
				b *= 1f - (colorValue * 0.89f); //255 to 50
				a *= 1f - colorValue; //255 to 25
				Player.armorEffectDrawOutlines = false;
				Player.armorEffectDrawShadow = false;
				Player.armorEffectDrawShadowSubtle = false;
			}
			if (CalamityWorldPreTrailer.ironHeart && !Main.gameMenu)
			{
				Asset<Texture2D> ironHeart = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/IronHeart");
				TextureAssets.Heart = TextureAssets.Heart2 = ironHeart;
			}
			else
			{
				Asset<Texture2D> heart3 = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/Heart3");
				Asset<Texture2D> heart4 = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/Heart4");
				Asset<Texture2D> heart5 = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/Heart5");
				Asset<Texture2D> heart6 = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/Heart6");
				Asset<Texture2D> heartOriginal = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/HeartOriginal");
				Asset<Texture2D> heartOriginal2 = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/HeartOriginal2");

				int totalFruit =
					(mFruit ? 1 : 0) +
					(bOrange ? 1 : 0) +
					(eBerry ? 1 : 0) +
					(dFruit ? 1 : 0);
				switch (totalFruit)
				{
					default:
						TextureAssets.Heart2 = heartOriginal; TextureAssets.Heart = heartOriginal2;
						break;
					case 4:
						TextureAssets.Heart2 = heart6; TextureAssets.Heart = heartOriginal2;
						break;
					case 3:
						TextureAssets.Heart2 = heart5; TextureAssets.Heart = heartOriginal2;
						break;
					case 2:
						TextureAssets.Heart2 = heart4; TextureAssets.Heart = heartOriginal2;
						break;
					case 1:
						TextureAssets.Heart2 = heart3; TextureAssets.Heart = heartOriginal2;
						break;
				}
			}
			if (revivifyTimer > 0)
			{
				if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 91, Player.velocity.X * 0.2f, Player.velocity.Y * 0.2f, 100, default(Color), 1f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity.Y -= 0.5f;
				}
			}
			if (tRegen)
			{
				if (Main.rand.Next(10) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 107, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 0.75f;
					Main.dust[dust].velocity.Y -= 0.35f;
				}
				if (noRogueStealth)
				{
					r *= 0.025f;
					g *= 0.15f;
					b *= 0.035f;
					fullBright = true;
				}
			}
			if (IBoots)
			{
				if (((double)Math.Abs(Player.velocity.X) > 0.05 || (double)Math.Abs(Player.velocity.Y) > 0.05) && !Player.mount.Active)
				{
					if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
					{
						int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 229, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1f);
						Main.dust[dust].noGravity = true;
						Main.dust[dust].velocity *= 0.5f;
					}
					if (noRogueStealth)
					{
						r *= 0.05f;
						g *= 0.05f;
						b *= 0.05f;
						fullBright = true;
					}
				}
			}
			if (elysianFire)
			{
				if (((double)Math.Abs(Player.velocity.X) > 0.05 || (double)Math.Abs(Player.velocity.Y) > 0.05) && !Player.mount.Active)
				{
					if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
					{
						int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 246, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1f);
						Main.dust[dust].noGravity = true;
						Main.dust[dust].velocity *= 0.5f;
					}
					if (noRogueStealth)
					{
						r *= 0.75f;
						g *= 0.55f;
						b *= 0f;
						fullBright = true;
					}
				}
			}
			if (dsSetBonus)
			{
				if (((double)Math.Abs(Player.velocity.X) > 0.05 || (double)Math.Abs(Player.velocity.Y) > 0.05) && !Player.mount.Active)
				{
					if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
					{
						int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 27, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1.5f);
						Main.dust[dust].noGravity = true;
						Main.dust[dust].velocity *= 0.5f;
					}
					if (noRogueStealth)
					{
						r *= 0.15f;
						g *= 0.025f;
						b *= 0.1f;
						fullBright = true;
					}
				}
			}
			if (auricSet)
			{
				if (((double)Math.Abs(Player.velocity.X) > 0.05 || (double)Math.Abs(Player.velocity.Y) > 0.05) && !Player.mount.Active)
				{
					if (drawInfo.shadow == 0f)
					{
						int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, Main.rand.Next(2) == 0 ? 57 : 244, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1.5f);
						Main.dust[dust].noGravity = true;
						Main.dust[dust].velocity *= 0.5f;
					}
					if (noRogueStealth)
					{
						r *= 0.15f;
						g *= 0.025f;
						b *= 0.1f;
						fullBright = true;
					}
				}
			}
			if (bFlames || aFlames || rageMode)
			{
				if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<BrimstoneFlame>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
				}
				if (noRogueStealth)
				{
					r *= 0.25f;
					g *= 0.01f;
					b *= 0.01f;
					fullBright = true;
				}
			}
			if (adrenalineMode)
			{
				if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 206, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
				}
				if (noRogueStealth)
				{
					r *= 0.01f;
					g *= 0.15f;
					b *= 0.1f;
					fullBright = true;
				}
			}
			if (gsInferno)
			{
				if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 173, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
				}
				if (noRogueStealth)
				{
					r *= 0.25f;
					g *= 0.01f;
					b *= 0.01f;
					fullBright = true;
				}
			}
			if (hFlames || hInferno)
			{
				if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<HolyFlame>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
				}
				if (noRogueStealth)
				{
					r *= 0.25f;
					g *= 0.25f;
					b *= 0.1f;
					fullBright = true;
				}
			}
			if (pFlames)
			{
				if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 89, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.2f;
					Main.dust[dust].velocity.Y -= 0.15f;
				}
				if (noRogueStealth)
				{
					r *= 0.07f;
					g *= 0.15f;
					b *= 0.01f;
					fullBright = true;
				}
			}
			if (gState || cDepth)
			{
				if (noRogueStealth)
				{
					r *= 0f;
					g *= 0.05f;
					b *= 0.3f;
					fullBright = true;
				}
			}
			if (draedonsHeart && !shadeRegen && !cFreeze && (double)Math.Abs(Player.velocity.X) < 0.05 && (double)Math.Abs(Player.velocity.Y) < 0.05 && Player.itemAnimation == 0)
			{
				if (noRogueStealth)
				{
					r *= 0f;
					g *= 0.5f;
					b *= 0f;
					fullBright = true;
				}
			}
			if (bBlood)
			{
				if (Main.rand.Next(6) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 5, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
				}
				if (noRogueStealth)
				{
					r *= 0.15f;
					g *= 0.01f;
					b *= 0.01f;
					fullBright = true;
				}
			}
			if (mushy)
			{
				if (Main.rand.Next(6) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 56, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 2f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 0.5f;
					Main.dust[dust].velocity.Y -= 0.1f;
				}
				if (noRogueStealth)
				{
					r *= 0.15f;
					g *= 0.01f;
					b *= 0.01f;
					fullBright = true;
				}
			}
		}
		#endregion

		/*#region NurseModifications
		public override bool ModifyNurseHeal(NPC nurse, ref int health, ref bool removeDebuffs, ref string chatText)
		{
			if (CalamityWorld.revenge)
			{
				chatText = "Now is not the time!";
				return !areThereAnyDamnBosses;
			}
			return true;
		}

		public override void ModifyNursePrice(NPC nurse, int health, bool removeDebuffs, ref int price)
		{
			if (CalamityWorld.revenge)
			{
				double priceMultiplier = 1.0 + //3 silver base
					(Main.hardMode ? 99.0 : 0.0) + //3 gold
					(NPC.downedMechBossAny ? 200.0 : 0.0) + //9 gold
					(NPC.downedPlantBoss ? 300.0 : 0.0) + //18 gold
					(NPC.downedGolemBoss ? 400.0 : 0.0) + //30 gold
					(NPC.downedFishron ? 500.0 : 0.0) + //45 gold
					(NPC.downedAncientCultist ? 600.0 : 0.0) + //63 gold
					(NPC.downedMoonlord ? 700.0 : 0.0) + //84 gold
					(CalamityWorld.downedProvidence ? 800.0 : 0.0) + //1 platinum 8 gold
					(CalamityWorld.downedDoG ? 900.0 : 0.0) + //1 platinum 35 gold
					(CalamityWorld.downedYharon ? 1000.0 : 0.0); //1 platinum 65 gold
				price = (int)((double)price * priceMultiplier);
			}
		}
		#endregion*/

		#region DamageAndCrit
		public void AllDamageBoost(float boost)
		{
			Player.GetDamage(DamageClass.Melee) += boost;
			Player.GetDamage(DamageClass.Ranged) += boost;
			Player.GetDamage(DamageClass.Magic) += boost;
			Player.GetDamage(DamageClass.Summon) += boost;
			CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += boost;
		}

		public void AllCritBoost(int boost)
		{
			Player.GetCritChance(DamageClass.Melee) += boost;
			Player.GetCritChance(DamageClass.Ranged) += boost;
			Player.GetCritChance(DamageClass.Magic) += boost;
			CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += boost;
		}
		#endregion

		#region PacketStuff
		private void ExactLevelPacket(bool server, int levelType)
		{
			ModPacket packet = Mod.GetPacket(256);
			switch (levelType)
			{
				case 0:
					packet.Write((byte)CalamityModClassicPreTrailerMessageType.ExactMeleeLevelSync);
					packet.Write(Player.whoAmI);
					packet.Write(exactMeleeLevel);
					break;
				case 1:
					packet.Write((byte)CalamityModClassicPreTrailerMessageType.ExactRangedLevelSync);
					packet.Write(Player.whoAmI);
					packet.Write(exactRangedLevel);
					break;
				case 2:
					packet.Write((byte)CalamityModClassicPreTrailerMessageType.ExactMagicLevelSync);
					packet.Write(Player.whoAmI);
					packet.Write(exactMagicLevel);
					break;
				case 3:
					packet.Write((byte)CalamityModClassicPreTrailerMessageType.ExactSummonLevelSync);
					packet.Write(Player.whoAmI);
					packet.Write(exactSummonLevel);
					break;
				case 4:
					packet.Write((byte)CalamityModClassicPreTrailerMessageType.ExactRogueLevelSync);
					packet.Write(Player.whoAmI);
					packet.Write(exactRogueLevel);
					break;
			}
			if (!server)
			{
				packet.Send();
			}
			else
			{
				packet.Send(-1, Player.whoAmI);
			}
		}

		private void LevelPacket(bool server, int levelType)
		{
			ModPacket packet = Mod.GetPacket(256);
			switch (levelType)
			{
				case 0:
					packet.Write((byte)CalamityModClassicPreTrailerMessageType.MeleeLevelSync);
					packet.Write(Player.whoAmI);
					packet.Write(meleeLevel);
					break;
				case 1:
					packet.Write((byte)CalamityModClassicPreTrailerMessageType.RangedLevelSync);
					packet.Write(Player.whoAmI);
					packet.Write(rangedLevel);
					break;
				case 2:
					packet.Write((byte)CalamityModClassicPreTrailerMessageType.MagicLevelSync);
					packet.Write(Player.whoAmI);
					packet.Write(magicLevel);
					break;
				case 3:
					packet.Write((byte)CalamityModClassicPreTrailerMessageType.SummonLevelSync);
					packet.Write(Player.whoAmI);
					packet.Write(summonLevel);
					break;
				case 4:
					packet.Write((byte)CalamityModClassicPreTrailerMessageType.RogueLevelSync);
					packet.Write(Player.whoAmI);
					packet.Write(rogueLevel);
					break;
			}
			if (!server)
			{
				packet.Send();
			}
			else
			{
				packet.Send(-1, Player.whoAmI);
			}
		}

		private void StressPacket(bool server)
		{
			ModPacket packet = Mod.GetPacket(256);
			packet.Write((byte)CalamityModClassicPreTrailerMessageType.StressSync);
			packet.Write(Player.whoAmI);
			packet.Write(stress);
			if (!server)
			{
				packet.Send();
			}
			else
			{
				packet.Send(-1, Player.whoAmI);
			}
		}

		private void AdrenalinePacket(bool server)
		{
			ModPacket packet = Mod.GetPacket(256);
			packet.Write((byte)CalamityModClassicPreTrailerMessageType.AdrenalineSync);
			packet.Write(Player.whoAmI);
			packet.Write(adrenaline);
			if (!server)
			{
				packet.Send();
			}
			else
			{
				packet.Send(-1, Player.whoAmI);
			}
		}

		private void DeathPacket(bool server)
		{
			ModPacket packet = Mod.GetPacket(256);
			packet.Write((byte)CalamityModClassicPreTrailerMessageType.DeathCountSync);
			packet.Write(Player.whoAmI);
			packet.Write(deathCount);
			if (!server)
			{
				packet.Send();
			}
			else
			{
				packet.Send(-1, Player.whoAmI);
			}
		}

		internal void HandleExactLevels(BinaryReader reader, int levelType)
		{
			switch (levelType)
			{
				case 0:
					exactMeleeLevel = reader.ReadInt32();
					break;
				case 1:
					exactRangedLevel = reader.ReadInt32();
					break;
				case 2:
					exactMagicLevel = reader.ReadInt32();
					break;
				case 3:
					exactSummonLevel = reader.ReadInt32();
					break;
				case 4:
					exactRogueLevel = reader.ReadInt32();
					break;
			}
			if (Main.netMode == 2)
			{
				ExactLevelPacket(true, levelType);
			}
		}

		internal void HandleLevels(BinaryReader reader, int levelType)
		{
			switch (levelType)
			{
				case 0:
					meleeLevel = reader.ReadInt32();
					break;
				case 1:
					rangedLevel = reader.ReadInt32();
					break;
				case 2:
					magicLevel = reader.ReadInt32();
					break;
				case 3:
					summonLevel = reader.ReadInt32();
					break;
				case 4:
					rogueLevel = reader.ReadInt32();
					break;
			}
			if (Main.netMode == 2)
			{
				LevelPacket(true, levelType);
			}
		}

		internal void HandleStress(BinaryReader reader)
		{
			stress = reader.ReadInt32();
			if (Main.netMode == 2)
			{
				StressPacket(true);
			}
		}

		internal void HandleAdrenaline(BinaryReader reader)
		{
			adrenaline = reader.ReadInt32();
			if (Main.netMode == 2)
			{
				AdrenalinePacket(true);
			}
		}

		internal void HandleDeathCount(BinaryReader reader)
		{
			deathCount = reader.ReadInt32();
			if (Main.netMode == 2)
			{
				DeathPacket(true);
			}
		}

		public override void OnEnterWorld()
		{
			if (Main.netMode == 1)
			{
				ExactLevelPacket(false, 0);
				ExactLevelPacket(false, 1);
				ExactLevelPacket(false, 2);
				ExactLevelPacket(false, 3);
				ExactLevelPacket(false, 4);
				LevelPacket(false, 0);
				LevelPacket(false, 1);
				LevelPacket(false, 2);
				LevelPacket(false, 3);
				LevelPacket(false, 4);
				StressPacket(false);
				AdrenalinePacket(false);
				DeathPacket(false);
			}
		}
		#endregion

		#region ProficiencyStuff
		private void GetExactLevelUp()
		{
			if (gainLevelCooldown > 0)
				gainLevelCooldown--;

			#region MeleeLevels
			switch (meleeLevel)
			{
				case 100:
					this.ExactLevelUp(0, 1, false);
					break;
				case 300:
					this.ExactLevelUp(0, 2, false);
					break;
				case 600:
					this.ExactLevelUp(0, 3, false);
					break;
				case 1000:
					this.ExactLevelUp(0, 4, false);
					break;
				case 1500:
					this.ExactLevelUp(0, 5, false);
					break;
				case 2100:
					this.ExactLevelUp(0, 6, false);
					break;
				case 2800:
					this.ExactLevelUp(0, 7, false);
					break;
				case 3600:
					this.ExactLevelUp(0, 8, false);
					break;
				case 4500:
					this.ExactLevelUp(0, 9, false);
					break;
				case 5500:
					this.ExactLevelUp(0, 10, false);
					break;
				case 6600:
					this.ExactLevelUp(0, 11, false);
					break;
				case 7800:
					this.ExactLevelUp(0, 12, false);
					break;
				case 9100:
					this.ExactLevelUp(0, 13, false);
					break;
				case 10500:
					this.ExactLevelUp(0, 14, false);
					break;
				case 12500: //celebration or some shit for final level, yay
					this.ExactLevelUp(0, 15, true);
					break;
				default:
					break;
			}
			#endregion

			#region RangedLevels
			switch (rangedLevel)
			{
				case 100:
					this.ExactLevelUp(1, 1, false);
					break;
				case 300:
					this.ExactLevelUp(1, 2, false);
					break;
				case 600:
					this.ExactLevelUp(1, 3, false);
					break;
				case 1000:
					this.ExactLevelUp(1, 4, false);
					break;
				case 1500:
					this.ExactLevelUp(1, 5, false);
					break;
				case 2100:
					this.ExactLevelUp(1, 6, false);
					break;
				case 2800:
					this.ExactLevelUp(1, 7, false);
					break;
				case 3600:
					this.ExactLevelUp(1, 8, false);
					break;
				case 4500:
					this.ExactLevelUp(1, 9, false);
					break;
				case 5500:
					this.ExactLevelUp(1, 10, false);
					break;
				case 6600:
					this.ExactLevelUp(1, 11, false);
					break;
				case 7800:
					this.ExactLevelUp(1, 12, false);
					break;
				case 9100:
					this.ExactLevelUp(1, 13, false);
					break;
				case 10500:
					this.ExactLevelUp(1, 14, false);
					break;
				case 12500: //celebration or some shit for final level, yay
					this.ExactLevelUp(1, 15, true);
					break;
				default:
					break;
			}
			#endregion

			#region MagicLevels
			switch (magicLevel)
			{
				case 100:
					this.ExactLevelUp(2, 1, false);
					break;
				case 300:
					this.ExactLevelUp(2, 2, false);
					break;
				case 600:
					this.ExactLevelUp(2, 3, false);
					break;
				case 1000:
					this.ExactLevelUp(2, 4, false);
					break;
				case 1500:
					this.ExactLevelUp(2, 5, false);
					break;
				case 2100:
					this.ExactLevelUp(2, 6, false);
					break;
				case 2800:
					this.ExactLevelUp(2, 7, false);
					break;
				case 3600:
					this.ExactLevelUp(2, 8, false);
					break;
				case 4500:
					this.ExactLevelUp(2, 9, false);
					break;
				case 5500:
					this.ExactLevelUp(2, 10, false);
					break;
				case 6600:
					this.ExactLevelUp(2, 11, false);
					break;
				case 7800:
					this.ExactLevelUp(2, 12, false);
					break;
				case 9100:
					this.ExactLevelUp(2, 13, false);
					break;
				case 10500:
					this.ExactLevelUp(2, 14, false);
					break;
				case 12500: //celebration or some shit for final level, yay
					this.ExactLevelUp(2, 15, true);
					break;
				default:
					break;
			}
			#endregion

			#region SummonLevels
			switch (summonLevel)
			{
				case 100:
					this.ExactLevelUp(3, 1, false);
					break;
				case 300:
					this.ExactLevelUp(3, 2, false);
					break;
				case 600:
					this.ExactLevelUp(3, 3, false);
					break;
				case 1000:
					this.ExactLevelUp(3, 4, false);
					break;
				case 1500:
					this.ExactLevelUp(3, 5, false);
					break;
				case 2100:
					this.ExactLevelUp(3, 6, false);
					break;
				case 2800:
					this.ExactLevelUp(3, 7, false);
					break;
				case 3600:
					this.ExactLevelUp(3, 8, false);
					break;
				case 4500:
					this.ExactLevelUp(3, 9, false);
					break;
				case 5500:
					this.ExactLevelUp(3, 10, false);
					break;
				case 6600:
					this.ExactLevelUp(3, 11, false);
					break;
				case 7800:
					this.ExactLevelUp(3, 12, false);
					break;
				case 9100:
					this.ExactLevelUp(3, 13, false);
					break;
				case 10500:
					this.ExactLevelUp(3, 14, false);
					break;
				case 12500: //celebration or some shit for final level, yay
					this.ExactLevelUp(3, 15, true);
					break;
				default:
					break;
			}
			#endregion

			#region RogueLevels
			switch (rogueLevel)
			{
				case 100:
					this.ExactLevelUp(4, 1, false);
					break;
				case 300:
					this.ExactLevelUp(4, 2, false);
					break;
				case 600:
					this.ExactLevelUp(4, 3, false);
					break;
				case 1000:
					this.ExactLevelUp(4, 4, false);
					break;
				case 1500:
					this.ExactLevelUp(4, 5, false);
					break;
				case 2100:
					this.ExactLevelUp(4, 6, false);
					break;
				case 2800:
					this.ExactLevelUp(4, 7, false);
					break;
				case 3600:
					this.ExactLevelUp(4, 8, false);
					break;
				case 4500:
					this.ExactLevelUp(4, 9, false);
					break;
				case 5500:
					this.ExactLevelUp(4, 10, false);
					break;
				case 6600:
					this.ExactLevelUp(4, 11, false);
					break;
				case 7800:
					this.ExactLevelUp(4, 12, false);
					break;
				case 9100:
					this.ExactLevelUp(4, 13, false);
					break;
				case 10500:
					this.ExactLevelUp(4, 14, false);
					break;
				case 12500: //celebration or some shit for final level, yay
					this.ExactLevelUp(4, 15, true);
					break;
				default:
					break;
			}
			#endregion
		}

		private void ExactLevelUp(int levelUpType, int level, bool final)
		{
			Color messageColor = Color.Orange;
			switch (levelUpType)
			{
				case 0:
					exactMeleeLevel = level;
					if (shootFireworksLevelUpMelee)
					{
						string key = (final ? "Melee weapon proficiency maxed out!" : "Melee weapon proficiency level up!");
						shootFireworksLevelUpMelee = false;
						if (Player.whoAmI == Main.myPlayer)
						{
							if (final)
							{
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, -5f, ProjectileID.RocketFireworkRed + Main.rand.Next(4),
									0, 0f, Main.myPlayer, 0f, 1f);
							}
							else
							{
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, -5f, ProjectileID.RocketFireworksBoxRed + Main.rand.Next(4),
									0, 0f, Main.myPlayer, 0f, 0f);
							}
						}
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
					}
					break;
				case 1:
					exactRangedLevel = level;
					if (shootFireworksLevelUpRanged)
					{
						string key = (final ? "Ranged weapon proficiency maxed out!" : "Ranged weapon proficiency level up!");
						messageColor = Color.GreenYellow;
						shootFireworksLevelUpRanged = false;
						if (Player.whoAmI == Main.myPlayer)
						{
							if (final)
							{
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, -5f, ProjectileID.RocketFireworkRed + Main.rand.Next(4),
									0, 0f, Main.myPlayer, 0f, 1f);
							}
							else
							{
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, -5f, ProjectileID.RocketFireworksBoxRed + Main.rand.Next(4),
									0, 0f, Main.myPlayer, 0f, 0f);
							}
						}
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
					}
					break;
				case 2:
					exactMagicLevel = level;
					if (shootFireworksLevelUpMagic)
					{
						string key = (final ? "Magic weapon proficiency maxed out!" : "Magic weapon proficiency level up!");
						messageColor = Color.DodgerBlue;
						shootFireworksLevelUpMagic = false;
						if (Player.whoAmI == Main.myPlayer)
						{
							if (final)
							{
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, -5f, ProjectileID.RocketFireworkRed + Main.rand.Next(4),
									0, 0f, Main.myPlayer, 0f, 1f);
							}
							else
							{
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, -5f, ProjectileID.RocketFireworksBoxRed + Main.rand.Next(4),
									0, 0f, Main.myPlayer, 0f, 0f);
							}
						}
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
					}
					break;
				case 3:
					exactSummonLevel = level;
					if (shootFireworksLevelUpSummon)
					{
						string key = (final ? "Summoner weapon proficiency maxed out!" : "Summoner weapon proficiency level up!");
						messageColor = Color.Aquamarine;
						shootFireworksLevelUpSummon = false;
						if (Player.whoAmI == Main.myPlayer)
						{
							if (final)
							{
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, -5f, ProjectileID.RocketFireworkRed + Main.rand.Next(4),
									0, 0f, Main.myPlayer, 0f, 1f);
							}
							else
							{
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, -5f, ProjectileID.RocketFireworksBoxRed + Main.rand.Next(4),
									0, 0f, Main.myPlayer, 0f, 0f);
							}
						}
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
					}
					break;
				case 4:
					exactRogueLevel = level;
					if (shootFireworksLevelUpRogue)
					{
						string key = (final ? "Rogue weapon proficiency maxed out!" : "Rogue weapon proficiency level up!");
						messageColor = Color.Orchid;
						shootFireworksLevelUpRogue = false;
						if (Player.whoAmI == Main.myPlayer)
						{
							if (final)
							{
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, -5f, ProjectileID.RocketFireworkRed + Main.rand.Next(4),
									0, 0f, Main.myPlayer, 0f, 1f);
							}
							else
							{
								Projectile.NewProjectile(Entity.GetSource_FromThis(null), Player.Center.X, Player.Center.Y, 0f, -5f, ProjectileID.RocketFireworksBoxRed + Main.rand.Next(4),
									0, 0f, Main.myPlayer, 0f, 0f);
							}
						}
						if (Main.netMode == 0)
						{
							Main.NewText(Language.GetTextValue(key), messageColor);
						}
						else if (Main.netMode == 2)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
						}
					}
					break;
			}
			if (Main.netMode == 1)
			{
				ExactLevelPacket(false, levelUpType);
			}
		}

		private void GetStatBonuses()
		{
			#region MeleeLevelBoosts
			if (meleeLevel >= 12500)
			{
				Player.GetDamage(DamageClass.Melee) += 0.12f;
				Player.GetCritChance(DamageClass.Melee) += 6;
			}
			else if (meleeLevel >= 10500)
			{
				Player.GetDamage(DamageClass.Melee) += 0.1f;
				Player.GetCritChance(DamageClass.Melee) += 5;
			}
			else if (meleeLevel >= 9100)
			{
				Player.GetDamage(DamageClass.Melee) += 0.09f;
				Player.GetCritChance(DamageClass.Melee) += 5;
			}
			else if (meleeLevel >= 7800)
			{
				Player.GetDamage(DamageClass.Melee) += 0.08f;
				Player.GetCritChance(DamageClass.Melee) += 4;
			}
			else if (meleeLevel >= 6600)
			{
				Player.GetDamage(DamageClass.Melee) += 0.07f;
				Player.GetCritChance(DamageClass.Melee) += 4;
			}
			else if (meleeLevel >= 5500) //hm limit
			{
				Player.GetDamage(DamageClass.Melee) += 0.06f;
				Player.GetCritChance(DamageClass.Melee) += 3;
			}
			else if (meleeLevel >= 4500)
			{
				Player.GetDamage(DamageClass.Melee) += 0.05f;
				Player.GetCritChance(DamageClass.Melee) += 3;
			}
			else if (meleeLevel >= 3600)
			{
				Player.GetDamage(DamageClass.Melee) += 0.05f;
				Player.GetCritChance(DamageClass.Melee) += 2;
			}
			else if (meleeLevel >= 2800)
			{
				Player.GetDamage(DamageClass.Melee) += 0.04f;
				Player.GetCritChance(DamageClass.Melee) += 2;
			}
			else if (meleeLevel >= 2100)
			{
				Player.GetDamage(DamageClass.Melee) += 0.04f;
				Player.GetCritChance(DamageClass.Melee) += 1;
			}
			else if (meleeLevel >= 1500) //prehm limit
			{
				Player.GetDamage(DamageClass.Melee) += 0.03f;
				Player.GetCritChance(DamageClass.Melee) += 1;
			}
			else if (meleeLevel >= 1000)
			{
				Player.GetDamage(DamageClass.Melee) += 0.03f;
				Player.GetCritChance(DamageClass.Melee) += 1;
			}
			else if (meleeLevel >= 600)
			{
				Player.GetDamage(DamageClass.Melee) += 0.02f;
			}
			else if (meleeLevel >= 300)
				Player.GetDamage(DamageClass.Melee) += 0.02f;
			else if (meleeLevel >= 100)
				Player.GetDamage(DamageClass.Melee) += 0.01f;
			#endregion

			#region RangedLevelBoosts
			if (rangedLevel >= 12500)
			{
				Player.GetDamage(DamageClass.Ranged) += 0.12f;
				Player.moveSpeed += 0.12f;
				Player.GetCritChance(DamageClass.Ranged) += 6;
			}
			else if (rangedLevel >= 10500)
			{
				Player.GetDamage(DamageClass.Ranged) += 0.1f;
				Player.moveSpeed += 0.1f;
				Player.GetCritChance(DamageClass.Ranged) += 5;
			}
			else if (rangedLevel >= 9100)
			{
				Player.GetDamage(DamageClass.Ranged) += 0.09f;
				Player.moveSpeed += 0.09f;
				Player.GetCritChance(DamageClass.Ranged) += 5;
			}
			else if (rangedLevel >= 7800)
			{
				Player.GetDamage(DamageClass.Ranged) += 0.08f;
				Player.moveSpeed += 0.08f;
				Player.GetCritChance(DamageClass.Ranged) += 4;
			}
			else if (rangedLevel >= 6600)
			{
				Player.GetDamage(DamageClass.Ranged) += 0.07f;
				Player.moveSpeed += 0.07f;
				Player.GetCritChance(DamageClass.Ranged) += 4;
			}
			else if (rangedLevel >= 5500)
			{
				Player.GetDamage(DamageClass.Ranged) += 0.06f;
				Player.moveSpeed += 0.06f;
				Player.GetCritChance(DamageClass.Ranged) += 3;
			}
			else if (rangedLevel >= 4500)
			{
				Player.GetDamage(DamageClass.Ranged) += 0.05f;
				Player.moveSpeed += 0.05f;
				Player.GetCritChance(DamageClass.Ranged) += 3;
			}
			else if (rangedLevel >= 3600)
			{
				Player.GetDamage(DamageClass.Ranged) += 0.05f;
				Player.moveSpeed += 0.05f;
				Player.GetCritChance(DamageClass.Ranged) += 2;
			}
			else if (rangedLevel >= 2800)
			{
				Player.GetDamage(DamageClass.Ranged) += 0.04f;
				Player.moveSpeed += 0.04f;
				Player.GetCritChance(DamageClass.Ranged) += 2;
			}
			else if (rangedLevel >= 2100)
			{
				Player.GetDamage(DamageClass.Ranged) += 0.04f;
				Player.moveSpeed += 0.03f;
				Player.GetCritChance(DamageClass.Ranged) += 1;
			}
			else if (rangedLevel >= 1500)
			{
				Player.GetDamage(DamageClass.Ranged) += 0.03f;
				Player.moveSpeed += 0.02f;
				Player.GetCritChance(DamageClass.Ranged) += 1;
			}
			else if (rangedLevel >= 1000)
			{
				Player.GetDamage(DamageClass.Ranged) += 0.03f;
				Player.moveSpeed += 0.01f;
				Player.GetCritChance(DamageClass.Ranged) += 1;
			}
			else if (rangedLevel >= 600)
			{
				Player.GetDamage(DamageClass.Ranged) += 0.02f;
				Player.GetCritChance(DamageClass.Ranged) += 1;
			}
			else if (rangedLevel >= 300)
				Player.GetDamage(DamageClass.Ranged) += 0.02f;
			else if (rangedLevel >= 100)
				Player.GetDamage(DamageClass.Ranged) += 0.01f;
			#endregion

			#region MagicLevelBoosts
			if (magicLevel >= 12500)
			{
				Player.GetDamage(DamageClass.Magic) += 0.12f;
				Player.manaCost *= 0.88f;
				Player.GetCritChance(DamageClass.Magic) += 6;
			}
			else if (magicLevel >= 10500)
			{
				Player.GetDamage(DamageClass.Magic) += 0.1f;
				Player.manaCost *= 0.9f;
				Player.GetCritChance(DamageClass.Magic) += 5;
			}
			else if (magicLevel >= 9100)
			{
				Player.GetDamage(DamageClass.Magic) += 0.09f;
				Player.manaCost *= 0.91f;
				Player.GetCritChance(DamageClass.Magic) += 5;
			}
			else if (magicLevel >= 7800)
			{
				Player.GetDamage(DamageClass.Magic) += 0.08f;
				Player.manaCost *= 0.92f;
				Player.GetCritChance(DamageClass.Magic) += 4;
			}
			else if (magicLevel >= 6600)
			{
				Player.GetDamage(DamageClass.Magic) += 0.07f;
				Player.manaCost *= 0.93f;
				Player.GetCritChance(DamageClass.Magic) += 4;
			}
			else if (magicLevel >= 5500)
			{
				Player.GetDamage(DamageClass.Magic) += 0.06f;
				Player.manaCost *= 0.94f;
				Player.GetCritChance(DamageClass.Magic) += 3;
			}
			else if (magicLevel >= 4500)
			{
				Player.GetDamage(DamageClass.Magic) += 0.05f;
				Player.manaCost *= 0.95f;
				Player.GetCritChance(DamageClass.Magic) += 3;
			}
			else if (magicLevel >= 3600)
			{
				Player.GetDamage(DamageClass.Magic) += 0.05f;
				Player.manaCost *= 0.95f;
				Player.GetCritChance(DamageClass.Magic) += 2;
			}
			else if (magicLevel >= 2800)
			{
				Player.GetDamage(DamageClass.Magic) += 0.04f;
				Player.manaCost *= 0.96f;
				Player.GetCritChance(DamageClass.Magic) += 2;
			}
			else if (magicLevel >= 2100)
			{
				Player.GetDamage(DamageClass.Magic) += 0.04f;
				Player.manaCost *= 0.97f;
				Player.GetCritChance(DamageClass.Magic) += 1;
			}
			else if (magicLevel >= 1500)
			{
				Player.GetDamage(DamageClass.Magic) += 0.03f;
				Player.manaCost *= 0.98f;
				Player.GetCritChance(DamageClass.Magic) += 1;
			}
			else if (magicLevel >= 1000)
			{
				Player.GetDamage(DamageClass.Magic) += 0.03f;
				Player.manaCost *= 0.99f;
				Player.GetCritChance(DamageClass.Magic) += 1;
			}
			else if (magicLevel >= 600)
			{
				Player.GetDamage(DamageClass.Magic) += 0.02f;
				Player.manaCost *= 0.99f;
			}
			else if (magicLevel >= 300)
				Player.GetDamage(DamageClass.Magic) += 0.02f;
			else if (magicLevel >= 100)
				Player.GetDamage(DamageClass.Magic) += 0.01f;
			#endregion

			#region SummonLevelBoosts
			if (summonLevel >= 12500)
			{
				Player.GetDamage(DamageClass.Summon) += 0.12f;
				Player.GetKnockback(DamageClass.Summon).Base += 3.0f;
				Player.maxMinions += 3;
			}
			else if (summonLevel >= 10500)
			{
				Player.GetDamage(DamageClass.Summon) += 0.1f;
				Player.GetKnockback(DamageClass.Summon).Base += 3.0f;
				Player.maxMinions += 2;
			}
			else if (summonLevel >= 9100)
			{
				Player.GetDamage(DamageClass.Summon) += 0.09f;
				Player.GetKnockback(DamageClass.Summon).Base += 2.7f;
				Player.maxMinions += 2;
			}
			else if (summonLevel >= 7800)
			{
				Player.GetDamage(DamageClass.Summon) += 0.08f;
				Player.GetKnockback(DamageClass.Summon).Base += 2.4f;
				Player.maxMinions += 2;
			}
			else if (summonLevel >= 6600)
			{
				Player.GetDamage(DamageClass.Summon) += 0.07f;
				Player.GetKnockback(DamageClass.Summon).Base += 2.1f;
				Player.maxMinions += 2;
			}
			else if (summonLevel >= 5500)
			{
				Player.GetDamage(DamageClass.Summon) += 0.06f;
				Player.GetKnockback(DamageClass.Summon).Base += 1.8f;
				Player.maxMinions += 2;
			}
			else if (summonLevel >= 4500)
			{
				Player.GetDamage(DamageClass.Summon) += 0.06f;
				Player.GetKnockback(DamageClass.Summon).Base += 1.8f;
				Player.maxMinions++;
			}
			else if (summonLevel >= 3600)
			{
				Player.GetDamage(DamageClass.Summon) += 0.05f;
				Player.GetKnockback(DamageClass.Summon).Base += 1.5f;
				Player.maxMinions++;
			}
			else if (summonLevel >= 2800)
			{
				Player.GetDamage(DamageClass.Summon) += 0.04f;
				Player.GetKnockback(DamageClass.Summon).Base += 1.2f;
				Player.maxMinions++;
			}
			else if (summonLevel >= 2100)
			{
				Player.GetDamage(DamageClass.Summon) += 0.04f;
				Player.GetKnockback(DamageClass.Summon).Base += 0.9f;
				Player.maxMinions++;
			}
			else if (summonLevel >= 1500)
			{
				Player.GetDamage(DamageClass.Summon) += 0.03f;
				Player.GetKnockback(DamageClass.Summon).Base += 0.6f;
			}
			else if (summonLevel >= 1000)
			{
				Player.GetDamage(DamageClass.Summon) += 0.03f;
				Player.GetKnockback(DamageClass.Summon).Base += 0.3f;
			}
			else if (summonLevel >= 600)
			{
				Player.GetDamage(DamageClass.Summon) += 0.02f;
				Player.GetKnockback(DamageClass.Summon).Base += 0.3f;
			}
			else if (summonLevel >= 300)
				Player.GetDamage(DamageClass.Summon) += 0.02f;
			else if (summonLevel >= 100)
				Player.GetDamage(DamageClass.Summon) += 0.01f;
			#endregion

			#region RogueLevelBoosts
			if (rogueLevel >= 12500)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.12f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingVelocity += 0.12f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 6;
			}
			else if (rogueLevel >= 10500)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.1f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingVelocity += 0.1f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 5;
			}
			else if (rogueLevel >= 9100)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.09f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingVelocity += 0.09f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 5;
			}
			else if (rogueLevel >= 7800)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.08f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingVelocity += 0.08f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 4;
			}
			else if (rogueLevel >= 6600)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.07f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingVelocity += 0.07f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 4;
			}
			else if (rogueLevel >= 5500)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.06f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingVelocity += 0.06f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 3;
			}
			else if (rogueLevel >= 4500)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.05f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingVelocity += 0.05f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 3;
			}
			else if (rogueLevel >= 3600)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.05f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingVelocity += 0.05f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 2;
			}
			else if (rogueLevel >= 2800)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.04f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingVelocity += 0.04f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 2;
			}
			else if (rogueLevel >= 2100)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.04f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingVelocity += 0.03f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 1;
			}
			else if (rogueLevel >= 1500)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.03f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingVelocity += 0.02f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 1;
			}
			else if (rogueLevel >= 1000)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.03f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingVelocity += 0.01f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingCrit += 1;
			}
			else if (rogueLevel >= 600)
			{
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.02f;
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingVelocity += 0.01f;
			}
			else if (rogueLevel >= 300)
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.02f;
			else if (rogueLevel >= 100)
				CalamityCustomThrowingDamagePlayer.ModPlayer(Player).throwingDamage += 0.01f;
			#endregion
		}

		private float GetMeleeSpeedBonus()
		{
			float meleeSpeedBonus = 0f;
			if (meleeLevel >= 12500)
			{
				meleeSpeedBonus += 0.12f;
			}
			else if (meleeLevel >= 10500)
			{
				meleeSpeedBonus += 0.1f;
			}
			else if (meleeLevel >= 9100)
			{
				meleeSpeedBonus += 0.09f;
			}
			else if (meleeLevel >= 7800)
			{
				meleeSpeedBonus += 0.08f;
			}
			else if (meleeLevel >= 6600)
			{
				meleeSpeedBonus += 0.07f;
			}
			else if (meleeLevel >= 5500) //hm limit
			{
				meleeSpeedBonus += 0.06f;
			}
			else if (meleeLevel >= 4500)
			{
				meleeSpeedBonus += 0.05f;
			}
			else if (meleeLevel >= 3600)
			{
				meleeSpeedBonus += 0.05f;
			}
			else if (meleeLevel >= 2800)
			{
				meleeSpeedBonus += 0.04f;
			}
			else if (meleeLevel >= 2100)
			{
				meleeSpeedBonus += 0.03f;
			}
			else if (meleeLevel >= 1500) //prehm limit
			{
				meleeSpeedBonus += 0.02f;
			}
			else if (meleeLevel >= 1000)
			{
				meleeSpeedBonus += 0.01f;
			}
			else if (meleeLevel >= 600)
			{
				meleeSpeedBonus += 0.01f;
			}
			return meleeSpeedBonus;
		}
		#endregion
	}
}
