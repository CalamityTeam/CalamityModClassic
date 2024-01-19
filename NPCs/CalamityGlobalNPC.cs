using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Accessories;
using CalamityModClassic1Point2.NPCs.DesertScourge;
using CalamityModClassic1Point2.Items.DesertScourge;
using CalamityModClassic1Point2.Items.Placeables;
using CalamityModClassic1Point2.Items.Armor;
using CalamityModClassic1Point2.Items.Weapons.DesertScourge;
using CalamityModClassic1Point2.Items.Weapons;
using CalamityModClassic1Point2.NPCs.AstrumDeus;
using CalamityModClassic1Point2.Items;
using CalamityModClassic1Point2.NPCs.TheDevourerofGods;
using CalamityModClassic1Point2.Items.TheDevourerofGods;
using CalamityModClassic1Point2.Items.Weapons.DevourerofGods;
using CalamityModClassic1Point2.NPCs.Leviathan;

namespace CalamityModClassic1Point2.NPCs
{
	public class CalamityGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		public float protection;

		public float defProtection;

		public bool marked = false;

		public bool irradiated = false;

		public bool bFlames = false;

		public bool hFlames = false;

		public bool pFlames = false;

		public bool gState = false;

		public bool aCrunch = false;

		public bool tSad = false;

		public bool pShred = false;

		public bool cDepth = false;

		public bool gsInferno = false;

		public bool aFlames = false;

		public bool eFreeze = false;

		public bool wDeath = false;

		public bool nightwither = false;

		public static int maxAIMod = 4;

		public float[] newAI = new float[maxAIMod];

		public int CultProjectiles = 2;

		public const float CultAngleSpread = 170;

		public int CultCountdown = 0;

		public static int holyBoss = -1;

		public static int doughnutBoss = -1;

		public static int voidBoss = -1;

		public static int energyFlame = -1;

		public static int hiveMind = -1;

		public static int hiveMind2 = -1;

		public static int scavenger = -1;

		public static int supremeCalamitas = -1;

		public static int ghostBoss = -1;

		public int bloodMoonKillCount = 0;

		public bool lacerator = false;

