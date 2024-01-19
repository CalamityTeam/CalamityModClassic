using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using CalamityModClassic1Point2.NPCs.TheDevourerofGods;
using CalamityModClassic1Point2.NPCs.Calamitas;
using CalamityModClassic1Point2.NPCs.PlaguebringerGoliath;
using CalamityModClassic1Point2.NPCs.Yharon;
using CalamityModClassic1Point2.NPCs.Leviathan;
using CalamityModClassic1Point2.Items.Armor;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace CalamityModClassic1Point2
{
	public class CalamityPlayer : ModPlayer
	{
		private const int saveVersion = 0;
		
		public int modStealthTimer;
		
		public const int stressMax = 10000;
		
		public int stress;
		
		public int stressCD;

		public float modStealth = 1f;
		
		public float shieldInvinc = 5f;
		
		public int dashMod;
		
		public int dashTimeMod;
		
		public int dashDelayMod;
		
		public float defEndurance = 0f;
		
		public int[] modDoubleTapCardinalTimer = new int[4];
		
		public int[] modHoldDownCardinalTimer = new int[4];
		
		public bool afflicted = false;
		
		public bool affliction = false;
		
		public bool afflictedBuff = false;
		
		public bool stressLevel0 = false;
		
		public bool stressLevel100 = false;
		
		public bool stressLevel200 = false;
		
		public bool stressLevel300 = false;
		
		public bool stressLevel400 = false;
		
		public bool stressLevel500 = false;
		
		public bool extraAccessoryML = false;
		
		public bool eCore = false;
		
		public bool pHeart = false;
		
		public bool cShard = false;
		
		public bool mFruit = false;
		
		public bool bOrange = false;
		
		public bool eBerry = false;
		
		public bool dFruit = false;
		
		public bool dodgeScarf = false;
		
		public bool cryogenSoul = false;
		
		public bool yInsignia = false;
		
		public bool eGauntlet = false;
		
		public bool eTalisman = false;
		
		public bool tarraSet = false;
		
		public bool bloodflareSet = false;
		
		public bool elysianAegis = false;
		
		public bool elysianGuard = false;
		
		public bool godSlayer = false;
		
		public bool nCore = false;
		
		public bool godSlayerDamage = false;
		
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
		
		public bool shadeRegen = false;
		
		public bool scarfCooldown = false;
		
		public bool flamethrowerBoost = false;
		
		public bool shadowSpeed = false;
		
		public bool aSpark = false;
		
		public bool aBulwark = false;
		
		public bool dAmulet = false;
		
		public bool fCarapace = false;
		
		public bool gShell = false;
		
		public bool absorber = false;
		
		public bool aAmpoule = false;
		
		public bool pAmulet = false;
		
		public bool auricBoost = false;
		
		public bool fBarrier = false;
		
		public bool aBrain = false;
		
		public bool lol = false;
		
		public bool raiderTalisman = false;
		
		public int raiderStack = 0;
		
		public bool IBoots = false;
		
		public bool elysianFire = false;
		
		public bool dsSetBonus = false;
		
		public bool hAttack = false;
		
		public bool horror = false;
		
		public bool irradiated = false;
		
		public bool bFlames = false;
		
		public bool aFlames = false;
		
		public bool gsInferno = false;
		
		public bool pFlames = false;
		
		public bool hFlames = false;
		
		public bool gState = false;
		
		public bool bBlood = false;
		
		public bool rRage = false;
		
		public bool tRegen = false;
		
		public bool xRage = false;
		
		public bool xWrath = false;
		
		public bool graxDefense = false;
		
		public bool eGravity = false;
		
		public bool sMeleeBoost = false;
		
		public bool tFury = false;
		
		public bool vHex = false;
		
		public bool eGrav = false;
		
		public bool warped = false;
		
		public bool cadence = false;
		
		public bool omniscience = false;
		
		public bool zerg = false;
		
		public bool yPower = false;
		
		public bool aWeapon = false;
		
		public bool tScale = false;
		
		public bool fabsolVodka = false;
		
		public bool mushy = false;
		
		public bool molten = false;
		
		public bool shellBoost = false;
		
		public bool cFreeze = false;
		
		public bool mWorm = false;
		
		public bool cEyes = false;
		
		public bool cSlime = false;
		
		public bool cSlime2 = false;
		
		public bool bStar = false;
		
		public bool SP = false;
		
		public bool dCreeper = false;
		
		public bool bClot = false;
		
		public bool eAxe = false;
		
		public bool SPG = false;
		
		public bool aChicken = false;
		
		public bool cLamp = false;
		
		public bool pGuy = false;
		
		public bool sWaifu = false;
		
		public bool dWaifu = false;
		
		public bool cWaifu = false;
		
		public bool bWaifu = false;
		
		public bool slWaifu = false;
		
		public bool fClump = false;
		
		public bool rDevil = false;
		
		public bool aValkyrie = false;
		
		public bool sGod = false;
		
		public bool vUrchin = false;
		
		public bool cSpirit = false;
		
		public bool rOrb = false;
		
		public bool dCrystal = false;
		
		public bool sandWaifu = false;
		
		public bool sandBoobWaifu = false;
		
		public bool cloudWaifu = false;
		
		public bool brimstoneWaifu = false;
		
		public bool sirenWaifu = false;
		
		public bool fungalClump = false;
		
		public bool redDevil = false;
		
		public bool valkyrie = false;
		
		public bool slimeGod = false;
		
		public bool urchin = false;
		
		public bool chaosSpirit = false;
		
		public bool reaverOrb = false;
		
		public bool daedalusCrystal = false;
		
		public bool frostFlare = false;
		
		public bool beeResist = false;
		
		public bool uberBees = false;
		
		public bool projRef = false;
		
		public bool nanotech = false;
		
		public bool eQuiver = false;
		
		public bool shadowMinions = false;
		
		public bool tearMinions = false;
		
		public bool alchFlask = false;
		
		public bool community = false;
		
		public bool stressPills = false;
		
		public bool laudanum = false;
		
		public bool daedalusReflect = false;
		
		public bool daedalusSplit = false;
		
		public bool reaverBlast = false;
		
		public bool reaverBurst = false;
		
		public bool astralStarRain = false;
		
		public float ataxiaDmg;
		
		public bool ataxiaMage = false;
		
		public bool ataxiaGeyser = false;
		
		public float xerocDmg;
		
		public bool xerocSet = false;
		
		public bool silvaSet = false;
		
		public bool auricSet = false;
		
		public bool ZoneCalamity = false;
		
		public bool ZoneAstral = false;
		
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
			stress = tag.GetInt("stress");
		}
		
		public override void ResetEffects()
		{
			if (extraAccessoryML && Player.extraAccessory && (Main.expertMode || Main.gameMenu))
			{
				Player.extraAccessorySlots = 2;
			}
			else if (Player.extraAccessory && (Main.expertMode || Main.gameMenu))
			{
				Player.extraAccessorySlots = 1;
			}
			else if (extraAccessoryML && (Main.expertMode || Main.gameMenu))
			{
				Player.extraAccessorySlots = 1;
			}
			else
			{
				Player.extraAccessorySlots = 0;
			}
			defEndurance = 0f;
			dashMod = 0;
			afflicted = false;
			affliction = false;
			afflictedBuff = false;
			dodgeScarf = false;
			elysianAegis = false;
			godSlayer = false;
			nCore = false;
			godSlayerDamage = false;
			godSlayerReflect = false;
			godSlayerCooldown = false;
			silvaSet = false;
			auricSet = false;
			ataxiaBolt = false;
			ataxiaGeyser = false;
			shadeRegen = false;
			scarfCooldown = false;
			flamethrowerBoost = false;
			shadowSpeed = false;
			aSpark = false;
			aBulwark = false;
			dAmulet = false;
			fCarapace = false;
			gShell = false;
			absorber = false;
			aAmpoule = false;
			pAmulet = false;
			auricBoost = false;
			fBarrier = false;
			aBrain = false;
			frostFlare = false;
			beeResist = false;
			uberBees = false;
			projRef = false;
			nanotech = false;
			eQuiver = false;
			daedalusReflect = false;
			daedalusSplit = false;
			daedalusAbsorb = false;
			daedalusShard = false;
			reaverSpore = false;
			reaverDoubleTap = false;
			cryogenSoul = false;
			yInsignia = false;
			eGauntlet = false;
			eTalisman = false;
			ataxiaFire = false;
			ataxiaVolley = false;
			ataxiaBlaze = false;
			shadowMinions = false;
			tearMinions = false;
			alchFlask = false;
			community = false;
			stressPills = false;
			laudanum = false;
			reaverBlast = false;
			reaverBurst = false;
			astralStarRain = false;
			ataxiaMage = false;
			tarraSet = false;
			bloodflareSet = false;
			xerocSet = false;
			lol = false;
			raiderTalisman = false;
			IBoots = false;
			elysianFire = false;
			dsSetBonus = false;
			hAttack = false;
			horror = false;
			irradiated = false;
			bFlames = false;
			aFlames = false;
			gsInferno = false;
			pFlames = false;
			hFlames = false;
			gState = false;
			bBlood = false;
			rRage = false;
			xRage = false;
			xWrath = false;
			graxDefense = false;
			eGravity = false;
			sMeleeBoost = false;
			tFury = false;
			vHex = false;
			eGrav = false;
			warped = false;
			cadence = false;
			omniscience = false;
			zerg = false;
			yPower = false;
			aWeapon = false;
			tScale = false;
			fabsolVodka = false;
			mushy = false;
			molten = false;
			shellBoost = false;
			cFreeze = false;
			tRegen = false;
			mWorm = false;
			cEyes = false;
			cSlime = false;
			cSlime2 = false;
			bStar = false;
			SP = false;
			dCreeper = false;
			bClot = false;
			eAxe = false;
			SPG = false;
			aChicken = false;
			cLamp = false;
			pGuy = false;
			sWaifu = false;
			dWaifu = false;
			cWaifu = false;
			bWaifu = false;
			slWaifu = false;
			fClump = false;
			rDevil = false;
			aValkyrie = false;
			sGod = false;
			vUrchin = false;
			cSpirit = false;
			rOrb = false;
			dCrystal = false;
			sandWaifu = false;
			sandBoobWaifu = false;
			cloudWaifu = false;
			brimstoneWaifu = false;
			sirenWaifu = false;
			fungalClump = false;
			redDevil = false;
			valkyrie = false;
			slimeGod = false;
			urchin = false;
			chaosSpirit = false;
			reaverOrb = false;
			daedalusCrystal = false;
		}

		public override void UpdateDead()
		{
			#region Debuffs
			stress = 0;
			raiderStack = 0;
			hAttack = false;
			horror = false;
			irradiated = false;
			bFlames = false;
			aFlames = false;
			gsInferno = false;
			pFlames = false;
			hFlames = false;
			gState = false;
			bBlood = false;
			eGravity = false;
			vHex = false;
			eGrav = false;
			warped = false;
			scarfCooldown = false;
			godSlayerCooldown = false;
			#endregion
			
			#region Buffs
			afflictedBuff = false;
			rRage = false;
			xRage = false;
			xWrath = false;
			graxDefense = false;
			sMeleeBoost = false;
			tFury = false;
			cadence = false;
			omniscience = false;
			zerg = false;
			yPower = false;
			aWeapon = false;
			tScale = false;
			fabsolVodka = false;
			mushy = false;
			molten = false;
			shellBoost = false;
			cFreeze = false;
			tRegen = false;
			#endregion
			
			#region Armorbonuses
			flamethrowerBoost = false;
			shadowSpeed = false;
			godSlayer = false;
			godSlayerDamage = false;
			godSlayerReflect = false;
			auricBoost = false;
			silvaSet = false;
			auricSet = false;
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
			tarraSet = false;
			bloodflareSet = false;
			xerocSet = false;
			IBoots = false;
			elysianFire = false;
			elysianAegis = false;
			elysianGuard = false;
			#endregion
		}
		
		public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
		{
			Item rev = new Item();
			rev.SetDefaults(Mod.Find<ModItem>("Revenge").Type);

            return new Item[] { rev };
		}

		public override void UpdateBadLifeRegen()
		{
			if (irradiated)
			{
				Player.statDefense -= 30 +
					(CalamityWorld.revenge ? 30 : 0);
				Player.endurance -= 0.1f +
					(CalamityWorld.revenge ? 0.1f : 0f);
				Player.GetCritChance(DamageClass.Melee) += 5;
				Player.GetDamage(DamageClass.Melee) += 0.05f;
				Player.GetAttackSpeed(DamageClass.Melee) -= 0.05f;
				Player.GetCritChance(DamageClass.Magic) += 5;
				Player.GetDamage(DamageClass.Magic) += 0.05f;
				Player.GetCritChance(DamageClass.Ranged) += 5;
				Player.GetDamage(DamageClass.Ranged) += 0.05f;
				Player.GetCritChance(DamageClass.Throwing) += 5;
				Player.GetDamage(DamageClass.Throwing) += 0.05f;
				Player.GetDamage(DamageClass.Summon) += 0.05f;
				Player.GetKnockback(DamageClass.Summon).Base += 0.5f;
				Player.moveSpeed += 0.1f;
			}
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
			if (gState)
			{
				Player.statDefense /= 2;
				Player.velocity.Y = 0f;
				Player.velocity.X = 0f;
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
				Player.GetAttackSpeed(DamageClass.Melee) -= 0.025f;
				Player.GetDamage(DamageClass.Melee) += 0.05f;
				Player.GetDamage(DamageClass.Ranged) -= 0.1f;
				Player.GetDamage(DamageClass.Magic) -= 0.1f;
			}
			if (rRage)
			{
				Player.GetCritChance(DamageClass.Melee) += 5;
				Player.GetDamage(DamageClass.Melee) += 0.05f;
				Player.GetCritChance(DamageClass.Magic) += 5;
				Player.GetDamage(DamageClass.Magic) += 0.05f;
				Player.GetCritChance(DamageClass.Ranged) += 5;
				Player.GetDamage(DamageClass.Ranged) += 0.05f;
				Player.GetCritChance(DamageClass.Throwing) += 5;
				Player.GetDamage(DamageClass.Throwing) += 0.05f;
				Player.GetDamage(DamageClass.Summon) += 0.05f;
				Player.GetAttackSpeed(DamageClass.Melee) -= 0.05f;
        		Player.moveSpeed += 0.05f;
			}
			if (tRegen)
			{
				Player.lifeRegen += 10;
			}
			if (tarraSet)
			{
				Player.calmed = true;
				Player.lifeMagnet = true;
			}
			if (xRage)
			{
				Player.GetDamage(DamageClass.Throwing) += 0.1f;
				Player.GetDamage(DamageClass.Ranged) += 0.1f;
				Player.GetDamage(DamageClass.Melee) += 0.1f;
				Player.GetDamage(DamageClass.Magic) += 0.1f;
				Player.GetDamage(DamageClass.Summon) += 0.1f;
			}
			if (xWrath)
			{
				Player.GetCritChance(DamageClass.Melee) += 10;
				Player.GetCritChance(DamageClass.Magic) += 10;
				Player.GetCritChance(DamageClass.Ranged) += 10;
				Player.GetCritChance(DamageClass.Throwing) += 10;
			}
			if (godSlayerCooldown)
			{
				Player.GetDamage(DamageClass.Throwing) += 0.1f;
				Player.GetDamage(DamageClass.Ranged) += 0.1f;
				Player.GetDamage(DamageClass.Melee) += 0.1f;
				Player.GetDamage(DamageClass.Magic) += 0.1f;
				Player.GetDamage(DamageClass.Summon) += 0.1f;
			}
			if (aChicken)
			{
				Player.lifeRegen += 5;
				Player.statDefense += 10;
				Player.moveSpeed += 0.1f;
			}
			if (graxDefense)
			{
				Player.statDefense += 50;
				Player.endurance += 0.05f;
				Player.GetAttackSpeed(DamageClass.Melee) -= 0.1f;
				Player.GetDamage(DamageClass.Melee) += 0.15f;
				Player.GetCritChance(DamageClass.Melee) += 20;
			}
			if (eGravity)
			{
				if (Player.wingTimeMax <= 0)
				{
					Player.wingTimeMax = 0;
				}
				Player.wingTimeMax /= 4;
			}
			if (eGrav)
			{
				if (Player.wingTimeMax <= 0)
				{
					Player.wingTimeMax = 0;
				}
				Player.wingTimeMax /= 2;
			}
			if (warped)
			{
				float randomGravity = ((float)Main.rand.Next(1, 2001) / 1000);
				float randomFallSpeed = ((float)Main.rand.Next(1, 501) / 10);
				Player.gravity = randomGravity;
				Player.maxFallSpeed = randomFallSpeed;
			}
			if (sMeleeBoost)
			{
				Player.GetAttackSpeed(DamageClass.Melee) -= 0.1f;
				Player.GetDamage(DamageClass.Melee) += 0.1f;
				Player.GetCritChance(DamageClass.Melee) += 10;
			}
			if (tFury)
			{
				Player.GetDamage(DamageClass.Melee) += 0.3f;
				Player.GetCritChance(DamageClass.Melee) += 30;
			}
			if (vHex)
			{
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 28;
				Player.blind = true;
				Player.statDefense -= 30;
				Player.moveSpeed -= 0.1f;
				Player.endurance -= 0.3f;
				if (Player.wingTimeMax <= 0)
				{
					Player.wingTimeMax = 0;
				}
				Player.wingTimeMax /= 4;
			}
			if (cadence)
			{
				Player.discountAvailable = true;
				Player.lifeMagnet = true;
				Player.calmed = true;
				Player.loveStruck = true;
				Player.lifeRegen += 2;
				Player.statLifeMax2 += Player.statLifeMax / 5 / 20 * 20;
			}
			if (omniscience)
			{
				Player.detectCreature = true;
				Player.dangerSense = true;
				Player.findTreasure = true;
			}
			if (yPower)
			{
				Player.endurance += 0.06f;
				Player.statDefense += 10;
				Player.GetCritChance(DamageClass.Melee) += 5;
				Player.GetDamage(DamageClass.Melee) += 0.22f;
				Player.GetAttackSpeed(DamageClass.Melee) -= 0.12f;
				Player.GetCritChance(DamageClass.Magic) += 5;
				Player.GetDamage(DamageClass.Magic) += 0.22f;
				Player.GetCritChance(DamageClass.Ranged) += 5;
				Player.GetDamage(DamageClass.Ranged) += 0.22f;
				Player.GetCritChance(DamageClass.Throwing) += 5;
				Player.GetDamage(DamageClass.Throwing) += 0.22f;
				Player.GetDamage(DamageClass.Summon) += 0.22f;
				Player.GetKnockback(DamageClass.Summon).Base += 1.5f;
				Player.moveSpeed += 0.3f;
			}
			if (aWeapon)
			{
				Player.moveSpeed += 0.15f;
			}
			if (tScale)
			{
				Player.endurance += 0.05f;
				Player.statDefense += 5;
				Player.kbBuff = true;
			}
			if (fabsolVodka)
			{
				Player.GetDamage(DamageClass.Throwing) += 0.1f;
				Player.GetDamage(DamageClass.Ranged) += 0.1f;
				Player.GetDamage(DamageClass.Melee) += 0.1f;
				Player.GetDamage(DamageClass.Magic) += 0.1f;
				Player.GetDamage(DamageClass.Summon) += 0.1f;
				Player.statDefense -= 20;
			}
			if (mushy)
			{
				Player.statDefense += 10;
				Player.lifeRegen += 5;
			}
			if (molten)
			{
				Player.resistCold = true;
				Lighting.AddLight((int)(Player.position.X + (float)(Player.width / 2)) / 16, (int)(Player.position.Y + (float)(Player.height / 2)) / 16, 2.5f, 0.6f, 0f);
			}
			if (shellBoost)
			{
				Player.moveSpeed += 0.9f;
			}
			if (nanotech && Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Throwing))
			{
				Player.endurance += 0.1f;
				Player.statDefense += 30;
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
					(NPC.downedMoonlord ? 0.03f : 0f) + //0.15
					(CalamityWorld.downedProvidence ? 0.05f : 0f) + //0.2
					(CalamityWorld.downedDoG ? 0.05f : 0f) + //0.25
					(CalamityWorld.downedYharon ? 0.05f : 0f); //0.3
	        	int integerTypeBoost = (int)(floatTypeBoost * 100f);
	        	int regenBoost = 1 + (integerTypeBoost / 4);
	        	Player.endurance += (floatTypeBoost * 0.5f);
				Player.statDefense += integerTypeBoost;
				Player.GetCritChance(DamageClass.Melee) += integerTypeBoost;
				Player.GetDamage(DamageClass.Melee) += floatTypeBoost;
				Player.GetAttackSpeed(DamageClass.Melee) -= (floatTypeBoost * 0.5f);
				Player.GetCritChance(DamageClass.Magic) += integerTypeBoost;
				Player.GetDamage(DamageClass.Magic) += floatTypeBoost;
				Player.GetCritChance(DamageClass.Ranged) += integerTypeBoost;
				Player.GetDamage(DamageClass.Ranged) += floatTypeBoost;
				Player.GetCritChance(DamageClass.Throwing) += integerTypeBoost;
				Player.GetDamage(DamageClass.Throwing) += floatTypeBoost;
				Player.GetDamage(DamageClass.Summon) += floatTypeBoost;
				Player.GetKnockback(DamageClass.Summon).Base += floatTypeBoost;
				Player.moveSpeed += floatTypeBoost;
	        	Player.statLifeMax2 += Player.statLifeMax / 5 / 20 * integerTypeBoost;
				if (Player.lifeRegen > 0)
				{
					Player.lifeRegen += regenBoost;
				}
				if (Player.wingTimeMax > 0)
				{
					Player.wingTimeMax *= 2;
				}
			}
		}
		
		public override void UpdateLifeRegen()
		{
			bool shinyStoned = Player.shinyStone;
			float num2 = 0f;
			if (!shinyStoned && shadeRegen && (double)Math.Abs(Player.velocity.X) < 0.05 && (double)Math.Abs(Player.velocity.Y) < 0.05 && Player.itemAnimation == 0)
			{
				if (Player.lifeRegen < 0)
				{
					Player.lifeRegen /= 2;
				}
				if (Player.lifeRegenTime > 90 && Player.lifeRegenTime < 1800)
				{
					Player.lifeRegenTime = 1800;
				}
				Player.lifeRegenTime += 4;
				Player.lifeRegen += 4;
				float num3 = (float)(Player.lifeRegenTime - 3000);
				num3 /= 300f;
				if (num3 > 0f)
				{
					if (num3 > 30f)
					{
						num3 = 30f;
					}
					num2 += num3;
				}
				if (Player.lifeRegen > 0 && Player.statLife < Player.statLifeMax2)
				{
					Player.lifeRegenCount++;
					if ((Main.rand.Next(30000) < Player.lifeRegenTime || Main.rand.NextBool(30)))
					{
						int num5 = Dust.NewDust(Player.position, Player.width, Player.height, DustID.ShadowbeamStaff, 0f, 0f, 200, default(Color), 1f);
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
			if (!shinyStoned && shadeRegen)
			{
				num2 *= 1.1f;
			}
			if (!shinyStoned && cFreeze && !shadeRegen && (double)Math.Abs(Player.velocity.X) < 0.05 && (double)Math.Abs(Player.velocity.Y) < 0.05 && Player.itemAnimation == 0)
			{
				if (Player.lifeRegen < 0)
				{
					Player.lifeRegen /= 2;
				}
				if (Player.lifeRegenTime > 90 && Player.lifeRegenTime < 1800)
				{
					Player.lifeRegenTime = 1800;
				}
				Player.lifeRegenTime += 4;
				Player.lifeRegen += 4;
				float num3 = (float)(Player.lifeRegenTime - 3000);
				num3 /= 300f;
				if (num3 > 0f)
				{
					if (num3 > 30f)
					{
						num3 = 30f;
					}
					num2 += num3;
				}
				if (Player.lifeRegen > 0 && Player.statLife < Player.statLifeMax2)
				{
					Player.lifeRegenCount++;
					if ((Main.rand.Next(30000) < Player.lifeRegenTime || Main.rand.NextBool(30)))
					{
						int num5 = Dust.NewDust(Player.position, Player.width, Player.height, DustID.IceRod, 0f, 0f, 200, new Color(150, Main.DiscoG, 255), 0.75f);
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
			if (!shinyStoned && cFreeze && !shadeRegen)
			{
				num2 *= 1.1f;
			}
		}
		
		public override void PostUpdateMiscEffects()
        {
            bool useNebula = NPC.AnyNPCs(Mod.Find<ModNPC>("DevourerofGodsHead").Type);
            Player.ManageSpecialBiomeVisuals("CalamityModClassic1Point2:DevourerofGodsHead", useNebula);
            bool useBrimstone = NPC.AnyNPCs(Mod.Find<ModNPC>("CalamitasRun3").Type);
            Player.ManageSpecialBiomeVisuals("CalamityModClassic1Point2:CalamitasRun3", useBrimstone);
            bool usePlague = NPC.AnyNPCs(Mod.Find<ModNPC>("PlaguebringerGoliath").Type);
            Player.ManageSpecialBiomeVisuals("CalamityModClassic1Point2:PlaguebringerGoliath", usePlague);
            bool useFire = NPC.AnyNPCs(Mod.Find<ModNPC>("Yharon").Type);
            Player.ManageSpecialBiomeVisuals("CalamityModClassic1Point2:Yharon", useFire);
            Player.ManageSpecialBiomeVisuals("HeatDistortion", Main.UseHeatDistortion && useFire);
            bool useWater = NPC.AnyNPCs(Mod.Find<ModNPC>("Leviathan").Type);
            Player.ManageSpecialBiomeVisuals("CalamityModClassic1Point2:Leviathan", useWater);
            bool useHoly = NPC.AnyNPCs(Mod.Find<ModNPC>("Providence").Type);
            Player.ManageSpecialBiomeVisuals("CalamityModClassic1Point2:Providence", useHoly);
            bool useSBrimstone = NPC.AnyNPCs(Mod.Find<ModNPC>("SupremeCalamitas").Type);
            Player.ManageSpecialBiomeVisuals("CalamityModClassic1Point2:SupremeCalamitas", useSBrimstone);
            if (CalamityWorld.revenge && Player.whoAmI == Main.myPlayer)
			{
				int stressGain = 0 +
					(Main.dayTime ? -2 : 2) +
					((!NPC.downedBoss1 && NPC.AnyNPCs(NPCID.EyeofCthulhu)) ? 5 : 0) +
					(Main.SceneMetrics.HasSunflower ? -2 : 0) +
					(Player.ZoneSandstorm ? 6 : 0) +
					(Main.SceneMetrics.HasCampfire ? -2 : 0) +
					(Player.ZoneWaterCandle ? 3 : 0) +
					(Player.ZonePeaceCandle ? -3 : 0) +
					(Player.ZoneSnow ? 2 : 0) +
					((Player.wet && !Player.ignoreWater) ? 1 : 0) +
					((Player.lavaWet && !Player.lavaImmune) ? 8 : 0) +
					((Player.breath <= 0) ? 25 : 0) +
					((Player.ZoneTowerStardust || Player.ZoneTowerVortex || Player.ZoneTowerNebula || Player.ZoneTowerSolar) ? 20 : 0) +
					(Player.ZoneRain ? 2 : 0) +
					(Player.ZoneBeach ? -2 : 0) +
					(Player.ZoneSkyHeight ? 1 : 0) +
					(Player.ZoneDirtLayerHeight ? 1 : 0) +
					(Player.ZoneRockLayerHeight ? 2 : 0) +
					(Player.ZoneUnderworldHeight ? 5 : 0) +
					(Player.ZoneGlowshroom ? -2 : 0) +
					(Player.ZoneCrimson ? 5 : 0) +
					(Player.ZoneJungle ? 3 : 0) +
					(Player.ZoneMeteor ? 1 : 0) +
					(Player.ZoneHallow ? -3 : 0) +
					(Player.ZoneCorrupt ? 5 : 0) +
					(Player.ZoneUndergroundDesert ? 3 : 0) +
					(Player.ZoneDungeon ? 5 : 0) +
					(Main.bloodMoon ? 8 : 0) +
					(Main.pumpkinMoon ? 5 : 0) +
					(Main.snowMoon ? 5 : 0) +
					(ZoneCalamity ? 10 : 0) +
					(ZoneAstral ? 3 : 0) +
					((!NPC.downedBoss2 && NPC.AnyNPCs(NPCID.EaterofWorldsHead)) ? 6 : 0) +
					((!NPC.downedBoss2 && NPC.AnyNPCs(NPCID.BrainofCthulhu)) ? 6 : 0) +
					((!NPC.downedBoss3 && NPC.AnyNPCs(NPCID.SkeletronHead)) ? 3 : 0) +
					((!Main.hardMode && NPC.AnyNPCs(NPCID.WallofFlesh)) ? 17 : 0) +
					((!NPC.downedMechBoss2 && NPC.AnyNPCs(NPCID.Spazmatism)) ? 5 : 0) +
					((!NPC.downedMechBoss2 && NPC.AnyNPCs(NPCID.Retinazer)) ? 5 : 0) +
					((!NPC.downedMechBoss1 && NPC.AnyNPCs(NPCID.TheDestroyer)) ? 10 : 0) +
					((!NPC.downedMechBoss3 && NPC.AnyNPCs(NPCID.SkeletronPrime)) ? 10 : 0) +
					((!NPC.downedPlantBoss && NPC.AnyNPCs(NPCID.Plantera)) ? 5 : 0) +
					((!NPC.downedFishron && NPC.AnyNPCs(NPCID.DukeFishron)) ? 3 : 0) +
					((!NPC.downedMoonlord && NPC.AnyNPCs(NPCID.MoonLordCore)) ? 30 : 0) +
					((!CalamityWorld.downedDesertScourge && NPC.AnyNPCs(Mod.Find<ModNPC>("DesertScourgeHead").Type)) ? 3 : 0) +
					(NPC.AnyNPCs(Mod.Find<ModNPC>("LeviathanStart").Type) ? -10 : 0) +
					(NPC.AnyNPCs(Mod.Find<ModNPC>("Providence").Type) ? -5 : 0) +
					((!CalamityWorld.downedHiveMind && NPC.AnyNPCs(Mod.Find<ModNPC>("HiveMind").Type)) ? 6 : 0) +
					((!CalamityWorld.downedHiveMind && NPC.AnyNPCs(Mod.Find<ModNPC>("HiveMindP2").Type)) ? 6 : 0) +
					((!CalamityWorld.downedPerforator && NPC.AnyNPCs(Mod.Find<ModNPC>("PerforatorHive").Type)) ? 6 : 0) +
					((!CalamityWorld.downedSlimeGod && NPC.AnyNPCs(Mod.Find<ModNPC>("SlimeGodCore").Type)) ? 3 : 0) +
					((!CalamityWorld.downedYharon && NPC.AnyNPCs(Mod.Find<ModNPC>("Yharon").Type)) ? 20 : 0) +
					((!CalamityWorld.downedDoG && NPC.AnyNPCs(Mod.Find<ModNPC>("DevourerofGodsHead").Type)) ? 25 : 0) +
					((!CalamityWorld.downedSCal && NPC.AnyNPCs(Mod.Find<ModNPC>("SupremeCalamitas").Type)) ? 30 : 0) +
					((!CalamityWorld.downedScavenger && NPC.AnyNPCs(Mod.Find<ModNPC>("ScavengerBody").Type)) ? 6 : 0) +
					((!CalamityWorld.downedPlaguebringer && NPC.AnyNPCs(Mod.Find<ModNPC>("PlaguebringerGoliath").Type)) ? 5 : 0) +
					(NPC.AnyNPCs(Mod.Find<ModNPC>("Siren").Type) ? -8 : 0) +
					((!CalamityWorld.downedLeviathan && NPC.AnyNPCs(Mod.Find<ModNPC>("Leviathan").Type)) ? 15 : 0) +
					((!CalamityWorld.downedSentinel3 && NPC.AnyNPCs(Mod.Find<ModNPC>("CosmicWraith").Type)) ? 10 : 0) +
					((!CalamityWorld.downedCalamitas && NPC.AnyNPCs(Mod.Find<ModNPC>("CalamitasRun3").Type)) ? 10 : 0) +
					((!CalamityWorld.downedStarGod && NPC.AnyNPCs(Mod.Find<ModNPC>("AstrumDeusHead").Type)) ? 5 : 0) +
					((!CalamityWorld.downedPolterghast && NPC.AnyNPCs(Mod.Find<ModNPC>("Polterghast").Type)) ? 15 : 0) +
					(Player.wellFed ? -3 : 0) +
					(Player.dryadWard ? -5 : 0) +
					(Player.calmed ? -5 : 0) +
					(Player.frozen ? 15 : 0) +
					(Player.dazed ? 3 : 0) +
					(Player.stoned ? 15 : 0) +
					(Player.webbed ? 15 : 0) +
					(Player.loveStruck ? -10 : 0) +
					(Player.stinky ? 5 : 0) +
					(Main.SceneMetrics.HasHeartLantern ? -2 : 0) +
					(Player.suffocating ? 25 : 0) +
					(Player.inventory[Player.selectedItem].type == Mod.Find<ModItem>("LunicEye").Type ? -8 : 0) +
					(horror ? (5 + (Main.hardMode ? 5 : 0)) : 0) +
					(stressPills ? -8 : 0) +
					(laudanum ? -4 : 0);
				int stressMaxGain = 20 +
					(Main.hardMode ? 20 : 0) +
					(NPC.downedMoonlord ? 20 : 0);
				if (stressGain < -20)
				{
					stressGain = -20;
				}
				if (stressGain > stressMaxGain)
				{
					stressGain = stressMaxGain;
				}
				stressCD++;
				int stressTick = Main.dayTime ? 60 : 54; //every second
				if (Player.inventory[Player.selectedItem].type == ItemID.DemonTorch) //demon torch
				{
					stressTick /= 2;
				}
				if (Player.inventory[Player.selectedItem].type == ItemID.Torch || Player.inventory[Player.selectedItem].type == ItemID.IceTorch) //torch
				{
					stressTick *= 2;
				}
				if (Player.inventory[Player.selectedItem].type == ItemID.UltrabrightTorch) //ultrabright torch
				{
					stressTick *= 4;
				}
				if (stressCD >= stressTick)
				{
					stressCD = 0;
					stress += stressGain;
                    if (stress < 0)
					{
						stress = 0;
					}
					if (stress >= stressMax)
					{
						stress = stressMax;
					}
				}
				stressLevel0 = stress <= 500;
				stressLevel100 = stress >= 2000;
				stressLevel200 = stress >= 4000;
				stressLevel300 = stress >= 6000;
				stressLevel400 = stress >= 8000;
				stressLevel500 = stress >= 9800;
				Player.GetDamage(DamageClass.Melee) += ((float)stress / 10000f) * 0.2f;
				Player.GetCritChance(DamageClass.Melee) += (int)(((float)stress / 10000f) * 20f);
				Player.GetDamage(DamageClass.Ranged) += ((float)stress / 10000f) * 0.2f;
				Player.GetCritChance(DamageClass.Ranged) += (int)(((float)stress / 10000f) * 20f);
				Player.GetDamage(DamageClass.Magic) += ((float)stress / 10000f) * 0.2f;
				Player.GetCritChance(DamageClass.Magic) += (int)(((float)stress / 10000f) * 20f);
				Player.GetDamage(DamageClass.Throwing) += ((float)stress / 10000f) * 0.2f;
				Player.GetCritChance(DamageClass.Throwing) += (int)(((float)stress / 10000f) * 20f);
				Player.GetDamage(DamageClass.Summon) += ((float)stress / 10000f) * 0.2f;
				Player.moveSpeed += ((float)stress / 10000f) * 0.2f;
				Player.statDefense -= (int)(((float)stress / 10000f) * 20f);
				Player.endurance -= ((float)stress / 10000f) * 0.2f;
				int randomEffect = horror ? 8000 : 10000;
				if (stressLevel100)
				{
					if (Main.rand.NextBool(randomEffect))
					{
						Player.AddBuff(BuffID.Blackout, 360);
					}
				}
				if (stressLevel200)
				{
					if (Main.rand.NextBool(randomEffect))
					{
						Player.AddBuff(BuffID.Confused, 120);
					}
				}
				if (stressLevel300)
				{
					if (Main.rand.NextBool(randomEffect))
					{
						int damage = (int)(25.0 * (double)Main.GameModeInfo.EnemyDamageMultiplier);
						Player.Hurt(PlayerDeathReason.ByOther(5), damage, 0, false, false, -1);
					}
				}
				if (stressLevel400)
				{
					int randomTP = horror ? 80000 : 100000;
					if (Main.rand.NextBool(randomTP))
					{
						if (Main.rand.NextBool(2))
						{
							Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror, 0f, 0f, 150, default(Color), 1.1f);
						}
						for (int num345 = 0; num345 < 70; num345++)
						{
							Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror, Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 150, default(Color), 1.5f);
						}
						Player.grappling[0] = -1;
						Player.grapCount = 0;
						for (int num346 = 0; num346 < 1000; num346++)
						{
							if (Main.projectile[num346].active && Main.projectile[num346].owner == Main.myPlayer && Main.projectile[num346].aiStyle == 7)
							{
								Main.projectile[num346].Kill();
							}
						}
						Player.Spawn(new PlayerSpawnContext());
						for (int num347 = 0; num347 < 70; num347++)
						{
							Dust.NewDust(Player.position, Player.width, Player.height, DustID.MagicMirror, 0f, 0f, 150, default(Color), 1.5f);
						}
					}
				}
				if (stressLevel500 && !hAttack)
				{
					if (Main.rand.NextBool(randomEffect))
					{
						int damage = (int)(100.0 * (double)Main.GameModeInfo.EnemyDamageMultiplier);
						Player.Hurt(PlayerDeathReason.ByOther(5), damage, 0, false, false, -1);
						Player.AddBuff(Mod.Find<ModBuff>("HeartAttack").Type, 18000);
					}
				}
				if (Player.talkNPC >= 0)
				{
					if (Main.npc[Player.talkNPC].type == NPCID.Nurse)
					{
						Player.AddBuff(BuffID.PotionSickness, 900);
						Player.AddBuff(BuffID.OgreSpit, 900);
					}
				}
            }
            else if (!CalamityWorld.revenge && Player.whoAmI == Main.myPlayer)
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
			}
			if (stressPills && stressLevel0)
			{
				Player.statDefense += 5;
				Player.GetCritChance(DamageClass.Melee) += 5;
				Player.GetDamage(DamageClass.Melee) += 0.05f;
				Player.GetCritChance(DamageClass.Magic) += 5;
				Player.GetDamage(DamageClass.Magic) += 0.05f;
				Player.GetCritChance(DamageClass.Ranged) += 5;
				Player.GetDamage(DamageClass.Ranged) += 0.05f;
				Player.GetCritChance(DamageClass.Throwing) += 5;
				Player.GetDamage(DamageClass.Throwing) += 0.05f;
				Player.GetDamage(DamageClass.Summon) += 0.05f;
			}
			if (laudanum && !stressLevel0)
			{
				Player.statDefense += 8;
				Player.GetCritChance(DamageClass.Melee) += 6;
				Player.GetDamage(DamageClass.Melee) += 0.06f;
				Player.GetCritChance(DamageClass.Magic) += 6;
				Player.GetDamage(DamageClass.Magic) += 0.06f;
				Player.GetCritChance(DamageClass.Ranged) += 6;
				Player.GetDamage(DamageClass.Ranged) += 0.06f;
				Player.GetCritChance(DamageClass.Throwing) += 6;
				Player.GetDamage(DamageClass.Throwing) += 0.06f;
				Player.GetDamage(DamageClass.Summon) += 0.06f;
			}
			if (!stressLevel500 && Player.FindBuffIndex(Mod.Find<ModBuff>("HeartAttack").Type) > -1)
			{
				Player.ClearBuff(Mod.Find<ModBuff>("HeartAttack").Type);
			}
			if (hAttack)
			{
				if (!Main.dayTime)
				{
					Player.lifeRegen += 4;
					Player.statLifeMax2 += Player.statLifeMax / 5 / 20 * 20;
				}
				else
				{
					Player.GetDamage(DamageClass.Throwing) -= 0.3f;
					Player.GetDamage(DamageClass.Ranged) -= 0.3f;
					Player.GetDamage(DamageClass.Melee) -= 0.3f;
					Player.GetDamage(DamageClass.Magic) -= 0.3f;
					Player.GetDamage(DamageClass.Summon) -= 0.3f;
					Player.blind = true;
				}
			}
			if (affliction)
			{
				Player.lifeRegen += CalamityWorld.revenge ? 4 : 2;
				Player.endurance += CalamityWorld.revenge ? 0.08f : 0.05f;
				Player.statDefense += CalamityWorld.revenge ? 45 : 30;
				Player.GetDamage(DamageClass.Throwing) += CalamityWorld.revenge ? 0.15f : 0.1f;
				Player.GetDamage(DamageClass.Ranged) += CalamityWorld.revenge ? 0.15f : 0.1f;
				Player.GetDamage(DamageClass.Melee) += CalamityWorld.revenge ? 0.15f : 0.1f;
				Player.GetDamage(DamageClass.Magic) += CalamityWorld.revenge ? 0.15f : 0.1f;
				Player.GetDamage(DamageClass.Summon) += CalamityWorld.revenge ? 0.15f : 0.1f;
				Player.statLifeMax2 += CalamityWorld.revenge ? (Player.statLifeMax / 5 / 20 * 20) : (Player.statLifeMax / 5 / 20 * 10);
			}
			else if (afflicted)
			{
				Player.lifeRegen += CalamityWorld.revenge ? 4 : 2;
				Player.endurance += CalamityWorld.revenge ? 0.08f : 0.05f;
				Player.statDefense += CalamityWorld.revenge ? 45 : 30;
				Player.GetDamage(DamageClass.Throwing) += CalamityWorld.revenge ? 0.15f : 0.1f;
				Player.GetDamage(DamageClass.Ranged) += CalamityWorld.revenge ? 0.15f : 0.1f;
				Player.GetDamage(DamageClass.Melee) += CalamityWorld.revenge ? 0.15f : 0.1f;
				Player.GetDamage(DamageClass.Magic) += CalamityWorld.revenge ? 0.15f : 0.1f;
				Player.GetDamage(DamageClass.Summon) += CalamityWorld.revenge ? 0.15f : 0.1f;
				Player.statLifeMax2 += CalamityWorld.revenge ? (Player.statLifeMax / 5 / 20 * 20) : (Player.statLifeMax / 5 / 20 * 10);
			}
			if (afflictedBuff)
			{
				afflicted = true;
			}
			Player.statLifeMax2 +=
				(mFruit ? 50 : 0) +
				(bOrange ? 50 : 0) +
				(eBerry ? 50 : 0) +
				(dFruit ? 50 : 0);
			Player.statManaMax2 +=
				(pHeart ? 100 : 0) +
				(eCore ? 100 : 0) +
				(cShard ? 50 : 0);
			if (Main.netMode != NetmodeID.Server && Player.whoAmI == Main.myPlayer)
			{
				ReLogic.Content.Asset<Texture2D> rain3 = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/Rain3");
				ReLogic.Content.Asset<Texture2D> rainOriginal = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/RainOriginal");
				ReLogic.Content.Asset<Texture2D> mana2 = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/Mana2");
				ReLogic.Content.Asset<Texture2D> mana3 = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/Mana3");
				ReLogic.Content.Asset<Texture2D> mana4 = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/Mana4");
				ReLogic.Content.Asset<Texture2D> manaOriginal = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/ManaOriginal");
				ReLogic.Content.Asset<Texture2D> heart3 = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/Heart3");
				ReLogic.Content.Asset<Texture2D> heart4 = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/Heart4");
				ReLogic.Content.Asset<Texture2D> heart5 = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/Heart5");
				ReLogic.Content.Asset<Texture2D> heart6 = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/Heart6");
				ReLogic.Content.Asset<Texture2D> heartOriginal = ModContent.Request<Texture2D>("CalamityModClassic1Point2/ExtraTextures/HeartOriginal");
				int totalFruit = 
					(mFruit ? 1 : 0) +
					(bOrange ? 1 : 0) +
					(eBerry ? 1 : 0) +
					(dFruit ? 1 : 0);
				int totalManaBoost = 
					(pHeart ? 1 : 0) +
					(eCore ? 1 : 0) +
					(cShard ? 1 : 0);
				if (totalFruit == 4)
				{
                    TextureAssets.Heart2 = heart6;
				}
				else if (totalFruit == 3)
				{
                    TextureAssets.Heart2 = heart5;
				}
				else if (totalFruit == 2)
				{
					TextureAssets.Heart2 = heart4;
				}
				else if (totalFruit == 1)
				{
                    TextureAssets.Heart2 = heart3;
				}
				else
				{
                    TextureAssets.Heart2 = heartOriginal;
				}
				if (totalManaBoost == 3)
				{
                    TextureAssets.Mana = mana4;
				}
				else if (totalManaBoost == 2)
				{
                    TextureAssets.Mana = mana3;
				}
				else if (totalManaBoost == 1)
				{
                    TextureAssets.Mana = mana2;
				}
				else
				{
                    TextureAssets.Mana = manaOriginal;
				}
				if (Main.bloodMoon)
				{
                    TextureAssets.Rain = rainOriginal;
				}
				else if (Main.raining && NPC.downedMoonlord)
				{
                    TextureAssets.Rain = rain3;
				}
				else
				{
                    TextureAssets.Rain = rainOriginal;
				}
			}
			if (CalamityWorld.revenge)
			{
				Player.GetDamage(DamageClass.Throwing) += 0.1f;
				Player.GetDamage(DamageClass.Ranged) += 0.1f;
				Player.GetDamage(DamageClass.Melee) += 0.1f;
				Player.GetDamage(DamageClass.Magic) += 0.1f;
				Player.GetDamage(DamageClass.Summon) += 0.1f;
				if (NPC.downedMoonlord)
				{
					Player.lifeRegen -= 3;
				}
			}
			else if (NPC.downedMoonlord)
			{
				Player.lifeRegen -= 1;
			}
			if (Main.raining && NPC.downedMoonlord)
			{
				if (Player.ZoneOverworldHeight || Player.ZoneSkyHeight)
				{
					Player.AddBuff(Mod.Find<ModBuff>("Irradiated").Type, 2);
				}
			}
			if (raiderTalisman)
			{
				Player.GetDamage(DamageClass.Throwing) += ((float)raiderStack / 100f) * 0.4f;
			}
			if (absorber)
			{
				Player.moveSpeed += 0.12f;
	        	Player.jumpSpeedBoost += 1.2f;
	        	Player.statLifeMax2 += 30;
	        	Player.thorns = 1f;
	        	Player.endurance += 0.06f;
				if ((double)Math.Abs(Player.velocity.X) < 0.05 && (double)Math.Abs(Player.velocity.Y) < 0.05 && Player.itemAnimation == 0)
				{
					Player.lifeRegen += 6;
					Player.manaRegenBonus += 2;
				}
				if (Player.wet == true || Player.honeyWet == true || Player.lavaWet == true)
				{
					Player.statDefense += 5;
					Player.endurance += 0.07f;
					Player.moveSpeed += 0.2f;
				}
			}
			if (aAmpoule)
			{
				Lighting.AddLight((int)(Player.position.X + (float)(Player.width / 2)) / 16, (int)(Player.position.Y + (float)(Player.height / 2)) / 16, 1f, 1f, 0.6f);
				Player.endurance += 0.12f;
				Player.pickSpeed -= 0.5f;
				Player.buffImmune[70] = true;
				Player.buffImmune[47] = true;
				Player.buffImmune[46] = true;
				Player.buffImmune[44] = true;
				Player.buffImmune[20] = true;
				if (!Player.honey && Player.lifeRegen < 0)
				{
					Player.lifeRegen += 4;
					if (Player.lifeRegen > 0)
					{
						Player.lifeRegen = 0;
					}
				}
				Player.lifeRegenTime += 2;
				Player.lifeRegen += 4;
			}
			if (cFreeze)
			{
				Lighting.AddLight((int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f), 0.3f, ((float)Main.DiscoG / 400f), 0.5f);
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
			if (vHex)
			{
				for (int num569 = 0; num569 < 200; num569++)
				{
					if (Main.npc[num569].active && Main.npc[num569].type == (NPCID.Nurse))
					{
						Main.npc[num569].life = 0;
					}
				}
			}
			if (Player.pulley)
			{
				ModDashMovement();
			}
			else if (Player.grappling[0] == -1 && !Player.tongued)
			{
				ModHorizontalMovement();
				ModDashMovement();
				if (pAmulet && modStealth < 1f)
				{
					float num43 = Player.maxRunSpeed / 2f * (1f - modStealth);
					Player.maxRunSpeed -= num43;
					Player.accRunSpeed = Player.maxRunSpeed;
				}
			}
			if (auricBoost)
			{
				if (Player.itemAnimation > 0)
				{
					modStealthTimer = 5;
				}
				if ((double)Player.velocity.X > -0.1 && (double)Player.velocity.X < 0.1 && (double)Player.velocity.Y > -0.1 && (double)Player.velocity.Y < 0.1 && !Player.mount.Active)
				{
					if (modStealthTimer == 0 && modStealth > 0f)
					{
						modStealth -= 0.015f;
						if ((double)modStealth <= 0.0)
						{
							modStealth = 0f;
							if (Main.netMode == NetmodeID.MultiplayerClient)
							{
								NetMessage.SendData(MessageID.PlayerStealth, -1, -1, null, Player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
							}
						}
					}
				}
				else
				{
					float num27 = Math.Abs(Player.velocity.X) + Math.Abs(Player.velocity.Y);
					modStealth += num27 * 0.0075f;
					if (modStealth > 1f)
					{
						modStealth = 1f;
					}
					if (Player.mount.Active)
					{
						modStealth = 1f;
					}
				}
				Player.GetDamage(DamageClass.Melee) += (1f - modStealth) * 1.15f;
				Player.GetCritChance(DamageClass.Melee) += (int)((1f - modStealth) * 15f);
				Player.GetDamage(DamageClass.Ranged) += (1f - modStealth) * 1.15f;
				Player.GetCritChance(DamageClass.Ranged) += (int)((1f - modStealth) * 15f);
				Player.GetDamage(DamageClass.Magic) += (1f - modStealth) * 1.15f;
				Player.GetCritChance(DamageClass.Magic) += (int)((1f - modStealth) * 15f);
				Player.GetDamage(DamageClass.Throwing) += (1f - modStealth) * 1.15f;
				Player.GetCritChance(DamageClass.Throwing) += (int)((1f - modStealth) * 15f);
				Player.GetDamage(DamageClass.Summon) += (1f - modStealth) * 1.15f;
				if (modStealthTimer > 0)
				{
					modStealthTimer--;
				}
			}
			else if (pAmulet)
			{
				if (Player.itemAnimation > 0)
				{
					modStealthTimer = 5;
				}
				if ((double)Player.velocity.X > -0.1 && (double)Player.velocity.X < 0.1 && (double)Player.velocity.Y > -0.1 && (double)Player.velocity.Y < 0.1 && !Player.mount.Active)
				{
					if (modStealthTimer == 0 && modStealth > 0f)
					{
						modStealth -= 0.015f;
						if ((double)modStealth <= 0.0)
						{
							modStealth = 0f;
							if (Main.netMode == NetmodeID.MultiplayerClient)
							{
								NetMessage.SendData(MessageID.PlayerStealth, -1, -1, null, Player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
							}
						}
					}
				}
				else
				{
					float num27 = Math.Abs(Player.velocity.X) + Math.Abs(Player.velocity.Y);
					modStealth += num27 * 0.0075f;
					if (modStealth > 1f)
					{
						modStealth = 1f;
					}
					if (Player.mount.Active)
					{
						modStealth = 1f;
					}
				}
				Player.GetDamage(DamageClass.Melee) += (1f - modStealth) * 1.2f;
				Player.GetCritChance(DamageClass.Melee) += (int)((1f - modStealth) * 20f);
				Player.aggro -= (int)((1f - modStealth) * 750f);
				if (modStealthTimer > 0)
				{
					modStealthTimer--;
				}
			}
			else
			{
				modStealth = 1f;
			}
			if (Player.whoAmI == Main.myPlayer)
			{
				if (Main.hasFocus)
				{
					for (int k = 0; k < modDoubleTapCardinalTimer.Length; k++)
					{
						modDoubleTapCardinalTimer[k]--;
						if (modDoubleTapCardinalTimer[k] < 0)
						{
							modDoubleTapCardinalTimer[k] = 0;
						}
					}
					for (int l = 0; l < 4; l++)
					{
						bool flag5 = false;
						bool flag6 = false;
						switch (l)
						{
						case 0:
							flag5 = (Player.controlDown && Player.releaseDown);
							flag6 = Player.controlDown;
							break;
						case 1:
							flag5 = (Player.controlUp && Player.releaseUp);
							flag6 = Player.controlUp;
							break;
						case 2:
							flag5 = (Player.controlRight && Player.releaseRight);
							flag6 = Player.controlRight;
							break;
						case 3:
							flag5 = (Player.controlLeft && Player.releaseLeft);
							flag6 = Player.controlLeft;
							break;
						}
						if (flag5)
						{
							if (modDoubleTapCardinalTimer[l] > 0)
							{
								ModKeyDoubleTap(l);
							}
							else
							{
								modDoubleTapCardinalTimer[l] = 15;
							}
						}
						if (flag6)
						{
							modHoldDownCardinalTimer[l]++;
							Player.KeyHoldDown(l, modHoldDownCardinalTimer[l]);
						}
						else
						{
							modHoldDownCardinalTimer[l] = 0;
						}
					}
				}
			}
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
					if (shieldInvinc == 0f && num29 != shieldInvinc && Main.netMode == NetmodeID.MultiplayerClient)
					{
						NetMessage.SendData(MessageID.PlayerStealth, -1, -1, null, Player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
					Player.GetDamage(DamageClass.Ranged) += (5f - shieldInvinc) * 0.08f;
					Player.GetCritChance(DamageClass.Ranged) += (int)((5f - shieldInvinc) * 8f);
					Player.GetDamage(DamageClass.Melee) += (5f - shieldInvinc) * 0.08f;
					Player.GetCritChance(DamageClass.Melee) += (int)((5f - shieldInvinc) * 8f);
					Player.GetDamage(DamageClass.Magic) += (5f - shieldInvinc) * 0.08f;
					Player.GetCritChance(DamageClass.Magic) += (int)((5f - shieldInvinc) * 8f);
					Player.GetDamage(DamageClass.Summon) += (5f - shieldInvinc) * 0.08f;
					Player.GetDamage(DamageClass.Throwing) += (5f - shieldInvinc) * 0.08f;
					Player.GetCritChance(DamageClass.Throwing) += (int)((5f - shieldInvinc) * 8f);
					Player.aggro += (int)((5f - shieldInvinc) * 220f);
					Player.statDefense += (int)((5f - shieldInvinc) * 20f);
					Player.moveSpeed *= 0.75f;
					Player.runAcceleration *= 0.3f;
					Player.maxRunSpeed *= 0.3f;
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
					if (shieldInvinc == 5f && num30 != shieldInvinc && Main.netMode == NetmodeID.MultiplayerClient)
					{
						NetMessage.SendData(MessageID.PlayerStealth, -1, -1, null, Player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (flag14)
				{
					if (Main.rand.NextBool(2))
					{
						Vector2 vector = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						Dust dust = Main.dust[Dust.NewDust(Player.Center - vector * 30f, 0, 0, DustID.CopperCoin, 0f, 0f, 0, default(Color), 1f)];
						dust.noGravity = true;
						dust.position = Player.Center - vector * (float)Main.rand.Next(5, 11);
						dust.velocity = vector.RotatedBy(1.5707963705062866, default(Vector2)) * 4f;
						dust.scale = 0.5f + Main.rand.NextFloat();
						dust.fadeIn = 0.5f;
					}
					if (Main.rand.NextBool(2))
					{
						Vector2 vector2 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						Dust dust2 = Main.dust[Dust.NewDust(Player.Center - vector2 * 30f, 0, 0, DustID.GoldCoin, 0f, 0f, 0, default(Color), 1f)];
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
		}
		
		public override void PostUpdateEquips()
		{
			defEndurance = CalamityWorld.revenge ? 0.5f : 0.6f;
			if (Player.endurance >= defEndurance)
			{
				Player.endurance = defEndurance;
			}
		}
		
		public override void PostUpdateRunSpeeds()
		{
			if (hAttack)
			{
				if (!Main.dayTime)
				{
					Player.runAcceleration *= 1.1f;
					Player.maxRunSpeed *= 1.1f;
				}
			}
			if ((stressPills && stressLevel0) || (laudanum && !stressLevel0))
			{
				Player.runAcceleration *= 1.05f;
				Player.maxRunSpeed *= 1.05f;
			}
			
			if (silvaSet)
			{
				Player.runAcceleration *= 1.3f;
				Player.maxRunSpeed *= 1.3f;
			}
			if (auricSet)
			{
				Player.runAcceleration *= 1.4f;
				Player.maxRunSpeed *= 1.4f;
			}
			if (shadowSpeed)
			{
				Player.runAcceleration *= 1.5f;
				Player.maxRunSpeed *= 1.5f;
			}
		}

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (lol)
            {
				return true;
            }
            if (godSlayerDamage && info.Damage <= 80)
            {
				return true;
            }
            if (godSlayerReflect && Main.rand.NextBool(20))
            {
				return true;
            }
			return false;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (nCore && Main.rand.NextBool(10))
			{
				SoundEngine.PlaySound(SoundID.Item67, Player.position);
				for (int j = 0; j < 50; j++)
				{
					int num = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Dust expr_A4_cp_0 = Main.dust[num];
					expr_A4_cp_0.position.X = expr_A4_cp_0.position.X + (float)Main.rand.Next(-20, 21);
					Dust expr_CB_cp_0 = Main.dust[num];
					expr_CB_cp_0.position.Y = expr_CB_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
					Main.dust[num].velocity *= 0.9f;
					Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
					if (Main.rand.NextBool(2))
					{
						Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					}
				}
				Player.statLife += 100;
    			Player.HealEffect(100);
				return false;
			}
			if (godSlayer && !godSlayerCooldown)
			{
				SoundEngine.PlaySound(SoundID.Item67, Player.position);
				for (int j = 0; j < 100; j++)
				{
					int num = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Dust expr_A4_cp_0 = Main.dust[num];
					expr_A4_cp_0.position.X = expr_A4_cp_0.position.X + (float)Main.rand.Next(-20, 21);
					Dust expr_CB_cp_0 = Main.dust[num];
					expr_CB_cp_0.position.Y = expr_CB_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
					Main.dust[num].velocity *= 0.9f;
					Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
					if (Main.rand.NextBool(2))
					{
						Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					}
				}
				Player.statLife += 300;
    			Player.HealEffect(300);
    			Player.AddBuff(Mod.Find<ModBuff>("GodSlayerCooldown").Type, 2400);
				return false;
			}
			if (bBlood && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(Player.name + " became a blood geyser");
			}
			if ((bFlames || aFlames) && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was consumed by the black flames");
			}
			if (pFlames && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(Player.name + "'s flesh was melted by the Plague");
			}
			if (hFlames && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
			{
				damageSource = PlayerDeathReason.ByCustomReason(Player.name + " fell prey to their sins");
			}
			return true;
		}

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
			if (dodgeScarf && item.CountsAsClass(DamageClass.Melee) && (item.shoot == ProjectileID.None || item.shoot == Mod.Find<ModProjectile>("NobodyKnows").Type))
            {
                damage *= 1.2f;
            }
			if (flamethrowerBoost && item.CountsAsClass(DamageClass.Ranged) && item.useAmmo == 23)
			{
				damage *= 1.25f;
			}
		}
		
		public override void ModifyWeaponKnockback(Item item, ref StatModifier knockback)
		{
			if (pAmulet && item.CountsAsClass(DamageClass.Melee))
			{
				knockback *= 1f + (1f - modStealth) * 0.5f;
			}
			if (auricBoost)
			{
				knockback *= 1f + (1f - modStealth) * 0.5f;
			}
		}
		
		public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (ataxiaBolt && item.CountsAsClass(DamageClass.Ranged) && Main.rand.NextBool(2))
			{
				int projDamage = item.damage;
				if (Player.whoAmI == Main.myPlayer)
				{
					Projectile.NewProjectile(Player.GetSource_FromThis(), position.X, position.Y, velocity.X * 1.25f, velocity.Y * 1.25f, Mod.Find<ModProjectile>("ChaosFlare").Type, projDamage * 2, 2f, Player.whoAmI, 0f, 0f);
				}
			}
			else if (ataxiaVolley && item.CountsAsClass(DamageClass.Throwing) && Main.rand.NextBool(10))
			{
				int projDamage = item.damage;
				if (Player.whoAmI == Main.myPlayer)
				{
					SoundEngine.PlaySound(SoundID.Item20, Player.position);
					float spread = 45f * 0.0174f;
					double startAngle = Math.Atan2(Player.velocity.X, Player.velocity.Y) - spread / 2;
					double deltaAngle = spread / 8f;
					double offsetAngle;
					int i;
					for (i = 0; i < 4; i++) 
					{
    					Vector2 vector2 = new Vector2(Player.Center.X, Player.Center.Y);
						offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
						Projectile.NewProjectile(Player.GetSource_FromThis(), vector2.X, vector2.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("ChaosFlare2").Type, projDamage / 2, 1.25f, Player.whoAmI, 0f, 0f);
						Projectile.NewProjectile(Player.GetSource_FromThis(), vector2.X, vector2.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("ChaosFlare2").Type, projDamage / 2, 1.25f, Player.whoAmI, 0f, 0f);
					}
				}
			}
			else if (reaverDoubleTap && item.CountsAsClass(DamageClass.Ranged) && Main.rand.Next(0, 100) >= 85)
			{
				int projDamage = item.damage;
				if (Player.whoAmI == Main.myPlayer)
				{
					Projectile.NewProjectile(Player.GetSource_FromThis(), position.X, position.Y, velocity.X * 1.25f, velocity.Y * 1.25f, Mod.Find<ModProjectile>("MiniRocket").Type, (int)((double)projDamage * 2.4f), 2f, Player.whoAmI, 0f, 0f);
				}
			}
			return true;
		}
		
		public override void MeleeEffects(Item item, Rectangle hitbox)
		{
			if (aWeapon && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
			{
				if (Main.rand.NextBool(3))
		        {
		        	int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, Mod.Find<ModDust>("BrimstoneFlame").Type, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
		        }
			}
			if (aChicken && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
			{
				if (Main.rand.NextBool(3))
		        {
		        	int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, DustID.CopperCoin, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
		        }
			}
			if (eGauntlet && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
			{
				if (Main.rand.NextBool(3))
		        {
					int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, DustID.RainbowTorch, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.25f);
		        	Main.dust[num280].noGravity = true;
		        }
			}
			if (cryogenSoul && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
			{
				if (Main.rand.NextBool(3))
		        {
		        	int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, DustID.IceRod, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
		        }
			}
			if (xerocSet && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
			{
				if (Main.rand.NextBool(3))
		        {
		        	int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, DustID.Enchanted_Pink, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 1.25f);
		        }
			}
			if (reaverBlast && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
			{
				if (Main.rand.NextBool(3))
		        {
		        	int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, DustID.GreenFairy, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
		        }
			}
			if (dsSetBonus && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
			{
				if (Main.rand.NextBool(3))
		        {
		        	int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, DustID.Shadowflame, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 2.5f);
		        }
			}
		}
		
		public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
		{
			if (tarraSet)
			{
				if (Main.rand.NextBool(12))
				{
					Player.AddBuff(Mod.Find<ModBuff>("TarraLifeRegen").Type, Main.rand.Next(120, 360));
				}
			}
			if (eGauntlet)
			{
				target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 120, false);
				target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120, false);
				target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 120, false);
				target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 120, false);
				if (Main.rand.NextBool(5))
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
				if (Main.rand.NextBool(4))
				{
					target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 360, false);
				}
				else if (Main.rand.NextBool(2))
				{
					target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 240, false);
				}
				else
				{
					target.AddBuff(Mod.Find<ModBuff>("AbyssalFlames").Type, 120, false);
				}
			}
			if (dsSetBonus)
			{
				if (Main.rand.NextBool(4))
				{
					target.AddBuff(153, 360, false);
				}
				else if (Main.rand.NextBool(2))
				{
					target.AddBuff(153, 240, false);
				}
				else
				{
					target.AddBuff(153, 120, false);
				}
			}
			if (cryogenSoul || frostFlare)
			{
				if (Main.rand.NextBool(4))
				{
					target.AddBuff(44, 360, false);
				}
				else if (Main.rand.NextBool(2))
				{
					target.AddBuff(44, 240, false);
				}
				else
				{
					target.AddBuff(44, 120, false);
				}
			}
			if (aChicken)
			{
				if (Main.rand.NextBool(4))
				{
					target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 360, false);
				}
				else if (Main.rand.NextBool(2))
				{
					target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 240, false);
				}
				else
				{
					target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 120, false);
				}
			}
			if (yInsignia)
			{
				if (Main.rand.NextBool(4))
				{
					target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 360, false);
				}
				else if (Main.rand.NextBool(2))
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
				if (Main.rand.NextBool(4))
				{
					target.AddBuff(BuffID.OnFire, 720, false);
				}
				else if (Main.rand.NextBool(2))
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
				if (Main.rand.NextBool(4))
				{
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 360, false);
				}
				else if (Main.rand.NextBool(2))
				{
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 240, false);
				}
				else
				{
					target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 120, false);
				}
			}
			if (ataxiaGeyser && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
			{
				if (Player.whoAmI == Main.myPlayer)
				{
					Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ChaosGeyser").Type, 60, 2f, Player.whoAmI, 0f, 0f);
				}
			}
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (astralStarRain && hit.Crit)
            {
                if (Player.whoAmI == Main.myPlayer)
                {
                    for (int n = 0; n < 3; n++)
                    {
                        float x = target.position.X + (float)Main.rand.Next(-400, 400);
                        float y = target.position.Y - (float)Main.rand.Next(500, 800);
                        Vector2 vector = new Vector2(x, y);
                        float num13 = target.position.X + (float)(target.width / 2) - vector.X;
                        float num14 = target.position.Y + (float)(target.height / 2) - vector.Y;
                        num13 += (float)Main.rand.Next(-100, 101);
                        int num15 = 25;
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
                        num16 = (float)num15 / num16;
                        num13 *= num16;
                        num14 *= num16;
                        int num17 = Projectile.NewProjectile(Player.GetSource_FromThis(), x, y, num13, num14, projectileType, 100, 5f, Player.whoAmI, 0f, 0f);
                        Main.projectile[num17].DamageType = DamageClass.Default;
                    }
                }
            }
        }
		
		public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
		{
			if (Player.FindBuffIndex(BuffID.Gills) > -1 && Main.hardMode && !attempt.inLava && Main.rand.NextBool(5))
			{
				itemDrop = Mod.Find<ModItem>("Floodtide").Type;
			}
		}
		
		public override void ModifyHurt(ref Player.HurtModifiers modifiers)
		{
			if (auricSet)
            {
				modifiers.FinalDamage *= 0.9f;
            }
			else if (silvaSet)
            {
				modifiers.FinalDamage *= 0.95f;
            }
			if (NPC.CountNPCS(NPCID.CultistBoss) > 0)
            {
                modifiers.FinalDamage *= 1.45f;
            }
			if (Main.pumpkinMoon && CalamityWorld.downedDoG)
            {
                modifiers.FinalDamage *= 2.5f;
            }
			if (Main.snowMoon && CalamityWorld.downedDoG)
            {
                modifiers.FinalDamage *= 2.5f;
            }
			if (Main.eclipse && CalamityWorld.downedYharon)
			{
				modifiers.FinalDamage *= 3.8f;
			}
			if (CalamityWorld.revenge)
			{
				float damageMult = CalamityWorld.downedBossAny ? 1.25f : 0.8f;
				modifiers.FinalDamage.Base = (int)((double)modifiers.FinalDamage.Flat * damageMult);
				double newDamage = (double)modifiers.FinalDamage.Flat - (double)Player.statDefense * (CalamityWorld.downedBossAny ? 1 : 0.75);
				double newDamageLimit = 5.0 + (Main.hardMode ? 10.0 : 0.0) + (NPC.downedPlantBoss ? 10.0 : 0.0) + (NPC.downedMoonlord ? 25.0 : 0.0); //5, 15, 25, 50
				if (newDamage < newDamageLimit)
				{
					newDamage = newDamageLimit;
				}
				modifiers.FinalDamage.Base = (int)newDamage;
			}
			if (raiderTalisman)
			{
                modifiers.FinalDamage += raiderStack / 4;
			}
			if (daedalusAbsorb && Main.rand.NextBool(10))
			{
				int healAmt = (int)(modifiers.FinalDamage.Flat / 2);
				Player.statLife += healAmt;
    			Player.HealEffect(healAmt);
			}
			if (absorber)
			{
				int healAmt = (int)(modifiers.FinalDamage.Flat / 16);
				Player.statLife += healAmt;
    			Player.HealEffect(healAmt);
			}
		}
		
		public override void OnHurt(Player.HurtInfo info)
		{
			modStealth = 1f;
			if (raiderTalisman && raiderStack < 100)
			{
				raiderStack++;
			}
			if (Player.whoAmI == Main.myPlayer && gShell && !Player.panic)
			{
				Player.AddBuff(Mod.Find<ModBuff>("ShellBoost").Type, 300);
			}
			if (Player.whoAmI == Main.myPlayer && xerocSet)
			{
				Player.AddBuff(Mod.Find<ModBuff>("XerocRage").Type, 240);
        		Player.AddBuff(Mod.Find<ModBuff>("XerocWrath").Type, 240);
			}
			else if (Player.whoAmI == Main.myPlayer && reaverBlast)
			{
				Player.AddBuff(Mod.Find<ModBuff>("ReaverRage").Type, 180);
			}
			if (fBarrier && Main.myPlayer == Player.whoAmI)
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
			if (aBrain && Main.myPlayer == Player.whoAmI)
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
				Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X + (float)Main.rand.Next(-40, 40), Player.Center.Y - (float)Main.rand.Next(20, 60), Player.velocity.X * 0.3f, Player.velocity.Y * 0.3f, 565, 0, 0f, Player.whoAmI, 0f, 0f);
			}
		}
		
		public override void PostHurt(Player.HurtInfo info)
		{
			bool hardMode = Main.hardMode;
			if (Player.whoAmI == Main.myPlayer && dAmulet)
			{
				if (info.Damage == 1.0)
				{
					Player.immuneTime += 15;
				}
				else
				{
					Player.immuneTime += 30;
				}
			}
			if (Player.whoAmI == Main.myPlayer && fabsolVodka)
			{
				if (info.Damage == 1.0)
				{
					Player.immuneTime += 5;
				}
				else
				{
					Player.immuneTime += 15;
				}
			}
			if (Player.whoAmI == Main.myPlayer)
			{
				if (aBulwark)
				{
					for (int n = 0; n < 4; n++)
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
						int num17 = Projectile.NewProjectile(Player.GetSource_FromThis(), x, y, num13, num14, Mod.Find<ModProjectile>("AstralStar").Type, 320, 5f, Player.whoAmI, 0f, 0f);
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
						int num17 = Projectile.NewProjectile(Player.GetSource_FromThis(), x, y, num13, num14, 92, 130, 4f, Player.whoAmI, 0f, 0f);
						Main.projectile[num17].usesLocalNPCImmunity = true;
						Main.projectile[num17].localNPCHitCooldown = 5;
						Main.projectile[num17].DamageType = DamageClass.Default;
					}
				}
			}
			if (ataxiaBlaze && Main.rand.NextBool(5))
			{
				if (info.Damage > 0)
				{
					SoundEngine.PlaySound(SoundID.Item74, Player.position);
					int eDamage = 100;
					if (Player.whoAmI == Main.myPlayer)
					{
						Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ChaosBlaze").Type, eDamage, 1f, Player.whoAmI, 0f, 0f);
					}
				}
			}
			if (daedalusShard)
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
							Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f) + randomSpeed, 90, sDamage, 1f, Player.whoAmI, 0f, 0f);
							Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ) + randomSpeed2, 90, sDamage, 1f, Player.whoAmI, 0f, 0f);
						}
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
							float xPos = Main.rand.NextBool(2)? Player.Center.X + 100 : Player.Center.X - 100;
    						Vector2 vector2 = new Vector2(xPos, Player.Center.Y + Main.rand.Next(-100, 101));
							offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
							int spore1 = Projectile.NewProjectile(Player.GetSource_FromThis(), vector2.X, vector2.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), 590, fDamage, 1.25f, Player.whoAmI, 0f, 0f);
							int spore2 = Projectile.NewProjectile(Player.GetSource_FromThis(), vector2.X, vector2.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), 590, fDamage, 1.25f, Player.whoAmI, 0f, 0f);
							Main.projectile[spore1].timeLeft = 120;
							Main.projectile[spore2].timeLeft = 120;
						}
					}
				}
			}
			if (reaverSpore)
			{
				if (info.Damage > 0)
				{
					SoundEngine.PlaySound(SoundID.NPCHit1, Player.position);
					float spread = 45f * 0.0174f;
					double startAngle = Math.Atan2(Player.velocity.X, Player.velocity.Y) - spread / 2;
					double deltaAngle = spread / 8f;
					double offsetAngle;
					int i;
					int rDamage = 38;
					if (Player.whoAmI == Main.myPlayer)
					{
						for (i = 0; i < 4; i++) 
						{
							float xPos = Main.rand.NextBool(2)? Player.Center.X + 100 : Player.Center.X - 100;
    						Vector2 vector2 = new Vector2(xPos, Player.Center.Y + Main.rand.Next(-100, 101));
							offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
							int spore1 = Projectile.NewProjectile(Player.GetSource_FromThis(), vector2.X, vector2.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), 567, rDamage, 2f, Player.whoAmI, 0f, 0f);
							int spore2 = Projectile.NewProjectile(Player.GetSource_FromThis(), vector2.X, vector2.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), 568, rDamage, 2f, Player.whoAmI, 0f, 0f);
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
					int sDamage = hardMode ? 40 : 10;
					if (Player.whoAmI == Main.myPlayer)
					{
						for (i = 0; i < 4; i++) 
						{
							offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
							int spark1 = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("Spark").Type, sDamage, 1.25f, Player.whoAmI, 0f, 0f);
							int spark2 = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("Spark").Type, sDamage, 1.25f, Player.whoAmI, 0f, 0f);
							Main.projectile[spark1].timeLeft = 120;
							Main.projectile[spark2].timeLeft = 120;
						}
					}
				}
			}
			if (godSlayerDamage)
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
							Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("GodKiller").Type, 300, 5f, Player.whoAmI, 0f, 0f);
							Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("GodKiller").Type, 300, 5f, Player.whoAmI, 0f, 0f);
						}
					}
				}
			}
			if (dsSetBonus)
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
						int num19 = Projectile.NewProjectile(Player.GetSource_FromThis(), x, y, num15, num16, 294, 3000, 7f, Player.whoAmI, 0f, 0f);
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
						int num19 = Projectile.NewProjectile(Player.GetSource_FromThis(), x, y, num15, num16, 45, 5000, 7f, Player.whoAmI, 0f, 0f);
						Main.projectile[num19].ai[1] = Player.position.Y;
					}
				}
			}
		}
		
		public void ModDashMovement()
		{
			if (dashMod == 4 && dashDelayMod < 0 && Player.whoAmI == Main.myPlayer)
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
							float num = 1500f * Player.GetDamage(DamageClass.Melee).Flat;
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
								int num6 = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyExplosionSupreme").Type, 1000, 20f, Main.myPlayer, 0f, 0f);
								Main.projectile[num6].Kill();
								Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyEruption").Type, 780, 5f, Main.myPlayer, 0f, 0f);
							}
							nPC.immune[Player.whoAmI] = 6;
							Player.immune = true;
							Player.immuneNoBlink = true;
							Player.immuneTime = 4;
						}
					}
				}
			}
			if (dashMod == 3 && dashDelayMod < 0 && Player.whoAmI == Main.myPlayer)
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
							float num = 500f * Player.GetDamage(DamageClass.Melee).Flat;
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
								int num6 = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyExplosionSupreme").Type, 500, 15f, Main.myPlayer, 0f, 0f);
								Main.projectile[num6].Kill();
								Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyEruption").Type, 380, 5f, Main.myPlayer, 0f, 0f);
							}
							nPC.immune[Player.whoAmI] = 6;
							Player.immune = true;
							Player.immuneNoBlink = true;
							Player.immuneTime = 4;
						}
					}
				}
			}
			if (dashMod == 2 && dashDelayMod < 0 && Player.whoAmI == Main.myPlayer)
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
							float num = 100f * Player.GetDamage(DamageClass.Melee).Flat;
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
								int num6 = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("HolyExplosion").Type, 100, 15f, Main.myPlayer, 0f, 0f);
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
			if (dashMod == 1 && dashDelayMod < 0 && Player.whoAmI == Main.myPlayer)
			{
				Rectangle rectangle = new Rectangle((int)((double)Player.position.X + (double)Player.velocity.X * 0.5 - 4.0), (int)((double)Player.position.Y + (double)Player.velocity.Y * 0.5 - 4.0), Player.width + 8, Player.height + 8);
				for (int i = 0; i < 200; i++)
				{
					if ((Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].immune[Player.whoAmI] <= 0 && Main.npc[i].damage > 0) ||
					    (Main.projectile[i].active && !Main.projectile[i].friendly && Main.projectile[i].hostile && Main.projectile[i].damage > 0))
					{
						NPC nPC = Main.npc[i];
						Projectile proj = Main.projectile[i];
						Rectangle rect = nPC.getRect();
						Rectangle rect2 = proj.getRect();
						if ((rectangle.Intersects(rect) && (nPC.noTileCollide || Player.CanHit(nPC))) || (rectangle.Intersects(rect2)))
						{
							if (Player.whoAmI == Main.myPlayer && dodgeScarf && !scarfCooldown)
							{
								Player.AddBuff(Mod.Find<ModBuff>("ScarfMeleeBoost").Type, Main.rand.Next(480, 561));
								Player.AddBuff(Mod.Find<ModBuff>("ScarfCooldown").Type, 480);
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
									int num = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
									Dust expr_A4_cp_0 = Main.dust[num];
									expr_A4_cp_0.position.X = expr_A4_cp_0.position.X + (float)Main.rand.Next(-20, 21);
									Dust expr_CB_cp_0 = Main.dust[num];
									expr_CB_cp_0.position.Y = expr_CB_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
									Main.dust[num].velocity *= 0.4f;
									Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
									Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
									if (Main.rand.NextBool(2))
									{
										Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
										Main.dust[num].noGravity = true;
									}
								}
								if (Player.whoAmI == Main.myPlayer)
								{
									NetMessage.SendData(MessageID.Dodge, -1, -1, null, Player.whoAmI, 1f, 0f, 0f, 0, 0, 0);
								}
							}
						}
					}
				}
			}
			if (dashDelayMod > 0)
			{
				if (Player.eocDash > 0)
				{
					Player.eocDash--;
				}
				if (Player.eocDash == 0)
				{
					Player.eocHit = -1;
				}
				dashDelayMod--;
				return;
			}
			if (dashDelayMod < 0)
			{
				float num7 = 12f;
				float num8 = 0.992f;
				float num9 = Math.Max(Player.accRunSpeed, Player.maxRunSpeed);
				float num10 = 0.96f;
				int num11 = 20;
				if (dashMod == 1)
				{
					for (int k = 0; k < 2; k++)
					{
						int num12;
						if (Player.velocity.Y == 0f)
						{
							num12 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + (float)Player.height - 4f), Player.width, 8, DustID.LifeDrain, 0f, 0f, 100, default(Color), 1.4f);
						}
						else
						{
							num12 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + (float)(Player.height / 2) - 8f), Player.width, 16, DustID.LifeDrain, 0f, 0f, 100, default(Color), 1.4f);
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
						int num14 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 4f), Player.width, Player.height - 8, DustID.GoldCoin, 0f, 0f, 100, default(Color), 2.75f);
						Main.dust[num14].velocity *= 0.1f;
						Main.dust[num14].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num14].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
						Main.dust[num14].noGravity = true;
						if (Main.rand.NextBool(2))
						{
							Main.dust[num14].fadeIn = 0.5f;
						}
					}
					num7 = 14f;
					num8 = 0.985f;
					num10 = 0.94f;
					num11 = 20;
				}
				else if (dashMod == 3)
				{
					for (int m = 0; m < 12; m++)
					{
						int num14 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 4f), Player.width, Player.height - 8, DustID.CopperCoin, 0f, 0f, 100, default(Color), 2.75f);
						Main.dust[num14].velocity *= 0.1f;
						Main.dust[num14].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num14].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
						Main.dust[num14].noGravity = true;
						if (Main.rand.NextBool(2))
						{
							Main.dust[num14].fadeIn = 0.5f;
						}
					}
					num7 = 16f;
					num8 = 0.98f;
					num10 = 0.92f;
					num11 = 20;
				}
				else if (dashMod == 4)
				{
					for (int m = 0; m < 24; m++)
					{
						int num14 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 4f), Player.width, Player.height - 8, DustID.CopperCoin, 0f, 0f, 100, default(Color), 2.75f);
						Main.dust[num14].velocity *= 0.1f;
						Main.dust[num14].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
						Main.dust[num14].shader = GameShaders.Armor.GetSecondaryShader(Player.ArmorSetDye(), Player);
						Main.dust[num14].noGravity = true;
						if (Main.rand.NextBool(2))
						{
							Main.dust[num14].fadeIn = 0.5f;
						}
					}
					num7 = 18f;
					num8 = 0.976f;
					num10 = 0.9f;
					num11 = 20;
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
					dashDelayMod = num11;
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
						Player.velocity.X = 16.9f * (float)num16;
						Point point = (Player.Center + new Vector2((float)(num16 * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
						Point point2 = (Player.Center + new Vector2((float)(num16 * Player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
						{
							Player.velocity.X = Player.velocity.X / 2f;
						}
						dashDelayMod = -1;
						for (int num17 = 0; num17 < 20; num17++)
						{
							int num18 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
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
						Player.velocity.X = 21.9f * (float)num23;
						Point point5 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
						Point point6 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point5.X, point5.Y) || WorldGen.SolidOrSlopedTile(point6.X, point6.Y))
						{
							Player.velocity.X = Player.velocity.X / 2f;
						}
						dashDelayMod = -1;
						for (int num24 = 0; num24 < 20; num24++)
						{
							int num25 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, DustID.GoldCoin, 0f, 0f, 100, default(Color), 3f);
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
						Player.velocity.X = 26.9f * (float)num23;
						Point point5 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
						Point point6 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point5.X, point5.Y) || WorldGen.SolidOrSlopedTile(point6.X, point6.Y))
						{
							Player.velocity.X = Player.velocity.X / 2f;
						}
						dashDelayMod = -1;
						for (int num24 = 0; num24 < 40; num24++)
						{
							int num25 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 3f);
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
						Player.velocity.X = 31.9f * (float)num23;
						Point point5 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
						Point point6 = (Player.Center + new Vector2((float)(num23 * Player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point5.X, point5.Y) || WorldGen.SolidOrSlopedTile(point6.X, point6.Y))
						{
							Player.velocity.X = Player.velocity.X / 2f;
						}
						dashDelayMod = -1;
						for (int num24 = 0; num24 < 60; num24++)
						{
							int num25 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, DustID.CopperCoin, 0f, 0f, 100, default(Color), 3f);
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
		
		public void ModHorizontalMovement()
		{
			float num = (Player.accRunSpeed + Player.maxRunSpeed) / 2f;
			if (Player.controlLeft && Player.velocity.X > -Player.accRunSpeed && dashDelayMod >= 0)
			{
				if (Player.mount.Active && Player.mount.Cart)
				{
					if (Player.velocity.X < 0f)
					{
						Player.direction = -1;
					}
				}
				else if ((Player.itemAnimation == 0 || Player.inventory[Player.selectedItem].useTurn) && Player.mount.AllowDirectionChange)
				{
					Player.direction = -1;
				}
				if (Player.velocity.Y == 0f || Player.wingsLogic > 0 || Player.mount.CanFly())
				{
					if (Player.velocity.X > Player.runSlowdown)
					{
						Player.velocity.X = Player.velocity.X - Player.runSlowdown;
					}
					Player.velocity.X = Player.velocity.X - Player.runAcceleration * 0.2f;
					if (Player.wingsLogic > 0)
					{
						Player.velocity.X = Player.velocity.X - Player.runAcceleration * 0.2f;
					}
				}
				if (Player.onWrongGround)
				{
					if (Player.velocity.X < Player.runSlowdown)
					{
						Player.velocity.X = Player.velocity.X + Player.runSlowdown;
					}
					else
					{
						Player.velocity.X = 0f;
					}
				}
				if (Player.velocity.X < -num && Player.velocity.Y == 0f && !Player.mount.Active)
				{
					int num3 = 0;
					if (Player.gravDir == -1f)
					{
						num3 -= Player.height;
					}
					if (dashMod == 1)
					{
						int num7 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num3), Player.width + 8, 4, DustID.LifeDrain, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 1.5f);
						Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
						Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
						Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 2)
					{
						int num7 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num3), Player.width + 8, 4, DustID.GoldCoin, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 2.5f);
						Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
						Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
						Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 3)
					{
						int num7 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num3), Player.width + 8, 4, DustID.CopperCoin, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 3f);
						Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
						Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
						Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 4)
					{
						int num7 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num3), Player.width + 8, 4, DustID.CopperCoin, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 3f);
						Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
						Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
						Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
				}
			}
			else if (Player.controlRight && Player.velocity.X < Player.accRunSpeed && dashDelayMod >= 0)
			{
				if (Player.mount.Active && Player.mount.Cart)
				{
					if (Player.velocity.X > 0f)
					{
						Player.direction = -1;
					}
				}
				else if ((Player.itemAnimation == 0 || Player.inventory[Player.selectedItem].useTurn) && Player.mount.AllowDirectionChange)
				{
					Player.direction = 1;
				}
				if (Player.velocity.Y == 0f || Player.wingsLogic > 0 || Player.mount.CanFly())
				{
					if (Player.velocity.X < -Player.runSlowdown)
					{
						Player.velocity.X = Player.velocity.X + Player.runSlowdown;
					}
					Player.velocity.X = Player.velocity.X + Player.runAcceleration * 0.2f;
					if (Player.wingsLogic > 0)
					{
						Player.velocity.X = Player.velocity.X + Player.runAcceleration * 0.2f;
					}
				}
				if (Player.onWrongGround)
				{
					if (Player.velocity.X > Player.runSlowdown)
					{
						Player.velocity.X = Player.velocity.X - Player.runSlowdown;
					}
					else
					{
						Player.velocity.X = 0f;
					}
				}
				if (Player.velocity.X > num && Player.velocity.Y == 0f && !Player.mount.Active)
				{
					int num8 = 0;
					if (Player.gravDir == -1f)
					{
						num8 -= Player.height;
					}
					if (dashMod == 1)
					{
						int num12 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num8), Player.width + 8, 4, DustID.LifeDrain, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 1.5f);
						Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
						Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 2)
					{
						int num12 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num8), Player.width + 8, 4, DustID.GoldCoin, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 2.5f);
						Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
						Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 3)
					{
						int num12 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num8), Player.width + 8, 4, DustID.CopperCoin, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 3f);
						Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
						Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
					else if (dashMod == 4)
					{
						int num12 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num8), Player.width + 8, 4, DustID.CopperCoin, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 3f);
						Main.dust[num12].velocity.X = Main.dust[num12].velocity.X * 0.2f;
						Main.dust[num12].velocity.Y = Main.dust[num12].velocity.Y * 0.2f;
						Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
					}
				}
			}
		}
		
		public void ModKeyDoubleTap(int keyDir)
		{
			int num = 0;
			if (Main.ReversedUpDownArmorSetBonuses)
			{
				num = 1;
			}
			if (keyDir == num)
			{
				if (elysianAegis && !Player.mount.Active)
				{
					elysianGuard = !elysianGuard;
				}
			}
		}

		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (tRegen)
			{
				if (Main.rand.NextBool(100))
				{
					Vector2 value = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
					value.Normalize();
					value.X *= 0.66f;
					int num36 = Gore.NewGore(Player.GetSource_FromThis(), drawInfo.Position + new Vector2((float)Main.rand.Next(Player.width + 1), (float)Main.rand.Next(Player.height + 1)), value * (float)Main.rand.Next(3, 6) * 0.33f, Mod.Find<ModGore>("TarraHeart").Type, (float)Main.rand.Next(40, 121) * 0.01f);
					Main.gore[num36].sticky = false;
					Main.gore[num36].velocity *= 0.4f;
					Main.gore[num36].timeLeft = 50;
					Gore expr_2481_cp_0 = Main.gore[num36];
					expr_2481_cp_0.velocity.Y = expr_2481_cp_0.velocity.Y - 0.6f;
                    drawInfo.GoreCache.Add(num36);
				}
				r *= 0.025f;
				g *= 0.15f;
				b *= 0.035f;
				fullBright = true;
			}
			if (IBoots)
			{
				if (((double)Math.Abs(Player.velocity.X) > 0.05 || (double)Math.Abs(Player.velocity.Y) > 0.05) && !Player.mount.Active)
				{	
					if (Main.rand.NextBool(2)&& drawInfo.shadow == 0f)
					{
						int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.Vortex, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1f);
						Main.dust[dust].noGravity = true;
						Main.dust[dust].velocity *= 2f;
						Main.dust[dust].velocity.Y *= 1.5f;
						drawInfo.DustCache.Add(dust);
					}
					r *= 0.05f;
					g *= 0.05f;
					b *= 0.05f;
					fullBright = true;
				}
			}
			if (elysianFire)
			{
				if (((double)Math.Abs(Player.velocity.X) > 0.05 || (double)Math.Abs(Player.velocity.Y) > 0.05) && !Player.mount.Active)
				{	
					if (Main.rand.NextBool(2)&& drawInfo.shadow == 0f)
					{
						int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.GoldCoin, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1f);
						Main.dust[dust].noGravity = true;
						Main.dust[dust].velocity *= 2f;
						Main.dust[dust].velocity.Y *= 1.5f;
                        drawInfo.DustCache.Add(dust);
					}
					r *= 0.75f;
					g *= 0.55f;
					b *= 0f;
					fullBright = true;
				}
			}
			if (dsSetBonus)
			{
				if (((double)Math.Abs(Player.velocity.X) > 0.05 || (double)Math.Abs(Player.velocity.Y) > 0.05) && !Player.mount.Active)
				{	
					if (Main.rand.NextBool(2)&& drawInfo.shadow == 0f)
					{
						int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.Shadowflame, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1.5f);
						Main.dust[dust].noGravity = true;
						Main.dust[dust].velocity *= 1.2f;
						Main.dust[dust].velocity.Y -= 0.15f;
                        drawInfo.DustCache.Add(dust);
					}
					r *= 0.15f;
					g *= 0.025f;
					b *= 0.1f;
					fullBright = true;
				}
			}
			if (auricSet)
			{
				if (((double)Math.Abs(Player.velocity.X) > 0.05 || (double)Math.Abs(Player.velocity.Y) > 0.05) && !Player.mount.Active)
				{	
					if (drawInfo.shadow == 0f)
					{
						int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, Main.rand.NextBool(2)? 57 : 244, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1.5f);
						Main.dust[dust].noGravity = true;
						Main.dust[dust].velocity *= 1.2f;
                        drawInfo.DustCache.Add(dust);
					}
					r *= 0.15f;
					g *= 0.025f;
					b *= 0.1f;
					fullBright = true;
				}
			}
			if (bFlames || aFlames)
			{
				if (Main.rand.NextBool(4)&& drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, Mod.Find<ModDust>("BrimstoneFlame").Type, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
                    drawInfo.DustCache.Add(dust);
				}
				r *= 0.25f;
				g *= 0.01f;
				b *= 0.01f;
				fullBright = true;
			}
			if (gsInferno)
			{
				if (Main.rand.NextBool(4)&& drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.ShadowbeamStaff, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
                    drawInfo.DustCache.Add(dust);
				}
				r *= 0.25f;
				g *= 0.01f;
				b *= 0.01f;
				fullBright = true;
			}
			if (hFlames)
			{
				if (Main.rand.NextBool(4)&& drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, Mod.Find<ModDust>("HolyFlame").Type, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
                    drawInfo.DustCache.Add(dust);
				}
				r *= 0.25f;
				g *= 0.25f;
				b *= 0.1f;
				fullBright = true;
			}
			if (pFlames)
			{
				if (Main.rand.NextBool(4)&& drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.GemEmerald, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.2f;
					Main.dust[dust].velocity.Y -= 0.15f;
                    drawInfo.DustCache.Add(dust);
				}
				r *= 0.07f;
				g *= 0.15f;
				b *= 0.01f;
				fullBright = true;
			}
			if (gState)
			{
				r *= 0f;
				g *= 0.05f;
				b *= 0.3f;
				fullBright = true;
			}
			if (bBlood)
			{
				if (Main.rand.NextBool(6)&& drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.Blood, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
                    drawInfo.DustCache.Add(dust);
				}
				r *= 0.15f;
				g *= 0.01f;
				b *= 0.01f;
				fullBright = true;
			}
			if (mushy)
			{
				if (Main.rand.NextBool(6)&& drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.BlueFairy, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 2f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 0.5f;
					Main.dust[dust].velocity.Y -= 0.1f;
                    drawInfo.DustCache.Add(dust);
				}
				r *= 0.15f;
				g *= 0.01f;
				b *= 0.01f;
				fullBright = true;
			}
		}
	}
}