		public override void ResetEffects(NPC npc)
		{
			marked = false;
			irradiated = false;
			bFlames = false;
			hFlames = false;
			pFlames = false;
			gState = false;
			aCrunch = false;
			tSad = false;
			pShred = false;
			cDepth = false;
			gsInferno = false;
			aFlames = false;
			eFreeze = false;
			wDeath = false;
			nightwither = false;
			lacerator = false;
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			bool hardMode = Main.hardMode;
			int npcDefense = npc.defense;
			npc.defense = npc.defDefense;
			if (Main.raining && NPC.downedMoonlord && npc.damage > 0 && !npc.boss && !npc.friendly && !npc.dontTakeDamage && (double)(npc.position.Y / 16f) < Main.worldSurface)
			{
				npc.AddBuff(Mod.Find<ModBuff>("Irradiated").Type, 2);
			}
			if (irradiated)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 20;
				if (damage < 4)
				{
					damage = 4;
				}
			}
			if (cDepth)
			{
				if (npcDefense < 0)
				{
					npcDefense = 0;
				}
				int depthDamage = hardMode ? 80 : 12;
				int calcDepthDamage = (depthDamage - npcDefense);
				if (calcDepthDamage <= 0)
				{
					calcDepthDamage = 0;
				}
				int calcDepthRegenDown = (calcDepthDamage * 5);
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= calcDepthRegenDown;
				if (damage < calcDepthDamage)
				{
					damage = calcDepthDamage;
				}
			}
			if (bFlames)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 40;
				if (damage < 8)
				{
					damage = 8;
				}
			}
			if (hFlames)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 50;
				if (damage < 10)
				{
					damage = 10;
				}
			}
			if (pFlames)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.defense -= 4;
				npc.lifeRegen -= 100;
				if (damage < 20)
				{
					damage = 20;
				}
			}
			if (wDeath)
			{
				npc.defense -= 50;
			}
			if (gsInferno)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.defense -= 20;
				npc.lifeRegen -= 250;
				if (damage < 50)
				{
					damage = 50;
				}
			}
			if (aFlames)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.defense -= 10;
				npc.lifeRegen -= 125;
				if (damage < 25)
				{
					damage = 25;
				}
			}
			if (pShred)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 1500;
				if (damage < 300)
				{
					damage = 300;
				}
			}
			if (gState)
			{
				npc.defense /= 2;
				npc.velocity.Y = 0f;
				npc.velocity.X = 0f;
			}
			if (eFreeze)
			{
				npc.velocity.Y = 0f;
				npc.velocity.X = 0f;
			}
			if (tSad)
			{
				npc.damage -= 15;
				npc.velocity.Y /= 2;
				npc.velocity.X /= 2;
			}
			if (aCrunch)
			{
				npc.defense /= 3;
			}
			if (nightwither)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 200;
				if (damage < 40)
				{
					damage = 40;
				}
			}
		}

		public override void SetDefaults(NPC npc)
		{
			for (int m = 0; m < maxAIMod; m++)
			{
				this.newAI[m] = 0f;
			}
			if (Main.hardMode && !CalamityWorld.spawnedHardBoss && !npc.boss && !npc.friendly && !npc.dontTakeDamage &&
				npc.type != NPCID.TheDestroyerBody && npc.type != NPCID.TheDestroyerTail && npc.type != NPCID.Probe && npc.type != NPCID.PrimeCannon &&
				npc.type != NPCID.PrimeSaw && npc.type != NPCID.PrimeVice && npc.type != NPCID.PrimeLaser && npc.type != NPCID.TheHungry &&
				npc.type != NPCID.TheHungryII && npc.type != NPCID.WallofFleshEye && npc.type != NPCID.Creeper && npc.type != NPCID.EaterofWorldsHead &&
				npc.type != NPCID.EaterofWorldsBody && npc.type != NPCID.EaterofWorldsTail && npc.type != NPCID.SkeletronHand && npc.type != Mod.Find<ModNPC>("CryogenIce").Type &&
				npc.type != Mod.Find<ModNPC>("Cryocore").Type && npc.type != Mod.Find<ModNPC>("Cryocore2").Type && npc.type != Mod.Find<ModNPC>("IceMass").Type &&
				npc.type != Mod.Find<ModNPC>("SlimeSpawnCorrupt").Type && npc.type != Mod.Find<ModNPC>("SlimeSpawnCorrupt2").Type &&
				npc.type != Mod.Find<ModNPC>("SlimeSpawnCrimson").Type && npc.type != Mod.Find<ModNPC>("SlimeSpawnCrimson2").Type && npc.type != Mod.Find<ModNPC>("CrabulonIdle").Type)
			{
				double multiplier = Main.expertMode ? 0.6 : 0.8;
				npc.lifeMax = (int)((double)npc.lifeMax * multiplier);
				npc.damage = (int)((double)npc.damage * multiplier);
			}
			if (CalamityWorld.revenge)
			{
				npc.value = (float)((int)((double)npc.value * 5));
				if (npc.type == NPCID.MoonLordFreeEye)
				{
					npc.dontTakeDamage = false;
					npc.lifeMax = (int)((double)npc.lifeMax * 250);
				}
			}
			if (npc.type == NPCID.CultistBoss)
			{
				npc.lifeMax = (int)((double)npc.lifeMax * (CalamityWorld.revenge ? 3 : 2));
			}
			if (CalamityWorld.revenge)
			{
				if (npc.type == NPCID.DukeFishron)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 1.5);
				}
				if (npc.type == NPCID.Golem)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 3);
				}
				if (npc.type == NPCID.Plantera)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 1.4);
				}
				if (npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 1.3);
				}
				if (npc.type == NPCID.SkeletronPrime || npc.type == NPCID.PrimeVice || npc.type == NPCID.PrimeCannon || npc.type == NPCID.PrimeSaw || npc.type == NPCID.PrimeLaser)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 1.15);
				}
				if (npc.type == NPCID.Retinazer)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 1.1);
				}
				if (npc.type == NPCID.Spazmatism)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 1.45);
				}
				if (npc.type == NPCID.WallofFlesh || npc.type == NPCID.WallofFleshEye)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 1.3);
				}
				if (npc.type == NPCID.SkeletronHead)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 1.3);
				}
				if (npc.type == NPCID.SkeletronHand)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 1.15);
				}
				if (npc.type == NPCID.QueenBee)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 1.25);
				}
				if (npc.type == NPCID.BrainofCthulhu)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 1.5);
				}
				if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 1.25);
					npc.damage = (int)((double)npc.damage * 1.25);
				}
				if (npc.type == NPCID.EyeofCthulhu)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 1.5);
				}
				if (npc.type == NPCID.KingSlime)
				{
					npc.lifeMax = (int)((double)npc.lifeMax * 1.4);
				}
			}
			if (Main.raining && NPC.downedMoonlord && !npc.boss && !npc.friendly && !npc.dontTakeDamage && npc.lifeMax <= 2000)
			{
				npc.lifeMax = (int)((double)npc.lifeMax * 1.25);
				npc.damage = (int)((double)npc.damage * 1.25);
			}
			if (Main.bloodMoon && NPC.downedMoonlord && !npc.boss && !npc.friendly && !npc.dontTakeDamage && npc.lifeMax <= 2000)
			{
				npc.lifeMax = (int)((double)npc.lifeMax * 7);
				npc.damage = (int)((double)npc.damage * 2);
			}
			if (Main.pumpkinMoon && CalamityWorld.downedDoG && !npc.friendly && !npc.dontTakeDamage)
			{
				npc.lifeMax = (int)((double)npc.lifeMax * 15);
			}
			if (Main.snowMoon && CalamityWorld.downedDoG && !npc.friendly && !npc.dontTakeDamage)
			{
				npc.lifeMax = (int)((double)npc.lifeMax * 15);
			}
			if (Main.eclipse && CalamityWorld.downedYharon && !npc.boss && !npc.friendly && !npc.dontTakeDamage)
			{
				npc.lifeMax = (int)((double)npc.lifeMax * 65);
			}
			if (CalamityWorld.downedProvidence && !npc.boss && !npc.friendly && !npc.dontTakeDamage && (npc.type == NPCID.SkeletonSniper || npc.type == NPCID.TacticalSkeleton || npc.type == NPCID.SkeletonCommando || npc.type == NPCID.Paladin ||
			   npc.type == NPCID.GiantCursedSkull || npc.type == NPCID.BoneLee || npc.type == NPCID.DiabolistWhite || npc.type == NPCID.DiabolistRed ||
			   npc.type == NPCID.NecromancerArmored || npc.type == NPCID.Necromancer || npc.type == NPCID.RaggedCasterOpenCoat || npc.type == NPCID.RaggedCaster ||
			   npc.type == NPCID.HellArmoredBonesSword || npc.type == NPCID.HellArmoredBonesMace || npc.type == NPCID.HellArmoredBonesSpikeShield ||
			   npc.type == NPCID.HellArmoredBones || npc.type == NPCID.BlueArmoredBonesSword || npc.type == NPCID.BlueArmoredBonesNoPants ||
			   npc.type == NPCID.BlueArmoredBonesMace || npc.type == NPCID.BlueArmoredBones || npc.type == NPCID.RustyArmoredBonesSwordNoArmor ||
			   npc.type == NPCID.RustyArmoredBonesSword || npc.type == NPCID.RustyArmoredBonesFlail || npc.type == NPCID.RustyArmoredBonesAxe))
			{
				npc.lifeMax = (int)((double)npc.lifeMax * 4);
				npc.damage = (int)((double)npc.damage * 2);
			}
			if (npc.type == NPCID.EyeofCthulhu ||
				npc.type == NPCID.EaterofWorldsHead ||
				npc.type == NPCID.EaterofWorldsBody ||
				npc.type == NPCID.EaterofWorldsTail ||
				npc.type == NPCID.BrainofCthulhu ||
				npc.type == NPCID.Creeper ||
				npc.type == NPCID.SkeletronHead ||
				npc.type == NPCID.SkeletronHand ||
				npc.type == NPCID.QueenBee ||
				npc.type == NPCID.KingSlime ||
				npc.type == NPCID.WallofFlesh ||
				npc.type == NPCID.WallofFleshEye ||
				npc.type == NPCID.TheHungry ||
				npc.type == NPCID.TheHungryII ||
				npc.type == NPCID.TheDestroyer ||
			   	npc.type == NPCID.TheDestroyerBody ||
			  	npc.type == NPCID.TheDestroyerTail ||
			  	npc.type == NPCID.Retinazer ||
			 	npc.type == NPCID.Spazmatism ||
			 	npc.type == NPCID.SkeletronPrime ||
				npc.type == NPCID.PrimeCannon ||
				npc.type == NPCID.PrimeSaw ||
				npc.type == NPCID.PrimeVice ||
				npc.type == NPCID.PrimeLaser ||
				npc.type == NPCID.Plantera ||
				npc.type == NPCID.PlanterasTentacle ||
				npc.type == NPCID.Golem ||
				npc.type == NPCID.GolemHead ||
				npc.type == NPCID.GolemFistLeft ||
				npc.type == NPCID.GolemFistRight ||
				npc.type == NPCID.DukeFishron ||
				npc.type == NPCID.CultistBoss ||
				npc.type == NPCID.MoonLordHead ||
				npc.type == NPCID.MoonLordHand ||
				npc.type == NPCID.MoonLordCore)
			{
				npc.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
				npc.buffImmune[Mod.Find<ModBuff>("TemporalSadness").Type] = true;
				npc.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = true;
			}
			if (npc.type == NPCID.EyeofCthulhu)
			{
				this.protection = 0.15f;
			}
			if (npc.type == NPCID.EaterofWorldsBody)
			{
				this.protection = 0.1f;
			}
			if (npc.type == NPCID.EaterofWorldsTail)
			{
				this.protection = 0.2f;
			}
			if (npc.type == NPCID.SkeletronHead)
			{
				this.protection = 0.25f;
			}
			if (npc.type == NPCID.SkeletronHand)
			{
				this.protection = 0.1f;
			}
			if (npc.type == NPCID.BoneSerpentHead)
			{
				this.protection = 0.1f;
			}
			if (npc.type == NPCID.BoneSerpentBody)
			{
				this.protection = 0.3f;
			}
			if (npc.type == NPCID.BoneSerpentTail)
			{
				this.protection = 0.5f;
			}
			if (npc.type == NPCID.KingSlime)
			{
				this.protection = 0.1f;
			}
			if (npc.type == NPCID.DungeonGuardian)
			{
				this.protection = 0.9999f;
			}
			if (npc.type == NPCID.Antlion)
			{
				this.protection = 0.15f;
			}
			if (npc.type == NPCID.ArmoredSkeleton)
			{
				this.protection = 0.35f;
			}
			if (npc.type == NPCID.Crab)
			{
				this.protection = 0.45f;
			}
			if (npc.type == NPCID.Mimic)
			{
				this.protection = 0.33f;
			}
			if (npc.type == NPCID.Werewolf)
			{
				this.protection = 0.25f;
			}
			if (npc.type == NPCID.SkeletonArcher)
			{
				this.protection = 0.25f;
			}
			if (npc.type == NPCID.WallofFlesh)
			{
				this.protection = 0.7f;
			}
			if (npc.type == NPCID.WallofFleshEye)
			{
				this.protection = 0.15f;
			}
			if (npc.type == NPCID.TheHungry)
			{
				this.protection = 0.15f;
			}
			if (npc.type == NPCID.Retinazer)
			{
				this.protection = 0.25f;
			}
			if (npc.type == NPCID.Spazmatism)
			{
				this.protection = 0.35f;
			}
			if (npc.type == NPCID.SkeletronPrime)
			{
				this.protection = 0.5f;
			}
			if (npc.type == NPCID.PrimeSaw)
			{
				this.protection = 0.3f;
			}
			if (npc.type == NPCID.PrimeVice)
			{
				this.protection = 0.3f;
			}
			if (npc.type == NPCID.PrimeCannon)
			{
				this.protection = 0.25f;
			}
			if (npc.type == NPCID.PrimeLaser)
			{
				this.protection = 0.25f;
			}
			if (npc.type == NPCID.TheDestroyer)
			{
				this.protection = 0.15f;
			}
			if (npc.type == NPCID.TheDestroyerBody)
			{
				this.protection = 0.25f;
			}
			if (npc.type == NPCID.TheDestroyerTail)
			{
				this.protection = 0.6f;
			}
			if (npc.type == NPCID.Probe)
			{
				this.protection = 0.3f;
			}
			if (npc.type == NPCID.PossessedArmor)
			{
				this.protection = 0.3f;
			}
			if (npc.type == NPCID.GiantTortoise)
			{
				this.protection = 0.4f;
			}
			if (npc.type == NPCID.IceTortoise)
			{
				this.protection = 0.4f;
			}
			if (npc.type == NPCID.Arapaima)
			{
				this.protection = 0.2f;
			}
			if (npc.type == NPCID.UndeadViking)
			{
				this.protection = 0.15f;
			}
			if (npc.type == NPCID.RuneWizard)
			{
				this.protection = 0.33f;
			}
			if (npc.type == NPCID.Derpling)
			{
				this.protection = 0.1f;
			}
			if (npc.type == NPCID.Nymph)
			{
				this.protection = 0.1f;
			}
			if (npc.type == NPCID.ArmoredViking)
			{
				this.protection = 0.45f;
			}
			if (npc.type == NPCID.PirateCaptain)
			{
				this.protection = 0.25f;
			}
			if (npc.type == NPCID.SeaSnail)
			{
				this.protection = 0.75f;
			}
			if (npc.type == NPCID.QueenBee)
			{
				this.protection = 0.15f;
			}
			if (npc.type == NPCID.IceGolem)
			{
				this.protection = 0.3f;
			}
			if (npc.type == NPCID.Golem)
			{
				this.protection = 0.66f;
			}
			if (npc.type == NPCID.Reaper)
			{
				this.protection = 0.2f;
			}
			if (npc.type == NPCID.AnomuraFungus)
			{
				this.protection = 0.33f;
			}
			if (npc.type == NPCID.Plantera)
			{
				this.protection = 0.3f;
			}
			if (npc.type == NPCID.BrainofCthulhu)
			{
				this.protection = 0.1f;
			}
			if (npc.type == NPCID.Creeper)
			{
				this.protection = 0.1f;
			}
			if (npc.type >= NPCID.RustyArmoredBonesAxe && npc.type <= NPCID.GiantCursedSkull)
			{
				this.protection = 0.25f;
			}
			if (npc.type >= NPCID.SkeletonSniper && npc.type <= NPCID.AngryBonesBigHelmet)
			{
				this.protection = 0.25f;
			}
			if (npc.type == NPCID.Paladin)
			{
				this.protection = 0.7f;
			}
			if (npc.type == NPCID.HeadlessHorseman)
			{
				this.protection = 0.1f;
			}
			if (npc.type == NPCID.MourningWood)
			{
				this.protection = 0.15f;
			}
			if (npc.type == NPCID.Pumpking)
			{
				this.protection = 0.25f;
			}
			if (npc.type == NPCID.PresentMimic)
			{
				this.protection = 0.5f;
			}
			if (npc.type == NPCID.Everscream)
			{
				this.protection = 0.15f;
			}
			if (npc.type == NPCID.IceQueen)
			{
				this.protection = 0.25f;
			}
			if (npc.type == NPCID.SantaNK1)
			{
				this.protection = 0.4f;
			}
			if (npc.type == NPCID.ElfCopter)
			{
				this.protection = 0.2f;
			}
			if (npc.type == NPCID.DukeFishron)
			{
				this.protection = 0.33f;
			}
			if (npc.type == NPCID.MartianTurret)
			{
				this.protection = 0.5f;
			}
			if (npc.type == NPCID.MartianDrone)
			{
				this.protection = 0.5f;
			}
			if (npc.type == NPCID.MartianSaucer)
			{
				this.protection = 0.5f;
			}
			if (npc.type == NPCID.MartianSaucerTurret)
			{
				this.protection = 0.25f;
			}
			if (npc.type == NPCID.MartianSaucerCannon)
			{
				this.protection = 0.25f;
			}
			if (npc.type == NPCID.MartianSaucerCore)
			{
				this.protection = 0.5f;
			}
			if (npc.type == NPCID.MoonLordHead)
			{
				this.protection = 0.33f;
			}
			if (npc.type == NPCID.MoonLordHand)
			{
				this.protection = 0.33f;
			}
			if (npc.type == NPCID.MoonLordCore)
			{
				this.protection = 0.5f;
			}
			if (npc.type == NPCID.MoonLordFreeEye)
			{
				this.protection = 0.33f;
			}
			if (npc.type == NPCID.CultistBoss)
			{
				this.protection = 0.33f;
			}
			if (npc.type == NPCID.DeadlySphere)
			{
				this.protection = 0.66f;
			}
			if (npc.type >= NPCID.BigMimicCorruption && npc.type <= NPCID.BigMimicJungle)
			{
				this.protection = 0.5f;
			}
			if (npc.type == NPCID.Mothron)
			{
				this.protection = CalamityWorld.downedDoG ? 0.5f : 0.33f;
			}
			if (npc.type == NPCID.MothronEgg)
			{
				this.protection = CalamityWorld.downedDoG ? 0.75f : 0.5f;
			}
			if (npc.type == NPCID.GreekSkeleton)
			{
				this.protection = 0.15f;
			}
			if (npc.type == NPCID.GraniteGolem)
			{
				this.protection = 0.2f;
			}
			if (npc.type == NPCID.GraniteFlyer)
			{
				this.protection = 0.15f;
			}
			if (npc.type == NPCID.PirateShipCannon)
			{
				this.protection = 0.5f;
			}
			if (npc.type >= NPCID.Crawdad && npc.type <= NPCID.GiantShelly2)
			{
				this.protection = 0.25f;
			}
			if (npc.type == NPCID.WalkingAntlion)
			{
				this.protection = 0.15f;
			}
			if (npc.type == NPCID.FlyingAntlion)
			{
				this.protection = 0.1f;
			}
			if (npc.type == NPCID.MartianWalker)
			{
				this.protection = 0.4f;
			}
			if (npc.type == NPCID.SandElemental)
			{
				this.protection = 0.2f;
			}
			if (npc.type == NPCID.DD2Betsy)
			{
				this.protection = 0.33f;
			}
			if (npc.type == NPCID.DD2OgreT2)
			{
				this.protection = 0.33f;
			}
			if (npc.type == NPCID.DD2OgreT3)
			{
				this.protection = 0.5f;
			}
			if (npc.type == Mod.Find<ModNPC>("ArmoredDiggerHead").Type)
			{
				this.protection = 0.3f;
			}
			if (npc.type == Mod.Find<ModNPC>("ArmoredDiggerBody").Type)
			{
				this.protection = 0.5f;
			}
			if (npc.type == Mod.Find<ModNPC>("ArmoredDiggerTail").Type)
			{
				this.protection = 0.7f;
			}
			if (npc.type == Mod.Find<ModNPC>("Astrageldon").Type)
			{
				this.protection = 0.15f;
			}
			if (npc.type == Mod.Find<ModNPC>("AstralProbe").Type)
			{
				this.protection = 0.3f;
			}
			if (npc.type == Mod.Find<ModNPC>("AstrumDeusHead").Type)
			{
				this.protection = 0.15f;
			}
			if (npc.type == Mod.Find<ModNPC>("AstrumDeusBody").Type)
			{
				this.protection = 0.25f;
			}
			if (npc.type == Mod.Find<ModNPC>("AstrumDeusTail").Type)
			{
				this.protection = 0.5f;
			}
			if (npc.type == Mod.Find<ModNPC>("AstrumDeusProbe3").Type)
			{
				this.protection = 0.33f;
			}
			if (npc.type == Mod.Find<ModNPC>("BrimstoneElemental").Type)
			{
				this.protection = 0.15f;
			}
			if (npc.type == Mod.Find<ModNPC>("Calamitas").Type)
			{
				this.protection = 0.3f;
			}
			if (npc.type == Mod.Find<ModNPC>("CalamitasRun").Type)
			{
				this.protection = 0.3f;
			}
			if (npc.type == Mod.Find<ModNPC>("CalamitasRun2").Type)
			{
				this.protection = 0.3f;
			}
			if (npc.type == Mod.Find<ModNPC>("CalamitasRun3").Type)
			{
				this.protection = 0.3f;
			}
			if (npc.type == Mod.Find<ModNPC>("SoulSeeker").Type)
			{
				this.protection = 0.25f;
			}
			if (npc.type == Mod.Find<ModNPC>("DespairStone").Type)
			{
				this.protection = 0.5f;
			}
			if (npc.type == Mod.Find<ModNPC>("SoulSlurper").Type)
			{
				this.protection = 0.3f;
			}
			if (npc.type == Mod.Find<ModNPC>("CeaselessVoid").Type)
			{
				this.protection = 0.9999f;
			}
			if (npc.type == Mod.Find<ModNPC>("Cnidrion").Type)
			{
				this.protection = 0.1f;
			}
			if (npc.type == Mod.Find<ModNPC>("CosmicWraith").Type)
			{
				this.protection = 0.25f;
			}
			if (npc.type == Mod.Find<ModNPC>("CosmicLantern").Type)
			{
				this.protection = 0.4f;
			}
			/*if (npc.type == Mod.Find<ModNPC>("Crabulon").Type) // the npc's name is "Crabulon" meaning this code doesn't work. It is not being fixed for authenticity :) _ YuH
			{
				this.protection = 0.2f;
			}*/
			if (npc.type == Mod.Find<ModNPC>("Cryogen").Type)
			{
				this.protection = 0.2f;
			}
			if (npc.type == Mod.Find<ModNPC>("CryogenIce").Type)
			{
				this.protection = 0.5f;
			}
			if (npc.type == Mod.Find<ModNPC>("DesertScourgeHead").Type)
			{
				this.protection = 0.05f;
			}
			if (npc.type == Mod.Find<ModNPC>("DesertScourgeBody").Type)
			{
				this.protection = 0.1f;
			}
			if (npc.type == Mod.Find<ModNPC>("DesertScourgeTail").Type)
			{
				this.protection = 0.25f;
			}
			if (npc.type == Mod.Find<ModNPC>("HiveMind").Type)
			{
				this.protection = 0.2f;
			}
			if (npc.type == Mod.Find<ModNPC>("HiveMindP2").Type)
			{
				this.protection = 0.1f;
			}
			if (npc.type == Mod.Find<ModNPC>("Horse").Type)
			{
				this.protection = 0.25f;
			}
			if (npc.type == Mod.Find<ModNPC>("Leviathan").Type)
			{
				this.protection = 0.6f;
			}
			if (npc.type == Mod.Find<ModNPC>("Siren").Type)
			{
				this.protection = 0.25f;
			}
			if (npc.type == Mod.Find<ModNPC>("SirenIce").Type)
			{
				this.protection = 0.7f;
			}
			if (npc.type == Mod.Find<ModNPC>("PhantomSpirit").Type)
			{
				this.protection = 0.4f;
			}
			if (npc.type == Mod.Find<ModNPC>("PerforatorHive").Type)
			{
				this.protection = 0.15f;
			}
			if (npc.type == Mod.Find<ModNPC>("PlaguebringerGoliath").Type)
			{
				this.protection = 0.4f;
			}
			if (npc.type == Mod.Find<ModNPC>("PlaguebringerShade").Type)
			{
				this.protection = 0.4f;
			}
			if (npc.type == Mod.Find<ModNPC>("ProfanedEnergyBody").Type)
			{
				this.protection = 0.5f;
			}
			if (npc.type == Mod.Find<ModNPC>("ProfanedGuardianBoss").Type)
			{
				this.protection = 0.6f;
			}
			if (npc.type == Mod.Find<ModNPC>("ProfanedGuardianBoss2").Type)
			{
				this.protection = 0.4f;
			}
			if (npc.type == Mod.Find<ModNPC>("ProfanedGuardianBoss3").Type)
			{
				this.protection = 0.2f;
			}
			if (npc.type == Mod.Find<ModNPC>("ProvSpawnOffense").Type)
			{
				this.protection = 0.33f;
			}
			if (npc.type == Mod.Find<ModNPC>("ProvSpawnDefense").Type)
			{
				this.protection = 0.66f;
			}
			if (npc.type == Mod.Find<ModNPC>("ProvSpawnHealer").Type)
			{
				this.protection = 0.25f;
			}
			if (npc.type == Mod.Find<ModNPC>("SandTortoise").Type)
			{
				this.protection = 0.4f;
			}
			if (npc.type == Mod.Find<ModNPC>("ScavengerBody").Type)
			{
				this.protection = 0.6f;
			}
			if (npc.type == Mod.Find<ModNPC>("ScavengerClawLeft").Type)
			{
				this.protection = 0.25f;
			}
			if (npc.type == Mod.Find<ModNPC>("ScavengerClawRight").Type)
			{
				this.protection = 0.25f;
			}
			if (npc.type == Mod.Find<ModNPC>("ScavengerLegLeft").Type)
			{
				this.protection = 0.3f;
			}
			if (npc.type == Mod.Find<ModNPC>("ScavengerLegRight").Type)
			{
				this.protection = 0.3f;
			}
			if (npc.type == Mod.Find<ModNPC>("ScavengerHead").Type)
			{
				this.protection = 0.25f;
			}
			if (npc.type == Mod.Find<ModNPC>("ScornEater").Type)
			{
				this.protection = 0.4f;
			}
			if (npc.type == Mod.Find<ModNPC>("ShockstormShuttle").Type)
			{
				this.protection = 0.33f;
			}
			if (npc.type == Mod.Find<ModNPC>("SlimeGod").Type)
			{
				this.protection = 0.1f;
			}
			if (npc.type == Mod.Find<ModNPC>("SlimeGodRun").Type)
			{
				this.protection = 0.1f;
			}
			if (npc.type == Mod.Find<ModNPC>("StasisProbe").Type)
			{
				this.protection = 0.4f;
			}
			if (npc.type == Mod.Find<ModNPC>("StasisProbeNaked").Type)
			{
				this.protection = 0.2f;
			}
			if (npc.type == Mod.Find<ModNPC>("SoulSeekerSupreme").Type)
			{
				this.protection = 0.5f;
			}
			if (npc.type == Mod.Find<ModNPC>("ThiccWaifu").Type)
			{
				this.protection = 0.33f;
			}
			this.defProtection = this.protection;
		}

		public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
		{
			if (Main.pumpkinMoon && CalamityWorld.downedDoG && !npc.boss && !npc.friendly && !npc.dontTakeDamage)
			{
				cooldownSlot = 1;
			}
			if (Main.snowMoon && CalamityWorld.downedDoG && !npc.boss && !npc.friendly && !npc.dontTakeDamage)
			{
				cooldownSlot = 1;
			}
			if (Main.eclipse && CalamityWorld.downedYharon && !npc.boss && !npc.friendly && !npc.dontTakeDamage)
			{
				cooldownSlot = 1;
			}
			return true;
		}

		public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
		{
			if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().beeResist)
			{
				if (npc.type == NPCID.GiantMossHornet || npc.type == NPCID.BigMossHornet || npc.type == NPCID.LittleMossHornet || npc.type == NPCID.TinyMossHornet ||
					npc.type == NPCID.MossHornet || npc.type == NPCID.VortexHornetQueen || npc.type == NPCID.VortexHornet || npc.type == NPCID.Bee ||
					npc.type == NPCID.BeeSmall || npc.type == NPCID.QueenBee || npc.type == Mod.Find<ModNPC>("PlaguebringerGoliath").Type || npc.type == Mod.Find<ModNPC>("PlaguebringerShade").Type ||
					npc.type == Mod.Find<ModNPC>("PlagueBeeLargeG").Type || npc.type == Mod.Find<ModNPC>("PlagueBeeG").Type || npc.type == Mod.Find<ModNPC>("PlagueBeeLarge").Type || npc.type == Mod.Find<ModNPC>("PlagueBee").Type)
				{
					modifiers.FinalDamage *= 0.5f;
				}
			}
		}

		public override void OnHitNPC(NPC npc, NPC target, NPC.HitInfo hit)
		{
			if (this.protection > 0f)
			{
				bool flag = Main.netMode == NetmodeID.SinglePlayer;
				if (!npc.active || npc.life <= 0)
				{
					return;
				}
				double newDamage = hit.Damage;
				int newDefense = npc.defense;
				if (marked)
				{
					protection *= 0.5f;
				}
				if (npc.ichor)
				{
					protection *= 0.75f;
					newDefense -= 20;
				}
				if (npc.betsysCurse)
				{
					protection *= 0.66f;
					newDefense -= 40;
				}
				if (newDefense < 0)
				{
					newDefense = 0;
				}
				if (protection < 0f)
				{
					protection = 0f;
				}
				newDamage = newDamage - (double)newDefense * 0.25;
				if (newDamage < 1.0)
				{
					newDamage = 1.0;
				}
				if (hit.Crit)
				{
					newDamage *= 2;
				}
				if (npc.takenDamageMultiplier > 1f)
				{
					newDamage *= (double)npc.takenDamageMultiplier;
				}
				if (newDamage >= 1.0)
				{
					newDamage = (double)((int)((double)(1f - this.protection) * newDamage));
					if (newDamage < 1.0)
					{
						newDamage = 1.0;
					}
					if (flag)
					{
						npc.PlayerInteraction(Main.myPlayer);
					}
					npc.justHit = true;
					if ((npc.type >= NPCID.RaggedCaster && npc.type <= NPCID.DiabolistWhite) || npc.type == NPCID.RuneWizard)
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							if (npc.type == NPCID.RuneWizard)
							{
								npc.ai[0] = 450f;
							}
							else if (npc.type == NPCID.Necromancer || npc.type == NPCID.NecromancerArmored)
							{
								if (Main.rand.NextBool(2))
								{
									npc.ai[0] = 390f;
									npc.netUpdate = true;
								}
							}
							else
							{
								npc.ai[0] = 400f;
							}
							npc.TargetClosest(true);
						}
					}
					if (npc.type == NPCID.SantaNK1 && (double)npc.life >= (double)npc.lifeMax * 0.5 && (double)npc.life - newDamage < (double)npc.lifeMax * 0.5)
					{
						Gore.NewGore(npc.GetSource_FromThis(), npc.position, npc.velocity, 517, 1f);
					}
					if (!npc.immortal)
					{
						if (npc.realLife >= 0)
						{
							Main.npc[npc.realLife].life -= (int)newDamage;
							npc.life = Main.npc[npc.realLife].life;
							npc.lifeMax = Main.npc[npc.realLife].lifeMax;
						}
						else
						{
							npc.life -= (int)newDamage;
						}
					}
					if (hit.Knockback > 0f && npc.knockBackResist > 0f)
					{
						float newKnockback = hit.Knockback * npc.knockBackResist;
						if (newKnockback > 8f)
						{
							float num4 = newKnockback - 8f;
							num4 *= 0.9f;
							newKnockback = 8f + num4;
						}
						if (newKnockback > 10f)
						{
							float num5 = newKnockback - 10f;
							num5 *= 0.8f;
							newKnockback = 10f + num5;
						}
						if (newKnockback > 12f)
						{
							float num6 = newKnockback - 12f;
							num6 *= 0.7f;
							newKnockback = 12f + num6;
						}
						if (newKnockback > 14f)
						{
							float num7 = newKnockback - 14f;
							num7 *= 0.6f;
							newKnockback = 14f + num7;
						}
						if (newKnockback > 16f)
						{
							newKnockback = 16f;
						}
						if (hit.Crit)
						{
							newKnockback *= 1.4f;
						}
						int num8 = (int)newDamage * 10;
						if (Main.expertMode)
						{
							num8 = (int)newDamage * 15;
						}
						if (num8 > npc.lifeMax)
						{
							if (hit.HitDirection < 0 && npc.velocity.X > -newKnockback)
							{
								if (npc.velocity.X > 0f)
								{
									npc.velocity.X = npc.velocity.X - newKnockback;
								}
								npc.velocity.X = npc.velocity.X - newKnockback;
								if (npc.velocity.X < -newKnockback)
								{
									npc.velocity.X = -newKnockback;
								}
							}
							else if (hit.HitDirection > 0 && npc.velocity.X < newKnockback)
							{
								if (npc.velocity.X < 0f)
								{
									npc.velocity.X = npc.velocity.X + newKnockback;
								}
								npc.velocity.X = npc.velocity.X + newKnockback;
								if (npc.velocity.X > newKnockback)
								{
									npc.velocity.X = newKnockback;
								}
							}
							if (!npc.noGravity)
							{
								newKnockback *= -0.75f;
							}
							else
							{
								newKnockback *= -0.5f;
							}
							if (npc.velocity.Y > newKnockback)
							{
								npc.velocity.Y = npc.velocity.Y + newKnockback;
								if (npc.velocity.Y < newKnockback)
								{
									npc.velocity.Y = newKnockback;
								}
							}
						}
						else
						{
							if (!npc.noGravity)
							{
								npc.velocity.Y = -newKnockback * 0.75f * npc.knockBackResist;
							}
							else
							{
								npc.velocity.Y = -newKnockback * 0.5f * npc.knockBackResist;
							}
							npc.velocity.X = newKnockback * (float)hit.HitDirection * npc.knockBackResist;
						}
					}
					if ((npc.type == NPCID.WallofFlesh || npc.type == NPCID.WallofFleshEye) && npc.life <= 0)
					{
						for (int i = 0; i < 200; i++)
						{
							if (Main.npc[i].active && (Main.npc[i].type == NPCID.WallofFlesh || Main.npc[i].type == NPCID.WallofFleshEye))
							{
								Main.npc[i].HitEffect(hit.HitDirection, newDamage);
							}
						}
					}
					else
					{
						npc.HitEffect(hit.HitDirection, newDamage);
					}
					if (npc.HitSound != null)
					{
						SoundEngine.PlaySound(npc.HitSound, npc.position);
					}
					if (npc.realLife >= 0)
					{
						Main.npc[npc.realLife].checkDead();
					}
					else
					{
						npc.checkDead();
					}
				}
				hit.Damage = (int)newDamage;
			}
		}

		public override void AI(NPC npc)
		{
            if (lacerator)
            {
				LaceratorEffects(ref npc);
            }
            if (npc.buffImmune[Mod.Find<ModBuff>("ExoFreeze").Type] && npc.type != Mod.Find<ModNPC>("Yharon").Type && npc.type != Mod.Find<ModNPC>("SupremeCalamitas").Type && npc.type != Mod.Find<ModNPC>("SupremeCataclysm").Type && npc.type != Mod.Find<ModNPC>("SupremeCatastrophe").Type)
			{
				npc.buffImmune[Mod.Find<ModBuff>("ExoFreeze").Type] = false;
			}
			if (npc.type == NPCID.TheDestroyer || npc.type == NPCID.Spazmatism || npc.type == NPCID.Retinazer || npc.type == NPCID.SkeletronPrime)
			{
				if (!CalamityWorld.spawnedHardBoss)
				{
					CalamityWorld.spawnedHardBoss = true;
				}
			}
			bool revenge = CalamityWorld.revenge;
			bool expertMode = Main.expertMode;
			if (expertMode)
			{
				if (npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight)
				{
					npc.life = revenge ? 100 : 500;
					npc.dontTakeDamage = true;
				}
				if (npc.type == NPCID.SkeletronPrime)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						npc.localAI[0] += 1f;
						if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
						{
							npc.localAI[0] += 1f;
						}
						if (revenge)
						{
							npc.localAI[0] += 1f;
						}
						if ((double)npc.life <= (double)npc.lifeMax * 0.7)
						{
							npc.localAI[0] += 1f;
						}
						if ((double)npc.life <= (double)npc.lifeMax * 0.4)
						{
							npc.localAI[0] += 1f;
						}
						if ((double)npc.life <= (double)npc.lifeMax * 0.1)
						{
							npc.localAI[0] += 2f;
						}
						if (npc.localAI[0] >= 150)
						{
							npc.localAI[0] = 0f;
							Vector2 vector16 = npc.Center;
							float num157 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector16.X;
							float num158 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector16.Y;
							Math.Sqrt((double)(num157 * num157 + num158 * num158));
							if (Collision.CanHit(vector16, 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
							{
								float num159 = 7f;
								float num160 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector16.X + (float)Main.rand.Next(-20, 21);
								float num161 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector16.Y + (float)Main.rand.Next(-20, 21);
								float num162 = (float)Math.Sqrt((double)(num160 * num160 + num161 * num161));
								num162 = num159 / num162;
								num160 *= num162;
								num161 *= num162;
								Vector2 value = new Vector2(num160 * 1f + (float)Main.rand.Next(-50, 51) * 0.01f, num161 * 1f + (float)Main.rand.Next(-50, 51) * 0.01f);
								value.Normalize();
								value *= num159;
								value += npc.velocity;
								num160 = value.X;
								num161 = value.Y;
								int num163 = 35;
								int num164 = 270;
								vector16 += value * 5f;
								int num165 = Projectile.NewProjectile(npc.GetSource_FromThis(), vector16.X, vector16.Y, num160, num161, num164, num163, 0f, Main.myPlayer, -1f, 0f);
								Main.projectile[num165].timeLeft = 300;
							}
						}
					}
				}
				if (npc.type == NPCID.PrimeCannon)
				{
					if (npc.ai[2] == 1f)
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							npc.localAI[1] += 1f;
							if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
							{
								npc.localAI[1] += 1f;
							}
							if (revenge)
							{
								npc.localAI[1] += 1f;
							}
							if (npc.localAI[1] > 30f)
							{
								npc.localAI[1] = 0f;
								npc.TargetClosest(true);
								if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
								{
									float num941 = 9f; //speed
									Vector2 vector104 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)(npc.height / 2));
									float num942 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector104.X + (float)Main.rand.Next(-20, 21);
									float num943 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector104.Y + (float)Main.rand.Next(-20, 21);
									float num944 = (float)Math.Sqrt((double)(num942 * num942 + num943 * num943));
									num944 = num941 / num944;
									num942 *= num944;
									num943 *= num944;
									num942 += (float)Main.rand.Next(-5, 6) * 0.05f;
									num943 += (float)Main.rand.Next(-5, 6) * 0.05f;
									int num945 = 40;
									int num946 = 303;
									vector104.X += num942 * 5f;
									vector104.Y += num943 * 5f;
									int num947 = Projectile.NewProjectile(npc.GetSource_FromThis(), vector104.X, vector104.Y, num942, num943, num946, num945, 0f, Main.myPlayer, 0f, 0f);
									Main.projectile[num947].timeLeft = 180;
									npc.netUpdate = true;
								}
							}
						}
					}
				}
			}
			if (revenge)
			{
				if (npc.type == NPCID.MoonLordCore)
				{
					int npcType = NPCID.MoonLordFreeEye;
					if (NPC.CountNPCS(npcType) > 0)
					{
						npc.dontTakeDamage = true;
					}
				}
				if (npc.type == NPCID.MoonLordHand || npc.type == NPCID.MoonLordHead)
				{
					bool flag90 = npc.ai[2] == 0f;
					float num1133 = (float)(-(float)flag90.ToDirectionInt());
					if (npc.ai[0] == -2f)
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							this.newAI[0] += (float)Main.rand.Next(4);
							if (this.newAI[0] >= (float)Main.rand.Next(200, 1201))
							{
								this.newAI[0] = 0f;
								npc.TargetClosest(true);
								SoundEngine.PlaySound(SoundID.Zombie103, npc.position);
								for (int num194 = 0; num194 < 40; num194++)
								{
									int num195 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Vortex, 0f, 0f, 0, default(Color), 2.5f);
									Main.dust[num195].noGravity = true;
									Main.dust[num195].velocity *= 3f;
									num195 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Vortex, 0f, 0f, 100, default(Color), 1.5f);
									Main.dust[num195].velocity *= 2f;
									Main.dust[num195].noGravity = true;
								}
								if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
								{
									Vector2 shootFromVector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
									float spread = 45f * 0.0174f;
									double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
									double deltaAngle = spread / 8f;
									double offsetAngle;
									int i;
									int laserDamage = 33;
									for (i = 0; i < 4; i++)
									{
										offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
										float ai = (6.28318548f * (float)Main.rand.NextDouble() - 3.14159274f) / 30f + 0.0174532924f * num1133;
										Projectile.NewProjectile(npc.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, (float)(Math.Sin(offsetAngle) * 9f), (float)(Math.Cos(offsetAngle) * 9f), 452, laserDamage, 0f, Main.myPlayer, 0f, ai);
										Projectile.NewProjectile(npc.GetSource_FromThis(), shootFromVector.X, shootFromVector.Y, (float)(-Math.Sin(offsetAngle) * 9f), (float)(-Math.Cos(offsetAngle) * 9f), 462, laserDamage, 0f, Main.myPlayer, 0f, 0f);
									}
								}
							}
						}
					}
				}
				if (npc.type == NPCID.CultistBoss)
				{
					bool goNuts = (double)npc.life <= (double)npc.lifeMax * 0.5;
					if (CultCountdown == 0)
					{
						if ((double)npc.life <= (double)npc.lifeMax * 0.1)
						{
							CultCountdown = 30;
						}
						else if ((double)npc.life <= (double)npc.lifeMax * 0.4)
						{
							CultCountdown = 80;
						}
						else if ((double)npc.life <= (double)npc.lifeMax * 0.7)
						{
							CultCountdown = 130;
						}
						else
						{
							CultCountdown = 180;
						}
					}
					if (CultCountdown > 0)
					{
						CultCountdown--;
						if (CultCountdown == 0)
						{
							if (Main.netMode != NetmodeID.MultiplayerClient)
							{
								for (int playerIndex = 0; playerIndex < 255; playerIndex++)
								{
									if (Main.player[playerIndex].active)
									{
										Player player2 = Main.player[playerIndex];
										int speed2 = goNuts ? 14 : 11;
										float spawnX = Main.rand.Next(1000) - 500 + player2.Center.X;
										float spawnY = -1000 + player2.Center.Y;
										Vector2 baseSpawn = new Vector2(spawnX, spawnY);
										Vector2 baseVelocity = player2.Center - baseSpawn;
										baseVelocity.Normalize();
										baseVelocity = baseVelocity * speed2;
										int damage = 35;
										if (goNuts)
										{
											CultProjectiles = 3;
										}
										for (int i = 0; i < CultProjectiles; i++)
										{
											Vector2 spawn2 = baseSpawn;
											spawn2.X = spawn2.X + i * 30 - (CultProjectiles * 15);
											Vector2 velocity = baseVelocity;
											velocity = baseVelocity.RotatedBy(MathHelper.ToRadians(-CultAngleSpread / 2 + (CultAngleSpread * i / (float)CultProjectiles)));
											velocity.X = velocity.X + 3 * Main.rand.NextFloat() - 1.5f;
											int projectileType = Main.rand.Next(3);
											if (projectileType == 0)
											{
												projectileType = 467;
											}
											else if (projectileType == 1)
											{
												projectileType = 348;
											}
											else
											{
												projectileType = 593;
											}
											Projectile.NewProjectile(npc.GetSource_FromThis(), spawn2.X, spawn2.Y, velocity.X, velocity.Y, projectileType, damage, 0f, Main.myPlayer, 0f, 0f);
										}
									}
								}
							}
						}
					}
				}
				if (npc.type == NPCID.DukeFishron)
				{
					Vector2 vector = npc.Center;
					bool murderMode = (double)npc.life <= (double)npc.lifeMax * 0.75;
					bool murderMode2 = (double)npc.life <= (double)npc.lifeMax * 0.33;
					float num = 0.6f * Main.GameModeInfo.EnemyDamageMultiplier;
					bool flag3 = npc.ai[0] > 4f;
					bool flag4 = npc.ai[0] > 9f;
					if (flag4)
					{
						npc.damage = (int)((float)npc.defDamage * 1.5f * num);
						npc.defense = 38;
					}
					else if (flag3)
					{
						npc.damage = (int)((float)npc.defDamage * 1.8f * num);
						npc.defense = (int)((float)npc.defDefense * 1.1f);
					}
					else
					{
						npc.damage = (int)((float)npc.defDamage * 1.3f * num);
						npc.defense = (int)((float)npc.defDefense * 1.3f);
					}
					if (npc.ai[0] == -1f || npc.ai[0] == 4f || npc.ai[0] == 9f || (Main.player[npc.target].position.Y < 800f || (double)Main.player[npc.target].position.Y > Main.worldSurface * 16.0 || (Main.player[npc.target].position.X > 6400f && Main.player[npc.target].position.X < (float)(Main.maxTilesX * 16 - 6400))))
					{
						npc.dontTakeDamage = true;
					}
					else if (npc.ai[0] <= 8f)
					{
						npc.dontTakeDamage = false;
					}
					float speed = 1.01f +
						(flag3 ? 0.005f : 0f) +
						(flag4 ? 0.005f : 0f);
					if (npc.ai[0] == 1f || npc.ai[0] == 6f || npc.ai[0] == 11f)
					{
						npc.velocity *= speed; //20, 24, 30
					}
					if (npc.ai[0] == 0f && !Main.player[npc.target].dead)
					{
						if (murderMode)
						{
							npc.ai[0] = 4f;
							npc.ai[1] = 0f;
							npc.ai[2] = 0f;
						}
					}
					else if (npc.ai[0] == 5f && !Main.player[npc.target].dead)
					{
						if (murderMode2)
						{
							npc.ai[0] = 9f;
							npc.ai[1] = 0f;
							npc.ai[2] = 0f;
						}
					}
				}
				if (npc.type == NPCID.GolemHeadFree && NPC.CountNPCS(NPCID.Golem) > 0)
				{
					npc.ai[1] += 1f;
					if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
					{
						npc.ai[1] += 1f;
					}
					if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax * 0.8)
					{
						npc.ai[1] += 2f;
					}
					if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax * 0.6)
					{
						npc.ai[1] += 2f;
					}
					if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax * 0.2)
					{
						npc.ai[1] += 2f;
					}
					if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax * 0.1)
					{
						npc.ai[1] += 4f;
					}
					npc.ai[2] += 1f;
					if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
					{
						npc.ai[2] += 1f;
					}
					if (Main.npc[NPC.golemBoss].life < Main.npc[NPC.golemBoss].lifeMax / 2)
					{
						npc.ai[2] += 2f;
					}
					if (Main.npc[NPC.golemBoss].life < Main.npc[NPC.golemBoss].lifeMax / 3)
					{
						npc.ai[2] += 2f;
					}
					if (Main.npc[NPC.golemBoss].life < Main.npc[NPC.golemBoss].lifeMax / 4)
					{
						npc.ai[2] += 2f;
					}
					if (Main.npc[NPC.golemBoss].life < Main.npc[NPC.golemBoss].lifeMax / 5)
					{
						npc.ai[2] += 2f;
					}
					if (Main.npc[NPC.golemBoss].life < Main.npc[NPC.golemBoss].lifeMax / 6)
					{
						npc.ai[2] += 4f;
					}
				}
				if (npc.type == NPCID.GolemHead)
				{
					if (npc.ai[0] == 1f)
					{
						npc.ai[1] += 1f;
						if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
						{
							npc.ai[1] += 1f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.4)
						{
							npc.ai[1] += 2f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.2)
						{
							npc.ai[1] += 3f;
						}
						npc.ai[2] += 1f;
						if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
						{
							npc.ai[2] += 1f;
						}
						if (npc.life < npc.lifeMax / 3)
						{
							npc.ai[2] += 1f;
						}
						if (npc.life < npc.lifeMax / 4)
						{
							npc.ai[2] += 2f;
						}
						if (npc.life < npc.lifeMax / 5)
						{
							npc.ai[2] += 2f;
						}
					}
				}
				if (npc.type == NPCID.Plantera)
				{
					bool jungle = Main.player[npc.target].ZoneJungle;
					if (npc.life > npc.lifeMax / 2)
					{
						npc.defense = 46;
						npc.damage = (int)(58f * Main.GameModeInfo.EnemyDamageMultiplier);
						if (jungle)
						{
							npc.defense *= 2;
							npc.damage *= 2;
						}
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							npc.localAI[1] += 0.5f;
							if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
							{
								npc.localAI[1] += 1f;
							}
							if ((double)npc.life <= (double)npc.lifeMax * 0.85)
							{
								npc.localAI[1] += 1f;
							}
							if ((double)npc.life <= (double)npc.lifeMax * 0.7)
							{
								npc.localAI[1] += 1.5f;
							}
							if ((double)npc.life <= (double)npc.lifeMax * 0.55)
							{
								npc.localAI[1] += 2f;
							}
						}
					}
					else
					{
						npc.defense = 26;
						npc.damage = (int)(90f * Main.GameModeInfo.EnemyDamageMultiplier);
						if (jungle)
						{
							npc.defense *= 4;
							npc.damage *= 2;
						}
						this.newAI[0] += 1f;
						if (this.newAI[0] >= 380f)
						{
							this.newAI[0] = 0f;
							if (Main.netMode != NetmodeID.MultiplayerClient)
							{
								NPC.NewNPC(npc.GetSource_FromThis(), (int)npc.Center.X, (int)npc.Center.Y, 264, npc.whoAmI, 0f, 0f, 0f, 0f, 255);
							}
						}
						npc.localAI[1] += 0.5f;
						if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
						{
							npc.localAI[1] += 1f;
						}
						if ((double)npc.life <= (double)npc.lifeMax * 0.4)
						{
							npc.localAI[1] += 1f;
						}
						if ((double)npc.life <= (double)npc.lifeMax * 0.25)
						{
							npc.localAI[1] += 1.5f;
						}
						if ((double)npc.life <= (double)npc.lifeMax * 0.1)
						{
							npc.localAI[1] += 2f;
						}
					}
				}
				if (npc.type == NPCID.SkeletronPrime)
				{
					if (npc.ai[1] == 1f)
					{
						int speed = 5;
						if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
						{
							speed++;
						}
						if ((double)npc.life <= (double)npc.lifeMax * 0.7)
						{
							speed++;
						}
						if ((double)npc.life <= (double)npc.lifeMax * 0.3)
						{
							speed++;
						}
						if ((double)npc.life <= (double)npc.lifeMax * 0.1)
						{
							speed++;
						}
						float speed2 = (float)speed;
						float speedBuff = 4f + (4f * (1f - (float)npc.life / (float)npc.lifeMax));
						float speedBuff2 = 14f + (14f * (1f - (float)npc.life / (float)npc.lifeMax));
						Vector2 vector45 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num444 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector45.X;
						float num445 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector45.Y;
						float num446 = (float)Math.Sqrt((double)(num444 * num444 + num445 * num445));
						speed2 += num446 / 100f;
						if (speed2 < speedBuff)
						{
							speed2 = speedBuff;
						}
						if (speed2 > speedBuff2)
						{
							speed2 = speedBuff2;
						}
						num446 = speed2 / num446;
						npc.velocity.X = num444 * num446;
						npc.velocity.Y = num445 * num446;
					}
				}
				if (npc.type == NPCID.Retinazer)
				{
					int npcType = NPCID.Spazmatism;
					bool spazAlive = false;
					if (NPC.CountNPCS(npcType) > 0)
					{
						spazAlive = true;
					}
					if (npc.ai[0] != 1f && npc.ai[0] != 2f && npc.ai[0] != 0f)
					{
						if (npc.ai[1] == 0f)
						{
							npc.ai[2] += 0.25f;
							npc.localAI[1] += 0.5f;
						}
						else
						{
							npc.ai[2] += 0.25f;
							npc.localAI[1] += 0.5f;
						}
						if (!spazAlive)
						{
							npc.damage = (int)((double)npc.defDamage * 2.2);
							npc.defense = npc.defDefense + 40;
							if (npc.ai[1] == 0f)
							{
								npc.ai[2] += 0.5f;
								npc.localAI[1] += 1f;
							}
							else
							{
								npc.ai[2] += 0.5f;
								npc.localAI[1] += 1f;
							}
							this.newAI[0] += 1f;
							if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
							{
								this.newAI[0] += 1f;
							}
							if (this.newAI[0] >= 150f)
							{
								this.newAI[0] = 0f;
								Vector2 vector34 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
								float num349 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector34.X;
								float num350 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector34.Y;
								float num351 = (float)Math.Sqrt((double)(num349 * num349 + num350 * num350));
								num349 *= num351;
								num350 *= num351;
								if (Main.netMode != NetmodeID.MultiplayerClient)
								{
									float num353 = 8f;
									int num354 = 30;
									int num355 = Mod.Find<ModProjectile>("ScavengerLaser").Type;
									vector34 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
									num349 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector34.X;
									num350 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector34.Y;
									num351 = (float)Math.Sqrt((double)(num349 * num349 + num350 * num350));
									num351 = num353 / num351;
									num349 *= num351;
									num350 *= num351;
									vector34.X += num349;
									vector34.Y += num350;
									Projectile.NewProjectile(npc.GetSource_FromThis(), vector34.X, vector34.Y, num349, num350, num355, num354, 0f, Main.myPlayer, 0f, 0f);
								}
							}
						}
					}
				}
				if (npc.type == NPCID.Spazmatism)
				{
					int npcType = NPCID.Retinazer;
					bool retAlive = false;
					if (NPC.CountNPCS(npcType) > 0)
					{
						retAlive = true;
					}
					if (npc.ai[0] != 1f && npc.ai[0] != 2f && npc.ai[0] != 0f)
					{
						if (npc.ai[1] == 0f)
						{
							npc.ai[2] += 1f;
							npc.velocity.X *= 1.0035f;
							npc.velocity.Y *= 1.0035f;
						}
						else
						{
							npc.ai[2] += 0.25f;
							npc.velocity.X *= 1.0035f;
							npc.velocity.Y *= 1.0035f;
						}
						if (!retAlive)
						{
							npc.damage = (int)((double)npc.defDamage * 2.5);
							npc.defense = npc.defDefense + 55;
							if (npc.ai[1] == 0f)
							{
								npc.ai[2] += 2f;
								npc.velocity.X *= 1.0045f;
								npc.velocity.Y *= 1.0045f;
							}
							else
							{
								npc.ai[2] += 0.5f;
								npc.velocity.X *= 1.0075f;
								npc.velocity.Y *= 1.0075f;
							}
						}
					}
				}
				if (npc.type == NPCID.TheDestroyerTail)
				{
					int defenseDown = (int)(185f * (1f - (float)npc.life / (float)npc.lifeMax));
					npc.defense = npc.defDefense - defenseDown;
				}
				if (npc.type == NPCID.TheDestroyerBody)
				{
					int defenseUp = (int)(50f * (1f - (float)npc.life / (float)npc.lifeMax));
					npc.defense = npc.defDefense + defenseUp;
					npc.localAI[0] = 0f;
					int shootTime = 4;
					if ((double)Main.player[npc.target].statLife > (double)Main.player[npc.target].statLifeMax2 * 0.5)
					{
						shootTime = 8;
					}
					if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel300)
					{
						shootTime++;
					}
					if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel400)
					{
						shootTime++;
					}
					this.newAI[0] += (float)Main.rand.Next(shootTime);
					if (this.newAI[0] >= (float)Main.rand.Next(1400, 26000))
					{
						this.newAI[0] = 0f;
						npc.TargetClosest(true);
						if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
						{
							float speed = 8.5f;
							Vector2 vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)(npc.height / 2));
							float num6 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector.X + (float)Main.rand.Next(-20, 21);
							float num7 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector.Y + (float)Main.rand.Next(-20, 21);
							float num8 = (float)Math.Sqrt((double)(num6 * num6 + num7 * num7));
							num8 = speed / num8;
							num6 *= num8;
							num7 *= num8;
							num6 += (float)Main.rand.Next(-10, 11) * 0.05f;
							num7 += (float)Main.rand.Next(-10, 11) * 0.05f;
							int num9 = 24;
							int num10 = 100;
							if (Main.rand.NextBool(25))
							{
								num10 = 257;
							}
							vector.X += num6 * 5f;
							vector.Y += num7 * 5f;
							int num11 = Projectile.NewProjectile(npc.GetSource_FromThis(), vector.X, vector.Y, num6, num7, num10, num9, 0f, Main.myPlayer, 0f, 0f);
							Main.projectile[num11].timeLeft = 300;
							npc.netUpdate = true;
						}
					}
				}
				if (npc.type == NPCID.TheDestroyer)
				{
					int defenseDown = (int)(150f * (1f - (float)npc.life / (float)npc.lifeMax));
					npc.defense = npc.defDefense - defenseDown;
				}
				if (npc.type == NPCID.WallofFlesh)
				{
					npc.velocity.X *= 1.08f;
				}
				if (npc.type == NPCID.WallofFleshEye && NPC.CountNPCS(NPCID.WallofFlesh) > 0)
				{
					Vector2 vector34 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float num349 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector34.X;
					float num350 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector34.Y;
					float num351 = (float)Math.Sqrt((double)(num349 * num349 + num350 * num350));
					num349 *= num351;
					num350 *= num351;
					bool flag30 = true;
					if (npc.direction > 0)
					{
						if (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) < npc.position.X + (float)(npc.width / 2))
						{
							flag30 = false;
						}
					}
					else if (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) > npc.position.X + (float)(npc.width / 2))
					{
						flag30 = false;
					}
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int num352 = 4;
						npc.localAI[1] -= 1f;
						npc.localAI[3] += 3f;
						if ((double)Main.npc[Main.wofNPCIndex].life < (double)Main.npc[Main.wofNPCIndex].lifeMax * 0.75)
						{
							npc.localAI[1] -= 1f;
							npc.localAI[3] += 1f;
							num352++;
						}
						if ((double)Main.npc[Main.wofNPCIndex].life < (double)Main.npc[Main.wofNPCIndex].lifeMax * 0.5)
						{
							npc.localAI[1] -= 1f;
							npc.localAI[3] += 1f;
							num352++;
						}
						if ((double)Main.npc[Main.wofNPCIndex].life < (double)Main.npc[Main.wofNPCIndex].lifeMax * 0.25)
						{
							npc.localAI[1] -= 1f;
							npc.localAI[3] += 1f;
							num352 += 2;
						}
						if ((double)Main.npc[Main.wofNPCIndex].life < (double)Main.npc[Main.wofNPCIndex].lifeMax * 0.1)
						{
							npc.localAI[1] -= 2f;
							npc.localAI[3] += 2f;
							num352 += 3;
						}
						if (expertMode)
						{
							npc.localAI[1] -= 0.5f;
							npc.localAI[3] += 1f;
							num352++;
							if ((double)Main.npc[Main.wofNPCIndex].life < (double)Main.npc[Main.wofNPCIndex].lifeMax * 0.1)
							{
								npc.localAI[1] -= 2f;
								npc.localAI[3] += 2f;
								num352 += 3;
							}
						}
						if (this.newAI[0] == 0f)
						{
							if (npc.localAI[3] > 600f)
							{
								this.newAI[0] = 1f;
								npc.localAI[3] = 0f;
								return;
							}
						}
						else if (npc.localAI[3] > 45f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
						{
							npc.localAI[3] = 0f;
							this.newAI[0] += 1f;
							if (this.newAI[0] >= (float)num352)
							{
								this.newAI[0] = 0f;
							}
							if (flag30)
							{
								float num353 = 10f;
								int num354 = 18;
								int num355 = 83;
								if ((double)Main.npc[Main.wofNPCIndex].life < (double)Main.npc[Main.wofNPCIndex].lifeMax * 0.5)
								{
									num354++;
									num353 += 1f;
								}
								if ((double)Main.npc[Main.wofNPCIndex].life < (double)Main.npc[Main.wofNPCIndex].lifeMax * 0.25)
								{
									num354++;
									num353 += 1f;
								}
								if ((double)Main.npc[Main.wofNPCIndex].life < (double)Main.npc[Main.wofNPCIndex].lifeMax * 0.1)
								{
									num354 += 2;
									num353 += 2f;
								}
								if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel300)
								{
									num354++;
									num353 += 1f;
								}
								vector34 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
								num349 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector34.X;
								num350 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector34.Y;
								num351 = (float)Math.Sqrt((double)(num349 * num349 + num350 * num350));
								num351 = num353 / num351;
								num349 *= num351;
								num350 *= num351;
								vector34.X += num349;
								vector34.Y += num350;
								Projectile.NewProjectile(npc.GetSource_FromThis(), vector34.X, vector34.Y, num349, num350, num355, num354, 0f, Main.myPlayer, 0f, 0f);
								return;
							}
						}
					}
				}
				if (npc.type == NPCID.SkeletronHand)
				{
					if (npc.ai[2] == 0f || npc.ai[2] == 3f)
					{
						if (Main.npc[(int)npc.ai[1]].ai[1] == 0f)
						{
							npc.ai[3] += 0.5f;
						}
					}
				}
				if (npc.type == NPCID.SkeletronHead)
				{
					if (npc.ai[1] == 1f)
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							npc.localAI[1] += 5f;
							if ((double)npc.life <= (double)npc.lifeMax * 0.5)
							{
								npc.localAI[1] += 3f;
							}
							if ((double)npc.life <= (double)npc.lifeMax * 0.15)
							{
								npc.localAI[1] += 2f;
							}
							if (npc.localAI[1] >= 600f)
							{
								npc.localAI[1] = 0f;
								Vector2 vector16 = npc.Center;
								if (Collision.CanHit(vector16, 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
								{
									float num159 = 5f;
									float num160 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector16.X + (float)Main.rand.Next(-20, 21);
									float num161 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector16.Y + (float)Main.rand.Next(-20, 21);
									float num162 = (float)Math.Sqrt((double)(num160 * num160 + num161 * num161));
									num162 = num159 / num162;
									num160 *= num162;
									num161 *= num162;
									Vector2 value = new Vector2(num160 * 1f + (float)Main.rand.Next(-50, 51) * 0.01f, num161 * 1f + (float)Main.rand.Next(-50, 51) * 0.01f);
									value.Normalize();
									value *= num159;
									value += npc.velocity;
									num160 = value.X;
									num161 = value.Y;
									int num163 = 25;
									int num164 = 270;
									vector16 += value * 5f;
									int num165 = Projectile.NewProjectile(npc.GetSource_FromThis(), vector16.X, vector16.Y, num160, num161, num164, num163, 0f, Main.myPlayer, -1f, 0f);
									Main.projectile[num165].timeLeft = 300;
								}
							}
						}
						Vector2 vector20 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num173 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector20.X;
						float num174 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector20.Y;
						float num175 = (float)Math.Sqrt((double)(num173 * num173 + num174 * num174));
						float num176 = 5.5f;
						npc.damage = (int)((double)npc.defDamage * 1.6);
						if (num175 > 150f)
						{
							num176 *= 1.05f;
						}
						if (num175 > 200f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 250f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 300f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 350f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 400f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 450f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 500f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 550f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 600f)
						{
							num176 *= 1.1f;
						}
						num175 = num176 / num175;
						npc.velocity.X = num173 * num175;
						npc.velocity.Y = num174 * num175;
					}
				}
				if (npc.type == NPCID.QueenBee)
				{
					Vector2 vector74 = new Vector2(npc.position.X + (float)(npc.width / 2) + (float)(Main.rand.Next(20) * npc.direction), npc.position.Y + (float)npc.height * 0.8f);
					int damageBoost = (int)(30f * (1f - (float)npc.life / (float)npc.lifeMax));
					npc.damage = npc.defDamage + damageBoost;
					if (npc.ai[0] == 1f)
					{
						npc.ai[1] += 1f;
					}
					else if (npc.ai[0] == 3f)
					{
						this.newAI[0] += 1f;
						bool flag39 = false;
						if ((double)npc.life < (double)npc.lifeMax * 0.1)
						{
							if (this.newAI[0] % 20f == 19f)
							{
								flag39 = true;
							}
						}
						else if (npc.life < npc.lifeMax / 3)
						{
							if (this.newAI[0] % 35f == 34f)
							{
								flag39 = true;
							}
						}
						else if (npc.life < npc.lifeMax / 2)
						{
							if (this.newAI[0] % 50f == 49f)
							{
								flag39 = true;
							}
						}
						else if (this.newAI[0] % 55f == 54f)
						{
							flag39 = true;
						}
						if (flag39 && npc.position.Y + (float)npc.height < Main.player[npc.target].position.Y && Collision.CanHit(vector74, 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
						{
							SoundEngine.PlaySound(SoundID.Item17, npc.position);
							if (Main.netMode != NetmodeID.MultiplayerClient)
							{
								float num602 = 12f;
								if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel300)
								{
									num602 += 2f;
								}
								if ((double)npc.life < (double)npc.lifeMax * 0.1)
								{
									num602 += 3f;
								}
								float num603 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector74.X + (float)Main.rand.Next(-80, 81);
								float num604 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector74.Y + (float)Main.rand.Next(-40, 41);
								float num605 = (float)Math.Sqrt((double)(num603 * num603 + num604 * num604));
								num605 = num602 / num605;
								num603 *= num605;
								num604 *= num605;
								int num606 = 14;
								int num607 = 55;
								int num608 = Projectile.NewProjectile(npc.GetSource_FromThis(), vector74.X, vector74.Y, num603, num604, num607, num606, 0f, Main.myPlayer, 0f, 0f);
								Main.projectile[num608].timeLeft = 300;
							}
						}
					}
				}
				if (npc.type == NPCID.BrainofCthulhu)
				{
					this.newAI[0] += 1f;
					if (this.newAI[0] >= 300f)
					{
						this.newAI[0] = 0f;
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							int creeper = NPC.NewNPC(npc.GetSource_FromThis(), (int)(npc.position.X + (float)(npc.width / 2) + npc.velocity.X), (int)(npc.position.Y + (float)(npc.height / 2) + npc.velocity.Y), 267, 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[creeper].netUpdate = true;
						}
					}
					if (npc.ai[0] < 0f)
					{
						npc.knockBackResist = (0.35f * Main.GameModeInfo.KnockbackToEnemiesMultiplier);
						npc.velocity.X *= 1.005f;
						npc.velocity.Y *= 1.005f;
					}
				}
				if (npc.type == NPCID.EaterofWorldsHead)
				{
					if (!Main.player[npc.target].dead)
					{
						this.newAI[0] += 1f;
					}
					if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel300)
					{
						this.newAI[0] += 1f;
					}
					if (this.newAI[0] >= 300f)
					{
						this.newAI[0] = 0f;
						if (Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
						{
							Vector2 vector34 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
							float num349 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector34.X;
							float num350 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector34.Y;
							float num351 = (float)Math.Sqrt((double)(num349 * num349 + num350 * num350));
							num349 *= num351;
							num350 *= num351;
							if (Main.netMode != NetmodeID.MultiplayerClient)
							{
								float num418 = 12f;
								int num419 = 12;
								int num420 = 96;
								num351 = (float)Math.Sqrt((double)(num349 * num349 + num350 * num350));
								num351 = num418 / num351;
								num349 *= num351;
								num350 *= num351;
								num349 += (float)Main.rand.Next(-40, 41) * 0.05f;
								num350 += (float)Main.rand.Next(-40, 41) * 0.05f;
								vector34.X += num349 * 4f;
								vector34.Y += num350 * 4f;
								Projectile.NewProjectile(npc.GetSource_FromThis(), vector34.X, vector34.Y, num349, num350, num420, num419, 0f, Main.myPlayer, 0f, 0f);
							}
						}
					}
				}
				if (npc.type == NPCID.EyeofCthulhu)
				{
					if (npc.ai[0] == 0f)
					{
						if (npc.ai[1] == 0f)
						{
							npc.ai[2] += 0.05f;
							if ((double)npc.life <= (double)npc.lifeMax * 0.85)
							{
								npc.ai[2] += 0.15f;
							}
							if ((double)npc.life <= (double)npc.lifeMax * 0.7)
							{
								npc.ai[2] += 0.3f;
							}
						}
						else if (npc.ai[1] == 2f)
						{
							npc.velocity *= 1.002f;
							if ((double)npc.life <= (double)npc.lifeMax * 0.85)
							{
								npc.velocity *= 1.004f;
							}
							if ((double)npc.life <= (double)npc.lifeMax * 0.7)
							{
								npc.velocity *= 1.0075f;
							}
						}
					}
					else
					{
						if (npc.ai[1] == 0f)
						{
							if ((double)npc.life <= (double)npc.lifeMax * 0.55)
							{
								npc.ai[2] += 1f;
							}
							if ((double)npc.life <= (double)npc.lifeMax * 0.4)
							{
								npc.ai[2] += 1f;
							}
						}
						else if (npc.ai[1] == 2f)
						{
							if ((double)npc.life <= (double)npc.lifeMax * 0.55)
							{
								npc.velocity *= 1.005f;
							}
							if ((double)npc.life <= (double)npc.lifeMax * 0.4)
							{
								npc.velocity *= 1.005f;
							}
						}
						else if (npc.ai[1] == 4f)
						{
							if ((double)npc.life <= (double)npc.lifeMax * 0.25)
							{
								npc.velocity *= 1.0075f;
							}
							if ((double)npc.life <= (double)npc.lifeMax * 0.1)
							{
								npc.velocity *= 1.0075f;
							}
						}
						else if (npc.ai[1] == 5f)
						{
							if ((double)npc.life <= (double)npc.lifeMax * 0.25)
							{
								npc.ai[2] += 1f;
							}
							if ((double)npc.life <= (double)npc.lifeMax * 0.1)
							{
								npc.ai[2] += 1f;
							}
						}
					}
				}
				if (npc.type == NPCID.KingSlime)
				{
					bool move = false;
					if (npc.ai[1] == 5f)
					{
						move = true;
						npc.ai[0] += 2f;
					}
					else if (npc.ai[1] == 6f)
					{
						move = true;
						npc.ai[0] += 2f;
					}
					if (npc.velocity.Y == 0f)
					{
						if (!move)
						{
							npc.ai[0] += 3f;
						}
					}
				}
			}
		}

		public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorld.revenge)
			{
				if (npc.type == NPCID.DemonEye)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 60);
				}
				if (npc.type == NPCID.EyeofCthulhu)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.EaterofSouls)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 60);
				}
				if (npc.type == NPCID.DevourerHead)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.EaterofWorldsHead)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.EaterofWorldsBody)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.EaterofWorldsTail)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 60);
				}
				if (npc.type == NPCID.ChaosBall)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 60);
				}
				if (npc.type == NPCID.CursedSkull)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.SkeletronHead)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.SkeletronHand)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.CorruptBunny)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.CorruptGoldfish)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.Demon)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.VoodooDemon)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.DungeonGuardian)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 1200);
				}
				if (npc.type == NPCID.DarkMummy)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.CorruptSlime)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.Wraith)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 240);
				}
				if (npc.type == NPCID.CursedHammer)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.Corruptor)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.SeekerHead)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.Clinger)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.VileSpit)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 60);
				}
				if (npc.type == NPCID.WallofFlesh)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300);
				}
				if (npc.type == NPCID.TheHungry)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.TheHungryII)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.LeechHead)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 60);
				}
				if (npc.type == NPCID.Slimer)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.WanderingEye)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.RedDevil)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.VampireBat)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 240);
				}
				if (npc.type == NPCID.Vampire)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 240);
				}
				if (npc.type == NPCID.Frankenstein)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.BlackRecluse)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.WallCreeper)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.WallCreeperWall)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.SwampThing)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.CorruptPenguin)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.Crimera)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 60);
				}
				if (npc.type == NPCID.Herpling)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.CrimsonAxe)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.FaceMonster)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 60);
				}
				if (npc.type == NPCID.FloatyGross)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.Crimslime)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type >= NPCID.CataractEye && npc.type <= NPCID.PurpleEye)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 60);
				}
				if (npc.type == NPCID.Nymph)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.BlackRecluseWall)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.BloodCrawler)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 60);
				}
				if (npc.type == NPCID.BloodCrawlerWall)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 60);
				}
				if (npc.type == NPCID.BloodFeeder)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.BloodJelly)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.Eyezor)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.Reaper)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 240);
				}
				if (npc.type == NPCID.BrainofCthulhu)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.Creeper)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 60);
				}
				if (npc.type == NPCID.IchorSticker)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.DungeonSpirit)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300);
				}
				if (npc.type == NPCID.GiantCursedSkull)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type >= NPCID.Scarecrow1 && npc.type <= NPCID.Scarecrow10)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.HeadlessHorseman)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.Ghost)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.MourningWood)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 240);
				}
				if (npc.type == NPCID.Splinterling)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.Pumpking)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 240);
				}
				if (npc.type == NPCID.PumpkingBlade)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.Hellhound)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.Poltergeist)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.Krampus)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300);
				}
				if (npc.type == NPCID.Butcher)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.CreatureFromTheDeep)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.Fritz)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.Nailhead)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.CrimsonBunny)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.CrimsonGoldfish)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.Psycho)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.DrManFly)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.ThePossessed)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.CrimsonPenguin)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.Mothron)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.Medusa)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.BloodZombie)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.Drippler)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 120);
				}
				if (npc.type == NPCID.AncientCultistSquidhead)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 240);
				}
				if (npc.type == NPCID.AncientDoom)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300);
				}
				if (npc.type == NPCID.SandsharkCorrupt)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
				if (npc.type == NPCID.SandsharkCrimson)
				{
					target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180);
				}
			}
		}

		public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
		{
			if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().eGauntlet)
			{
				if (item.CountsAsClass(DamageClass.Melee) && npc.damage > 0 /*&& modifiers.crit*/ && !npc.boss && !npc.friendly && !npc.dontTakeDamage && Main.rand.NextBool(3) && npc.type != NPCID.Mothron && npc.type != NPCID.Pumpking && npc.type != NPCID.TheDestroyerBody && npc.type != NPCID.TheDestroyerTail && npc.type != NPCID.MourningWood && npc.type != NPCID.Everscream && npc.type != NPCID.SantaNK1 && npc.type != NPCID.IceQueen)
				{
					modifiers.FinalDamage.Base = npc.lifeMax * 5;
				}
			}
			if (item.shoot == Mod.Find<ModProjectile>("Exobeam").Type)
			{
				if (npc.damage > 0 && !npc.boss && !npc.friendly && !npc.dontTakeDamage && Main.rand.NextBool(10) && npc.type != NPCID.Mothron && npc.type != NPCID.Pumpking && npc.type != NPCID.TheDestroyerBody && npc.type != NPCID.TheDestroyerTail && npc.type != NPCID.MourningWood && npc.type != NPCID.Everscream && npc.type != NPCID.SantaNK1 && npc.type != NPCID.IceQueen)
				{
					modifiers.FinalDamage.Base = npc.lifeMax * 5;
				}
			}
		}

		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().eTalisman)
			{
				if (projectile.CountsAsClass(DamageClass.Magic) && npc.damage > 0 && !npc.boss && !npc.friendly && !npc.dontTakeDamage && Main.rand.NextBool(15) && npc.type != NPCID.Mothron && npc.type != NPCID.Pumpking && npc.type != NPCID.TheDestroyerBody && npc.type != NPCID.TheDestroyerTail && npc.type != NPCID.MourningWood && npc.type != NPCID.Everscream && npc.type != NPCID.SantaNK1 && npc.type != NPCID.IceQueen)
				{
					modifiers.FinalDamage.Base = npc.lifeMax * 5;
				}
			}
			if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().nanotech)
			{
				if (projectile.CountsAsClass(DamageClass.Throwing) && npc.damage > 0 && !npc.boss && !npc.friendly && !npc.dontTakeDamage && Main.rand.NextBool(15) && npc.type != NPCID.Mothron && npc.type != NPCID.Pumpking && npc.type != NPCID.TheDestroyerBody && npc.type != NPCID.TheDestroyerTail && npc.type != NPCID.MourningWood && npc.type != NPCID.Everscream && npc.type != NPCID.SantaNK1 && npc.type != NPCID.IceQueen)
				{
					modifiers.FinalDamage.Base = npc.lifeMax * 5;
				}
			}
			if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().eQuiver)
			{
				if (projectile.CountsAsClass(DamageClass.Ranged) && npc.damage > 0 && !npc.boss && !npc.friendly && !npc.dontTakeDamage && Main.rand.NextBool(15) && npc.type != NPCID.Mothron && npc.type != NPCID.Pumpking && npc.type != NPCID.TheDestroyerBody && npc.type != NPCID.TheDestroyerTail && npc.type != NPCID.MourningWood && npc.type != NPCID.Everscream && npc.type != NPCID.SantaNK1 && npc.type != NPCID.IceQueen)
				{
					modifiers.FinalDamage.Base = npc.lifeMax * 5;
				}
			}
			if (CalamityWorld.revenge)
			{
				if (npc.type == NPCID.TheDestroyer ||
					npc.type == NPCID.TheDestroyerBody ||
					npc.type == NPCID.TheDestroyerTail)
				{
					if (projectile.type == ProjectileID.HallowStar)
					{
						modifiers.FinalDamage /= 3;
					}
					if ((projectile.penetrate == -1 || projectile.penetrate > 3) && !projectile.minion)
					{
						projectile.penetrate = 3;
					}
				}
				if (npc.type == NPCID.EaterofWorldsHead ||
					npc.type == NPCID.EaterofWorldsBody ||
					npc.type == NPCID.EaterofWorldsTail ||
				   (npc.type >= NPCID.CultistDragonHead && npc.type <= NPCID.CultistDragonTail) ||
					npc.type == Mod.Find<ModNPC>("DesertScourgeHead").Type ||
					npc.type == Mod.Find<ModNPC>("DesertScourgeBody").Type ||
					npc.type == Mod.Find<ModNPC>("DesertScourgeTail").Type)
				{
					if (projectile.penetrate == -1 && !projectile.minion)
					{
						modifiers.FinalDamage /= 2;
					}
					else if (projectile.penetrate > 1)
					{
						modifiers.FinalDamage /= projectile.penetrate;
					}
				}
			}
		}

		public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
		{
			if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().bloodflareSet)
			{
				if (!npc.SpawnedFromStatue && npc.damage > 0 && ((double)npc.life < (double)npc.lifeMax * 0.5) && Main.rand.NextBool(25))
				{
					Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 58, 1, false, 0, false, false);
				}
				else if (!npc.SpawnedFromStatue && npc.damage > 0 && ((double)npc.life > (double)npc.lifeMax * 0.5) && Main.rand.NextBool(25))
				{
					Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 184, 1, false, 0, false, false);
				}
			}
		}

		public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
		{
			if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().bloodflareSet)
			{
				if (!npc.SpawnedFromStatue && npc.damage > 0 && ((double)npc.life < (double)npc.lifeMax * 0.5) && Main.rand.NextBool(25))
				{
					Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 58, 1, false, 0, false, false);
				}
				else if (!npc.SpawnedFromStatue && npc.damage > 0 && ((double)npc.life > (double)npc.lifeMax * 0.5) && Main.rand.NextBool(25))
				{
					Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 184, 1, false, 0, false, false);
				}
			}
		}

		public override bool CheckDead(NPC npc)
		{
			if (npc.lifeMax > 1000 && npc.type != NPCID.DungeonSpirit && npc.type != Mod.Find<ModNPC>("PhantomSpirit").Type && npc.value > 0f && npc.HasPlayerTarget && CalamityWorld.downedProvidence && Main.player[npc.target].ZoneDungeon)
			{
				int maxValue = 6;
				if (Main.expertMode)
				{
					maxValue = 4;
				}
				if (Main.rand.NextBool(maxValue) && Main.wallDungeon[(int)Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16].WallType])
				{
					NPC.NewNPC(npc.GetSource_FromThis(), (int)npc.Center.X, (int)npc.Center.Y, Mod.Find<ModNPC>("PhantomSpirit").Type, 0, 0f, 0f, 0f, 0f, 255);
				}
			}
			return true;
		}

		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (npc.boss)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), ModContent.ItemType<Laudanum>(), 50));
				npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), ModContent.ItemType<StressPills>(), 50));
			}
			if (npc.type == ModContent.NPCType<DesertScourgeHead>())
			{
				npcLoot.Add(new CommonDrop(ItemID.LesserHealingPotion, 1, 8, 14));
				npcLoot.Add(new CommonDrop(ModContent.ItemType<AeroStone>(), 10));
				npcLoot.Add(new CommonDrop(ModContent.ItemType<DesertScourgeTrophy>(), 10));
				npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<DesertScourgeBag>()));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.Starfish, 1, 5, 9));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.Seashell, 1, 5, 9));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.Coral, 1, 5, 9));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<VictoryShard>(), 1, 7, 14));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DesertScourgeMask>(), 7));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SeaboundStaff>(), 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<AquaticDischarge>(), 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Barinade>(), 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<StormSpray>(), 4));
				npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), ModContent.ItemType<TorrentialTear>()));
				npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), ModContent.ItemType<ScourgeoftheDesert>()));
			}
			if (npc.type == ModContent.NPCType<AstrumDeusHead>())
			{
				npcLoot.Add(new CommonDrop(ModContent.ItemType<Stardust>(), 1, 50, 80));
				npcLoot.Add(new CommonDrop(ItemID.GreaterHealingPotion, 1, 8, 14));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<AstralBulwark>()));
			}
			if (npc.type == ModContent.NPCType<DevourerofGodsHead>())
			{
				npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<DevourerofGodsBag>()));
				npcLoot.Add(new CommonDrop(ModContent.ItemType<SupremeHealingPotion>(), 1, 8, 14));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DevourerofGodsTrophy>(), 10));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DevourerofGodsMask>(), 7));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CosmiliteBar>(), 1, 25, 34));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DeathhailStaff>(), 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Eradicator>(), 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Excelsus>(), 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Weapons.DevourerofGods.TheObliterator>(), 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EradicatorMelee>(), 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Deathwind>(), 4));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<StaffoftheMechworm>(), 4));
				npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), ModContent.ItemType<Murasama>()));
			}
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotFromStatue(), ModContent.ItemType<YharimsCrystal>(), 1000000));
			if (npc.type == ModContent.NPCType<Yharon.Yharon>())
			{
				npcLoot.Add(new CommonDrop(ModContent.ItemType<YharimsCrystal>(), 1000));
			}
			#region legends
			// this is a real scaling legendary moment
			LeadingConditionRule notStatue = new LeadingConditionRule(new Conditions.NotFromStatue());
			LeadingConditionRule preHm = new LeadingConditionRule(new Conditions.IsPreHardmode());
			LeadingConditionRule hm = new LeadingConditionRule(new Conditions.IsHardmode());
			LeadingConditionRule postML = new LeadingConditionRule(new MoonCondition());
			LeadingConditionRule expert = new LeadingConditionRule(new Conditions.IsExpert());
			int[] legendaries = new int[] { ModContent.ItemType<AegisBlade>(), ModContent.ItemType<BlossomFlux>(), ModContent.ItemType<BrinyBaron>(), ModContent.ItemType<SHPC>(), ModContent.ItemType<CosmicDischarge>(), ModContent.ItemType<Vesuvius>(), ModContent.ItemType<TheCommunity>(), ModContent.ItemType<Malachite>() };
			notStatue.OnSuccess(preHm.OnSuccess(ItemDropRule.OneFromOptions(500000, legendaries)));
			notStatue.OnSuccess(hm.OnSuccess(ItemDropRule.OneFromOptions(250000, legendaries)));
			notStatue.OnSuccess(postML.OnSuccess(ItemDropRule.OneFromOptions(150000, legendaries)));
			npcLoot.Add(notStatue);
			AddLegendaryBossDrop(ref npcLoot, ref npc, NPCID.Golem, ModContent.ItemType<AegisBlade>());
			AddLegendaryBossDrop(ref npcLoot, ref npc, NPCID.DukeFishron, ModContent.ItemType<BrinyBaron>());
			AddLegendaryBossDrop(ref npcLoot, ref npc, ModContent.NPCType<DevourerofGodsHead>(), ModContent.ItemType<CosmicDischarge>());
			AddLegendaryBossDrop(ref npcLoot, ref npc, NPCID.DD2Betsy, ModContent.ItemType<Vesuvius>());
			AddLegendaryBossDrop(ref npcLoot, ref npc, ModContent.NPCType<PlaguebringerGoliath.PlaguebringerGoliath>(), ModContent.ItemType<Malachite>());
			AddLegendaryBossDrop(ref npcLoot, ref npc, NPCID.Plantera, ModContent.ItemType<BlossomFlux>());
			AddLegendaryBossDrop(ref npcLoot, ref npc, NPCID.TheDestroyer, ModContent.ItemType<SHPC>());
			AddLegendaryBossDrop(ref npcLoot, ref npc, ModContent.NPCType<Siren>(), ModContent.ItemType<TheCommunity>());
			#endregion
			if (npc.type == NPCID.PossessedArmor)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PsychoticAmulet>(), 200, 150));
			}
			if (npc.type == NPCID.SeaSnail)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<SeaShell>(), 4, 3));
			}
			if (npc.type == NPCID.GiantTortoise)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<GiantTortoiseShell>(), 7, 5));
			}
			if (npc.type == NPCID.GiantShelly || npc.type == NPCID.GiantShelly2)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<GiantShell>(), 7, 5));
			}
			if (npc.type == NPCID.AnomuraFungus)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<FungalCarapace>(), 7, 5));
			}
			if (npc.type == NPCID.Crawdad || npc.type == NPCID.Crawdad2)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CrawCarapace>(), 7, 5));
			}
			if (npc.type == NPCID.GreenJellyfish)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<VitalJelly>(), 7, 5));
			}
			if (npc.type == NPCID.BlueJellyfish)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ManaJelly>(), 7, 5));
			}
			if (npc.type == NPCID.PinkJellyfish)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<LifeJelly>(), 7, 5));
			}
			if (npc.type == NPCID.MossHornet)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Needler>(), 25, 20));
			}
			if (npc.type == NPCID.DarkCaster)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<AncientShiv>(), 25, 20));
			}
			if (npc.type == NPCID.BigMimicCorruption || npc.type == NPCID.BigMimicCrimson || npc.type == NPCID.BigMimicHallow || npc.type == NPCID.BigMimicJungle)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CelestialClaymore>(), 7, 5));
			}
			if (npc.type == NPCID.Clinger)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CursedDagger>(), 25, 20));
			}
			if (npc.type == NPCID.Shark)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<DepthBlade>(), 15, 10));
			}
			if (npc.type == NPCID.Vulture)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<DesertFeather>(), 1, 1, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DesertFeather>(), 1));
			}
			if (npc.type == NPCID.SkeletonSniper || npc.type == NPCID.TacticalSkeleton || npc.type == NPCID.SkeletonCommando || npc.type == NPCID.Paladin ||
			   npc.type == NPCID.GiantCursedSkull || npc.type == NPCID.BoneLee || npc.type == NPCID.DiabolistWhite || npc.type == NPCID.DiabolistRed ||
			   npc.type == NPCID.NecromancerArmored || npc.type == NPCID.Necromancer || npc.type == NPCID.RaggedCasterOpenCoat || npc.type == NPCID.RaggedCaster ||
			   npc.type == NPCID.HellArmoredBonesSword || npc.type == NPCID.HellArmoredBonesMace || npc.type == NPCID.HellArmoredBonesSpikeShield ||
			   npc.type == NPCID.HellArmoredBones || npc.type == NPCID.BlueArmoredBonesSword || npc.type == NPCID.BlueArmoredBonesNoPants ||
			   npc.type == NPCID.BlueArmoredBonesMace || npc.type == NPCID.BlueArmoredBones || npc.type == NPCID.RustyArmoredBonesSwordNoArmor ||
			   npc.type == NPCID.RustyArmoredBonesSword || npc.type == NPCID.RustyArmoredBonesFlail || npc.type == NPCID.RustyArmoredBonesAxe)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<Ectoblood>(), 1, 1, 2));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Ectoblood>(), 1));
			}
			if (npc.type == NPCID.RedDevil || npc.type == NPCID.SeekerHead || npc.type == NPCID.IchorSticker)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<EssenceofChaos>(), 2, 1, 3));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EssenceofChaos>(), 2, 1, 2));
			}
			if (npc.type == NPCID.WyvernHead || npc.type == NPCID.AngryNimbus)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<EssenceofCinder>(), 2, 1, 3));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EssenceofCinder>(), 2, 1, 2));
			}
			if (npc.type == NPCID.IceTortoise || npc.type == NPCID.IcyMerman)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<EssenceofEleum>(), 2, 1, 3));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EssenceofEleum>(), 2, 1, 2));
			}
			if (npc.type == NPCID.PresentMimic)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<HolidayHalberd>(), 7, 5));
			}
			if (npc.type == NPCID.IchorSticker)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<IchorSpear>(), 25, 20));
			}
			if (npc.type == NPCID.Harpy)
			{
				LeadingConditionRule eoc = new LeadingConditionRule(new EyeCondition());
				eoc.OnSuccess(ItemDropRule.NormalvsExpert(ModContent.ItemType<SkyGlaze>(), 40, 30));
				npcLoot.Add(eoc);
			}
			if (npc.type == NPCID.Plantera)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<LivingShard>(), 1, 8, 11));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<LivingShard>(), 1, 6, 9));
			}
			if (npc.type == NPCID.Antlion || npc.type == NPCID.WalkingAntlion || npc.type == NPCID.FlyingAntlion)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MandibleBow>(), 40, 30));
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MandibleClaws>(), 40, 30));
			}
			if (npc.type == NPCID.NebulaBrain || npc.type == NPCID.NebulaSoldier || npc.type == NPCID.NebulaHeadcrab || npc.type == NPCID.NebulaBeast)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<MeldBlob>(), 4, 1, 3));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MeldBlob>(), 4, 1, 2));
			}
			if (npc.type == NPCID.CultistBoss)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<MeldBlob>(), 1, 20, 25));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MeldBlob>(), 1, 25, 30));
			}
			if (npc.type == NPCID.WallofFlesh)
			{
				npcLoot.Add(new CommonDrop(ModContent.ItemType<Meowthrower>(), 5));
			}
			if (npc.type == NPCID.MartianSaucerCore)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<NullificationRifle>(), 7, 5));
			}
			if (npc.type == NPCID.Demon)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BladecrestOathsword>(), 25, 20));
			}
			if (npc.type == NPCID.BoneSerpentHead)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<OldLordOathsword>(), 15, 10));
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<DemonicBoneAsh>(), 3, 2));
			}
			if (npc.type == NPCID.MoonLordCore)
			{
				npcLoot.Add(new CommonDrop(ModContent.ItemType<MLGRune2>(), 1));
			}
			if (npc.type == NPCID.Tim)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PlasmaRod>(), 7, 5));
			}
			if (npc.type == NPCID.GoblinSorcerer)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PlasmaRod>(), 25, 20));
			}
			if (npc.type == NPCID.PirateDeadeye)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ProporsePistol>(), 25, 20));
			}
			if (npc.type == NPCID.PirateCrossbower)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<RaidersGlory>(), 25, 20));
			}
			if (npc.type == NPCID.CultistBoss)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<StardustStaff>(), 7, 5));
			}
			if (npc.type == NPCID.EyeofCthulhu)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<TeardropCleaver>(), 7, 5));
			}
			if (npc.type == NPCID.GoblinSummoner)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<TheFirstShadowflame>(), 7, 5));
			}
			if (npc.type == NPCID.WallofFlesh)
			{
				npcLoot.Add(new CommonDrop(ModContent.ItemType<MLGRune>(), 1));
			}
			if (npc.type == NPCID.EyeofCthulhu)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<VictoryShard>(), 1, 3, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<VictoryShard>(), 1, 2, 4));
			}
			if (npc.type == NPCID.SandElemental)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<WifeinaBottle>(), 7, 5));
				npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<WifeinaBottlewithBoobs>(), 20));
			}
			if (npc.type == NPCID.Skeleton || npc.type == NPCID.ArmoredSkeleton)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Waraxe>(), 25, 20));
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<AncientBoneDust>(), 5, 4));
			}
			if (npc.type == NPCID.GoblinWarrior)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Warblade>(), 25, 20));
			}
			if (npc.type == NPCID.MartianWalker)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Wingman>(), 7, 5));
			}
			if (npc.type == NPCID.GiantCursedSkull || npc.type == NPCID.NecromancerArmored || npc.type == NPCID.Necromancer)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<WrathoftheAncients>(), 25, 20));
			}
			if (npc.type == NPCID.DevourerHead || npc.type == NPCID.SeekerHead)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<FetidEssence>(), 5, 4));
			}
			if (npc.type == NPCID.Herpling || npc.type == NPCID.FaceMonster)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BloodlettingEssence>(), 5, 4));
			}
			if (npc.type == NPCID.ManEater)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ManeaterBulb>(), 3, 2));
			}
			if (npc.type == NPCID.AngryTrapper)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ManeaterBulb>(), 5, 4));
			}
			if (npc.type == NPCID.MotherSlime || npc.type == NPCID.CorruptSlime || npc.type == NPCID.Crimslime)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MurkySludge>(), 5, 4));
			}
			if (npc.type == NPCID.Moth)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<GypsyPowder>(), 2, 1));
			}
			if (npc.type == NPCID.Derpling)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BeetleJuice>(), 5, 4));
			}
			if (npc.type == NPCID.SpikedJungleSlime || npc.type == NPCID.Arapaima)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MurkyPaste>(), 5, 4));
			}
			if (npc.type == NPCID.IceQueen)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new YharonCondition(), ModContent.ItemType<EndothermicEnergy>(), 1, 10, 20));
			}
			if (npc.type == NPCID.Pumpking)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new YharonCondition(), ModContent.ItemType<NightmareFuel>(), 1, 10, 20));
			}
			if (npc.type == NPCID.Mothron)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new YharonCondition(), ModContent.ItemType<DarksunFragment>(), 1, 3, 5));
			}
			if (npc.type == ModContent.NPCType<Calamitas.CalamitasRun3>())
			{
				npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), ModContent.ItemType<Animosity>()));
				npcLoot.Add(ItemDropRule.ByCondition(new EclipseCondition(), ItemID.BrokenHeroSword));
			}
			if (npc.type == ModContent.NPCType<Cryogen.Cryogen>())
			{
				npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), ModContent.ItemType<FrostFlare>()));
			}
			if (npc.type == ModContent.NPCType<SupremeCalamitas.SupremeCalamitas>())
			{
				npcLoot.Add(ItemDropRule.ByCondition(new RevCondition(), ModContent.ItemType<Vehemenc>()));
			}
		}

		public static void AddLegendaryBossDrop(ref NPCLoot npcLoot, ref NPC npc, int boss, int itemType)
		{
			LeadingConditionRule notStatue = new LeadingConditionRule(new Conditions.NotFromStatue());
			LeadingConditionRule preHm = new LeadingConditionRule(new Conditions.IsPreHardmode());
			LeadingConditionRule hm = new LeadingConditionRule(new Conditions.IsHardmode());
			LeadingConditionRule postML = new LeadingConditionRule(new MoonCondition());
			LeadingConditionRule expert = new LeadingConditionRule(new Conditions.IsExpert());
			if (npc.type == boss)
			{
				notStatue.OnSuccess(preHm.OnSuccess(expert.OnFailedRoll(new CommonDrop(itemType, 10000))));
				notStatue.OnSuccess(hm.OnSuccess(expert.OnFailedRoll(new CommonDrop(itemType, 5000))));
				notStatue.OnSuccess(postML.OnSuccess(expert.OnFailedRoll(new CommonDrop(itemType, 4000))));
				notStatue.OnSuccess(preHm.OnSuccess(expert.OnSuccess(new CommonDrop(itemType, 9000))));
				notStatue.OnSuccess(hm.OnSuccess(expert.OnSuccess(new CommonDrop(itemType, 4500))));
				notStatue.OnSuccess(postML.OnSuccess(expert.OnSuccess(new CommonDrop(itemType, 3700))));
			}
			npcLoot.Add(notStatue);
		}

		public override void OnKill(NPC npc)
		{
			#region stuff ive finished
			bool revenge = CalamityWorld.revenge;
			if (npc.type == Mod.Find<ModNPC>("PhantomSpirit").Type && !NPC.AnyNPCs(Mod.Find<ModNPC>("Polterghast").Type))
			{
				CalamityModClassic1Point2.ghostKillCount++;
				if (CalamityModClassic1Point2.ghostKillCount >= 30)
				{
					NPC.SpawnOnPlayer(npc.FindClosestPlayer(), Mod.Find<ModNPC>("Polterghast").Type);
					CalamityModClassic1Point2.ghostKillCount = 0;
				}
			}
			if (npc.type == Mod.Find<ModNPC>("DesertScourgeHead").Type)
			{
				Vector2 center = Main.player[npc.target].Center;
				float num2 = 1E+08f;
				Vector2 position2 = npc.position;
				for (int k = 0; k < 200; k++)
				{
					if (Main.npc[k].active && (Main.npc[k].type == Mod.Find<ModNPC>("DesertScourgeHead").Type || Main.npc[k].type == Mod.Find<ModNPC>("DesertScourgeBody").Type || Main.npc[k].type == Mod.Find<ModNPC>("DesertScourgeTail").Type))
					{
						float num3 = Math.Abs(Main.npc[k].Center.X - center.X) + Math.Abs(Main.npc[k].Center.Y - center.Y);
						if (num3 < num2)
						{
							num2 = num3;
							position2 = Main.npc[k].position;
						}
					}
				}
				npc.position = position2;
				CalamityWorld.downedDesertScourge = true;
			}
			if (npc.type == Mod.Find<ModNPC>("AstrumDeusHead").Type)
			{
				string key = "The seal of the stars has been broken!  You can now mine Astral Ore!";
				Color messageColor = Color.Gold;
				if (!CalamityWorld.downedStarGod)
				{
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText((key), messageColor);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
					}
				}
				CalamityWorld.downedStarGod = true;
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				Vector2 center = Main.player[npc.target].Center;
				float num2 = 1E+08f;
				Vector2 position2 = npc.position;
				for (int k = 0; k < 200; k++)
				{
					if (Main.npc[k].active && (Main.npc[k].type == Mod.Find<ModNPC>("AstrumDeusHead").Type || Main.npc[k].type == Mod.Find<ModNPC>("AstrumDeusBody").Type || Main.npc[k].type == Mod.Find<ModNPC>("AstrumDeusTail").Type))
					{
						float num3 = Math.Abs(Main.npc[k].Center.X - center.X) + Math.Abs(Main.npc[k].Center.Y - center.Y);
						if (num3 < num2)
						{
							num2 = num3;
							position2 = Main.npc[k].position;
						}
					}
				}
				npc.position = position2;
			}
			if (npc.type == Mod.Find<ModNPC>("DevourerofGodsHead").Type)
			{
				int bossType = Mod.Find<ModNPC>("DevourerofGodsHead").Type;
				if (NPC.CountNPCS(bossType) < 2)
				{
					string key = "The frigid moon shimmers brightly";
					Color messageColor = Color.Cyan;
					string key2 = "The harvest moon glows eerily";
					Color messageColor2 = Color.Orange;
					if (!CalamityWorld.downedDoG)
					{
						if (Main.netMode == NetmodeID.SinglePlayer)
						{
							Main.NewText((key), messageColor);
							Main.NewText((key2), messageColor2);
						}
						else if (Main.netMode == NetmodeID.Server)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
							ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key2), messageColor2);
						}
					}
					CalamityWorld.downedDoG = true;
					Vector2 center = Main.player[npc.target].Center;
					float num2 = 1E+08f;
					Vector2 position2 = npc.position;
					for (int k = 0; k < 200; k++)
					{
						if (Main.npc[k].active && (Main.npc[k].type == Mod.Find<ModNPC>("DevourerofGodsHead").Type || Main.npc[k].type == Mod.Find<ModNPC>("DevourerofGodsBody").Type || Main.npc[k].type == Mod.Find<ModNPC>("DevourerofGodsTail").Type))
						{
							float num3 = Math.Abs(Main.npc[k].Center.X - center.X) + Math.Abs(Main.npc[k].Center.Y - center.Y);
							if (num3 < num2)
							{
								num2 = num3;
								position2 = Main.npc[k].position;
							}
						}
					}
				}
				else
				{
					npc.value = 0f;
					npc.boss = false;
				}
			}
			if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().tarraSet)
			{
				if (!npc.SpawnedFromStatue && npc.damage > 0 && Main.rand.NextBool(5))
				{
					Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 58, 1, false, 0, false, false);
				}
			}
			if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().bloodflareSet)
			{
				if (!npc.SpawnedFromStatue && npc.damage > 0 && Main.rand.NextBool(5) && Main.bloodMoon && (Main.player[npc.target].ZoneOverworldHeight || Main.player[npc.target].ZoneSkyHeight))
				{
					Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("BloodOrb").Type);
				}
			}
			if (NPC.downedMoonlord)
			{
				if (!npc.SpawnedFromStatue && npc.damage > 0 && Main.rand.NextBool(25) && Main.bloodMoon && (Main.player[npc.target].ZoneOverworldHeight || Main.player[npc.target].ZoneSkyHeight))
				{
					Item.NewItem(npc.GetSource_FromThis(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("BloodOrb").Type);
				}
			}
			#endregion
			#region Boss Specials
			if (npc.boss)
			{
				CalamityWorld.downedBossAny = true;
			}
			if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail || npc.type == NPCID.BrainofCthulhu)
			{
				if (npc.boss)
				{
					bool downedEvil = CalamityWorld.downedWhar;
					CalamityWorld.downedWhar = true;
					string key = WorldGen.crimson ? "Bloody cysts are erupting from the crimson's flesh..." : "Rotten cysts are oozing from the corrupt land...";
					Color messageColor = WorldGen.crimson ? Color.Crimson : Color.Violet;
					if (!downedEvil)
					{
						if (Main.netMode == NetmodeID.SinglePlayer)
						{
							Main.NewText(key, messageColor);
						}
						else if (Main.netMode == NetmodeID.Server)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
						}
					}
				}
			}
			if (npc.type == NPCID.SkeletronHead)
			{
				bool downedSkull = CalamityWorld.downedSkullHead;
				CalamityWorld.downedSkullHead = true;
				Color messageColor = Color.SpringGreen;
				if (!downedSkull)
				{
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText("Blighted sludge spreads throughout the world's evil...", messageColor);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Blighted sludge spreads throughout the world's evil..."), messageColor);
					}
				}
			}
			if (npc.type == NPCID.WallofFlesh)
			{
				bool hardMode = CalamityWorld.downedUgly;
				CalamityWorld.downedUgly = true;
				string key2 = "The ancient ice spirits have been unbound!";
				Color messageColor = Color.Crimson;
				string key = "Calamitous creatures now roam free!";
				Color messageColor2 = Color.Cyan;
				if (!hardMode)
				{
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText(key, messageColor);
						Main.NewText(key2, messageColor2);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key2), messageColor2);
					}
				}
			}
			if (npc.type == NPCID.SkeletronPrime)
			{
				bool downedPrime = CalamityWorld.downedSkeletor;
				CalamityWorld.downedSkeletor = true;
				string key = "A blood red inferno lingers in the night...";
				Color messageColor = Color.Crimson;
				if (!downedPrime)
				{
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText(key, messageColor);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
					}
				}
			}
			if (npc.type == NPCID.Plantera)
			{
				bool downedPlant = CalamityWorld.downedPlantThing;
				CalamityWorld.downedPlantThing = true;
				string key = "The ocean depths are trembling";
				Color messageColor = Color.RoyalBlue;
				if (!downedPlant)
				{
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText(key, messageColor);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
					}
				}
			}
			if (npc.type == NPCID.Golem)
			{
				bool downedIdiot = CalamityWorld.downedGolemBaby;
				CalamityWorld.downedGolemBaby = true;
				string key = "A plague has befallen the Jungle";
				Color messageColor = Color.Lime;
				string key2 = "An ancient automaton roams the land";
				Color messageColor2 = Color.Yellow;
				if (!downedIdiot)
				{
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText(key, messageColor);
						Main.NewText(key2, messageColor2);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key2), messageColor2);
					}
				}
			}
			if (npc.type == NPCID.MoonLordCore)
			{
				bool downedMoonDude = CalamityWorld.downedMoonDude;
				CalamityWorld.downedMoonDude = true;
				string key = "The profaned flame blazes fiercely!";
				Color messageColor = Color.Orange;
				string key2 = "Cosmic terrors are watching...";
				Color messageColor2 = Color.Violet;
				string key3 = "The bloody moon beckons...";
				Color messageColor3 = Color.Crimson;
				string key4 = "The Curse of the Eldritch consumes your vitality";
				Color messageColor4 = Color.Cyan;
				if (!downedMoonDude)
				{
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText((key), messageColor);
						Main.NewText((key2), messageColor2);
						Main.NewText((key3), messageColor3);
						Main.NewText((key4), messageColor4);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key2), messageColor2);
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key3), messageColor3);
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key4), messageColor4);
					}
				}
			}
			if (npc.type == NPCID.DD2Betsy)
			{
				CalamityWorld.downedBetsy = true;
			}
			if (npc.type == NPCID.WallofFlesh)
			{
				if (!CalamityWorld.spawnAstralMeteor)
				{
					string key = "A star has fallen from the heavens!";
					Color messageColor = Color.Gold;
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText((key), messageColor);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
					}
					CalamityWorld.spawnAstralMeteor = true;
					CalamityWorld.dropAstralMeteor();
				}
				else if (Main.rand.NextBool(2) && !CalamityWorld.spawnAstralMeteor2)
				{
					string key = "A star has fallen from the heavens!";
					Color messageColor = Color.Gold;
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText((key), messageColor);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
					}
					CalamityWorld.spawnAstralMeteor2 = true;
					CalamityWorld.dropAstralMeteor();
				}
				else if (Main.rand.NextBool(4) && !CalamityWorld.spawnAstralMeteor3)
				{
					string key = "A star has fallen from the heavens!";
					Color messageColor = Color.Gold;
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText((key), messageColor);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
					}
					CalamityWorld.spawnAstralMeteor3 = true;
					CalamityWorld.dropAstralMeteor();
				}
			}
			if (npc.type == Mod.Find<ModNPC>("Astrageldon").Type)
			{
				string key = "A star has fallen from the heavens!";
				Color messageColor = Color.Gold;
				if (Main.netMode == NetmodeID.SinglePlayer)
				{
					Main.NewText((key), messageColor);
				}
				else if (Main.netMode == NetmodeID.Server)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
				}
				CalamityWorld.dropAstralMeteor();
			}
			if (npc.type == Mod.Find<ModNPC>("HiveMindP2").Type) //boss 2
			{
				CalamityWorld.downedHiveMind = true;
			}
			if (npc.type == Mod.Find<ModNPC>("PerforatorHeadLarge").Type) //boss 3
			{
				CalamityWorld.downedPerforator = true;
			}
			if (npc.type == Mod.Find<ModNPC>("SlimeGodCore").Type) //boss 4
			{
				CalamityWorld.downedSlimeGod = true;
			}
			if (npc.type == Mod.Find<ModNPC>("Cryogen").Type) //boss 5
			{
				CalamityWorld.downedCryogen = true;
			}
			if (npc.type == Mod.Find<ModNPC>("BrimstoneElemental").Type) //boss 6
			{
				CalamityWorld.downedBrimstoneElemental = true;
			}
			if (npc.type == Mod.Find<ModNPC>("CalamitasRun3").Type) //boss 7
			{
				CalamityWorld.downedCalamitas = true;
			}
			if (npc.type == Mod.Find<ModNPC>("Siren").Type || npc.type == Mod.Find<ModNPC>("Leviathan").Type) //boss 8
			{
				int bossType = (npc.type == Mod.Find<ModNPC>("Siren").Type) ? Mod.Find<ModNPC>("Leviathan").Type : Mod.Find<ModNPC>("Siren").Type;
				if (!NPC.AnyNPCs(bossType))
				{
					CalamityWorld.downedLeviathan = true;
				}
			}
			if (npc.type == Mod.Find<ModNPC>("PlaguebringerGoliath").Type) //boss 9
			{
				CalamityWorld.downedPlaguebringer = true;
			}
			if (npc.type == Mod.Find<ModNPC>("ProfanedGuardianBoss").Type) //boss 10
			{
				CalamityWorld.downedGuardians = true;
			}
			if (npc.type == Mod.Find<ModNPC>("Providence").Type) //boss 11
			{
				string key = "Shrieks are echoing from the dungeon";
				Color messageColor = Color.Cyan;
				string key2 = "The calamitous beings have been inundated with bloodstone";
				Color messageColor2 = Color.Orange;
				if (!CalamityWorld.downedProvidence)
				{
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText((key), messageColor);
						Main.NewText((key2), messageColor2);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key2), messageColor2);
					}
				}
				CalamityWorld.downedProvidence = true;
			}
			if (npc.type == Mod.Find<ModNPC>("CeaselessVoid").Type) //boss 12
			{
				CalamityWorld.downedSentinel1 = true;
			}
			if (npc.type == Mod.Find<ModNPC>("StormWeaverHeadNaked").Type) //boss 13
			{
				CalamityWorld.downedSentinel2 = true;
			}
			if (npc.type == Mod.Find<ModNPC>("CosmicWraith").Type) //boss 14
			{
				CalamityWorld.downedSentinel3 = true;
			}
			if (npc.type == Mod.Find<ModNPC>("Bumblefuck").Type) //boss 16
			{
				CalamityWorld.downedBumble = true;
			}
			if (npc.type == Mod.Find<ModNPC>("Yharon").Type) //boss 17
			{
				string key = "The dark sun awaits";
				Color messageColor = Color.Orange;
				if (!CalamityWorld.downedYharon)
				{
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText((key), messageColor);
					}
					else if (Main.netMode == NetmodeID.Server)
					{
						ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
					}
				}
				CalamityWorld.downedYharon = true;
			}
			if (npc.type == Mod.Find<ModNPC>("SupremeCalamitas").Type) //boss 18
			{
				CalamityWorld.downedSCal = true;
			}
			if (npc.type == Mod.Find<ModNPC>("CrabulonIdle").Type) //boss 19
			{
				CalamityWorld.downedCrabulon = true;
			}
			if (npc.type == Mod.Find<ModNPC>("ScavengerBody").Type) //boss 20
			{
				CalamityWorld.downedScavenger = true;
			}
			if (npc.type == Mod.Find<ModNPC>("Polterghast").Type) //boss 21
			{
				CalamityWorld.downedPolterghast = true;
			}
			#endregion
		}

		public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
			if ((player.ZoneOverworldHeight || player.ZoneSkyHeight) && NPC.downedMoonlord && Main.raining)
			{
				spawnRate = (int)((double)spawnRate * 0.75);
				maxSpawns = (int)((float)maxSpawns * 1.25f);
			}
			if (player.GetModPlayer<CalamityPlayer>().ZoneCalamity)
			{
				spawnRate = (int)((double)spawnRate * 0.5);
				maxSpawns = (int)((float)maxSpawns * 1.5f);
			}
			if (player.GetModPlayer<CalamityPlayer>().ZoneAstral)
			{
				spawnRate = (int)((double)spawnRate * 0.4);
				maxSpawns = (int)((float)maxSpawns * 1.1f);
			}
			if (CalamityWorld.revenge)
			{
				spawnRate = (int)((double)spawnRate * 0.85);
			}
			if (CalamityWorld.demonMode)
			{
				spawnRate = (int)((double)spawnRate * 0.75);
			}
			if (player.GetModPlayer<CalamityPlayer>().zerg)
			{
				spawnRate = (int)((double)spawnRate * 0.01);
				maxSpawns = (int)((float)maxSpawns * 5f);
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (bFlames)
			{
				if (Main.rand.Next(4) < 4)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("BrimstoneFlame").Type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
				Lighting.AddLight(npc.position, 0.05f, 0.01f, 0.01f);
			}
			if (aFlames)
			{
				drawColor = Color.Black;
				if (Main.rand.Next(4) < 4)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("BrimstoneFlame").Type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.25f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.35f;
					}
				}
				Lighting.AddLight(npc.position, 0.025f, 0f, 0f);
			}
			if (pShred)
			{
				if (Main.rand.Next(3) < 4)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Blood, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.1f;
					Main.dust[dust].velocity.Y += 0.25f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
			}
			if (hFlames)
			{
				if (Main.rand.Next(4) < 4)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("HolyFlame").Type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
				Lighting.AddLight(npc.position, 0.25f, 0.25f, 0.1f);
			}
			if (pFlames)
			{
				if (Main.rand.Next(4) < 4)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.GemEmerald, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.2f;
					Main.dust[dust].velocity.Y -= 0.15f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
				Lighting.AddLight(npc.position, 0.07f, 0.15f, 0.01f);
			}
			if (gsInferno)
			{
				if (Main.rand.Next(4) < 4)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.ShadowbeamStaff, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.2f;
					Main.dust[dust].velocity.Y -= 0.15f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
				Lighting.AddLight(npc.position, 0.1f, 0f, 0.135f);
			}
			if (nightwither)
			{
				Rectangle hitbox = npc.Hitbox;
				if (Main.rand.Next(4) < 4)
				{
					int num3 = Utils.SelectRandom<int>(Main.rand, new int[]
					{
						173,
						27,
						234
					});
					int num4 = Dust.NewDust(hitbox.TopLeft(), npc.width, npc.height, num3, 0f, -2.5f, 0, default(Color), 1f);
					Main.dust[num4].noGravity = true;
					Main.dust[num4].alpha = 200;
					Main.dust[num4].velocity.Y -= 0.2f;
					Dust dust = Main.dust[num4];
					dust.velocity *= 1.2f;
					dust = Main.dust[num4];
					dust.scale += Main.rand.NextFloat();
				}
			}
			if (tSad || cDepth)
			{
				if (Main.rand.Next(6) < 3)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Water, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = false;
					Main.dust[dust].velocity *= 1.2f;
					Main.dust[dust].velocity.Y += 0.15f;
					if (Main.rand.NextBool(4))
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
			}
			if (gState || eFreeze)
			{
				drawColor = Color.Cyan;
			}
			if (marked)
			{
				drawColor = Color.Fuchsia;
			}
		}

		public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayer>().stressLevel500 && npc.type != NPCID.BrainofCthulhu && npc.type != Mod.Find<ModNPC>("Yharon").Type)
			{
				Microsoft.Xna.Framework.Color color9 = Lighting.GetColor((int)((double)npc.position.X + (double)npc.width * 0.5) / 16, (int)(((double)npc.position.Y + (double)npc.height * 0.5) / 16.0));
				float num66 = 0f;
				Vector2 vector11 = new Vector2((float)(TextureAssets.Npc[npc.type].Value.Width / 2), (float)(TextureAssets.Npc[npc.type].Value.Height / Main.npcFrameCount[npc.type] / 2));
				SpriteEffects spriteEffects = SpriteEffects.None;
				if (npc.spriteDirection == 1)
				{
					spriteEffects = SpriteEffects.FlipHorizontally;
				}
				Microsoft.Xna.Framework.Rectangle frame6 = npc.frame;
				Microsoft.Xna.Framework.Color alpha15 = npc.GetAlpha(color9);
				float num212 = 1f - (float)npc.life / (float)npc.lifeMax;
				num212 *= num212;
				alpha15.R = (byte)((float)alpha15.R * num212);
				alpha15.G = (byte)((float)alpha15.G * num212);
				alpha15.B = (byte)((float)alpha15.B * num212);
				alpha15.A = (byte)((float)alpha15.A * num212);
				for (int num213 = 0; num213 < 4; num213++)
				{
					Vector2 position9 = npc.position;
					float num214 = Math.Abs(npc.Center.X - Main.player[Main.myPlayer].Center.X);
					float num215 = Math.Abs(npc.Center.Y - Main.player[Main.myPlayer].Center.Y);
					if (num213 == 0 || num213 == 2)
					{
						position9.X = Main.player[Main.myPlayer].Center.X + num214;
					}
					else
					{
						position9.X = Main.player[Main.myPlayer].Center.X - num214;
					}
					position9.X -= (float)(npc.width / 2);
					if (num213 == 0 || num213 == 1)
					{
						position9.Y = Main.player[Main.myPlayer].Center.Y + num215;
					}
					else
					{
						position9.Y = Main.player[Main.myPlayer].Center.Y - num215;
					}
					position9.Y -= (float)(npc.height / 2);
					Main.spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, new Vector2(position9.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)TextureAssets.Npc[npc.type].Value.Width * npc.scale / 2f + vector11.X * npc.scale, position9.Y - Main.screenPosition.Y + (float)npc.height - (float)TextureAssets.Npc[npc.type].Value.Height * npc.scale / (float)Main.npcFrameCount[npc.type] + 4f + vector11.Y * npc.scale + num66 + npc.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), alpha15, npc.rotation, vector11, npc.scale, spriteEffects, 0f);
				}
				Main.spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)TextureAssets.Npc[npc.type].Value.Width * npc.scale / 2f + vector11.X * npc.scale, npc.position.Y - Main.screenPosition.Y + (float)npc.height - (float)TextureAssets.Npc[npc.type].Value.Height * npc.scale / (float)Main.npcFrameCount[npc.type] + 4f + vector11.Y * npc.scale + num66 + npc.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), npc.GetAlpha(color9), npc.rotation, vector11, npc.scale, spriteEffects, 0f);
				return false;
			}
			return true;
		}

		public override void ModifyShop(NPCShop shop)
		{
			if (shop.NpcType == NPCID.Merchant)
			{
				shop.Add(new NPCShop.Entry(ItemID.Flare, new Condition("", () => Main.LocalPlayer.HasItem(ModContent.ItemType<SpectralstormCannon>()) || Main.LocalPlayer.HasItem(ModContent.ItemType<FirestormCannon>()))));
			}
			if (shop.NpcType == NPCID.ArmsDealer)
			{
				shop.Add(new NPCShop.Entry(ItemID.Stake, new Condition("", () => Main.LocalPlayer.HasItem(ModContent.ItemType<Impaler>()))));
			}
			if (shop.NpcType == NPCID.TravellingMerchant)
			{
				shop.Add(new NPCShop.Entry(ModContent.ItemType<FrostBarrier>(), Condition.MoonPhaseNew));
			}
        }
        public void LaceratorEffects(ref NPC npc)
        {
            if (!npc.GetGlobalNPC<CalamityGlobalNPC>().lacerator)
            {
                return;
            }
            int num = 1100;
            for (int i = 0; i < 255; i++)
            {
                if (!Main.player[i].active || Main.player[i].dead)
                {
                    continue;
                }
                Vector2 val = npc.Center - Main.player[i].position;
                if (val.Length() < (float)num && Main.player[i].itemAnimation > 0)
                {
                    if (i == Main.myPlayer)
                    {
                        Main.player[i].soulDrain++;
                    }
                    if (Main.rand.Next(3) != 0)
                    {
                        Vector2 center = npc.Center;
                        center.X += (float)Main.rand.Next(-100, 100) * 0.05f;
                        center.Y += (float)Main.rand.Next(-100, 100) * 0.05f;
                        center += npc.velocity;
                        int num2 = Dust.NewDust(center, 1, 1, 235);
                        Dust obj = Main.dust[num2];
                        obj.velocity *= 0f;
                        Main.dust[num2].scale = (float)Main.rand.Next(70, 85) * 0.01f;
                        Main.dust[num2].fadeIn = i + 1;
                    }
                }
            }
        }
    }
    public class ProvidenceDowned : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return CalamityWorld.downedProvidence;
        }
        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class RevCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return CalamityWorld.revenge;
        }
        public bool CanShowItemDropInUI()
        {
            return CalamityWorld.revenge;
        }

        public string GetConditionDescription()
        {
            return "This is a Revengeance Mode drop rate";
        }
    }
    public class MoonCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return NPC.downedMoonlord;
        }
        public bool CanShowItemDropInUI()
        {
            return NPC.downedMoonlord;
        }

        public string GetConditionDescription()
        {
            return "This is a Revengeance Mode drop rate";
        }
    }
    public class EyeCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return NPC.downedBoss1;
        }
        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "After beating the Eye of Cthulhu";
        }
    }
    public class YharonCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return CalamityWorld.downedYharon;
        }
        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "After beating Yharon";
        }
    }
    public class EclipseCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return Main.eclipse;
        }
        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "During a Solar Eclipse";
        }
    }
}