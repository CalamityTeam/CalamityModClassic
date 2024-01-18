using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.GameContent.Generation;
using CalamityModClassic1Point1.Tiles;
using CalamityModClassic1Point1;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point1.Items;
using CalamityModClassic1Point1.NPCs.Calamitas;
using CalamityModClassic1Point1.NPCs.DesertScourge;
using CalamityModClassic1Point1.NPCs.TheDevourerofGods;
using CalamityModClassic1Point1.Items.Weapons;
using CalamityModClassic1Point1.Items.DesertScourge;

namespace CalamityModClassic1Point1.NPCs
{
	public class CalamityGlobalNPC : GlobalNPC
	{
		public static bool bossBuff = false;
		public static bool superBossBuff = false;
		public static int holyBoss = -1;
		public static int doughnutBoss = -1;
		public static int voidBoss = -1;
        public bool bFlames = false;
        public bool hFlames = false;
        public bool pFlames = false;
        public bool gState = false;
        public bool aCrunch = false;
        public bool tSad = false;
        public bool pShred = false;
        public bool cDepth = false;
        public bool gsInferno = false;
        public override bool InstancePerEntity => true;

        public override void ResetEffects(NPC npc)
		{
            bFlames = false;
            hFlames = false;
            pFlames = false;
            gState = false;
            aCrunch = false;
            tSad = false;
            pShred = false;
            cDepth = false;
            gsInferno = false;
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            npc.defense = npc.defDefense;
            bool hardMode = Main.hardMode;
			int npcDefense = npc.defense;
			if (cDepth)
			{
				if (npcDefense < 0)
				{
					npcDefense = 0;
				}
				int depthDamage = hardMode ? 60 : 30;
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
		}
		
		public override void SetDefaults(NPC npc)
		{
			if (ModLoader.HasMod("ThoriumMod"))
			{
				if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("TheGrandThunderBird").Type ||
				    npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("ForgottenOne").Type ||
                    npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("QueenJelly").Type ||
					npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("GraniteEnergyStorm").Type ||
					npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("SlagFury").Type ||
					npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("Omnicide").Type ||
					npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("Aquaius").Type ||
					npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("StarScouter").Type ||
					npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("FallenBeholder").Type ||
                    npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("FallenBeholder2").Type ||
					npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("Lich").Type ||
					npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("LichHeadless").Type)
				{
					npc.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
					npc.buffImmune[Mod.Find<ModBuff>("TemporalSadness").Type] = true;
					npc.buffImmune[Mod.Find<ModBuff>("ArmorCrunch").Type] = true;
				}
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
			if (bossBuff && superBossBuff)
			{
				if (ModLoader.HasMod("ThoriumMod"))
				{
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("TheGrandThunderBird").Type)
					{
						npc.lifeMax += 7000;
						npc.damage += 100;
						npc.scale = 1.25f;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("ForgottenOne").Type)
					{
						npc.lifeMax += 26000;
						npc.damage += 160;
						npc.scale = 1.25f;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("QueenJelly").Type)
					{
						npc.lifeMax += 12000;
						npc.damage += 120;
						npc.scale = 1.25f;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("GraniteEnergyStorm").Type)
					{
						npc.lifeMax += 14000;
						npc.damage += 140;
						npc.scale = 1.25f;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("StarScouter").Type)
					{
						npc.lifeMax += 30000;
						npc.damage += 180;
						npc.scale = 1.25f;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("FallenBeholder").Type)
					{
						npc.lifeMax += 34000;
						npc.damage += 200;
						npc.scale = 1.25f;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("FallenBeholder2").Type)
					{
						npc.lifeMax += 17000;
						npc.damage += 240;
						npc.scale = 1.25f;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("Lich").Type)
					{
						npc.lifeMax += 50000;
						npc.damage += 320;
						npc.scale = 1.25f;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("LichHeadless").Type)
					{
						npc.lifeMax += 25000;
						npc.damage += 360;
						npc.scale = 1.25f;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("SlagFury").Type)
					{
						npc.lifeMax += 320000;
						npc.damage += 480;
						npc.scale = 1.25f;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("Omnicide").Type)
					{
						npc.lifeMax += 360000;
						npc.damage += 400;
						npc.scale = 1.25f;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("Aquaius").Type)
					{
						npc.lifeMax += 160000;
						npc.damage += 440;
						npc.scale = 1.25f;
					}
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
				    npc.type == Mod.Find<ModNPC>("SlimeGod").Type || 
				    npc.type == Mod.Find<ModNPC>("SlimeGodRun").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 10f);
					npc.damage = (int)(npc.damage * 4f);
				}
				if (npc.type == Mod.Find<ModNPC>("DesertScourgeHead").Type || 
				    npc.type == Mod.Find<ModNPC>("DesertScourgeBody").Type || 
				    npc.type == Mod.Find<ModNPC>("DesertScourgeTail").Type ||
				    npc.type == Mod.Find<ModNPC>("HiveMind").Type || 
				    npc.type == Mod.Find<ModNPC>("HiveMindP2").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 10f);
					npc.damage = (int)(npc.damage * 4f);
				}
				if (npc.type == Mod.Find<ModNPC>("PerforatorHeadSmall").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorBodySmall").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorTailSmall").Type ||
				    npc.type == Mod.Find<ModNPC>("PerforatorHeadMedium").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorBodyMedium").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorTailMedium").Type ||
				    npc.type == Mod.Find<ModNPC>("PerforatorHeadLarge").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorBodyLarge").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorTailLarge").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 10f);
					npc.damage = (int)(npc.damage * 4f);
				}
				if (npc.type == Mod.Find<ModNPC>("CryogenP1").Type || 
				    npc.type == Mod.Find<ModNPC>("CryogenP2").Type || 
				    npc.type == Mod.Find<ModNPC>("CryogenP3").Type ||
				    npc.type == Mod.Find<ModNPC>("CryogenP4").Type || 
				    npc.type == Mod.Find<ModNPC>("CryogenP5").Type || 
				    npc.type == Mod.Find<ModNPC>("CryogenP6").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 10f);
					npc.damage = (int)(npc.damage * 4f);
				}
				if (npc.type == NPCID.TheDestroyer ||
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
					npc.type == Mod.Find<ModNPC>("Calamitas").Type || 
				    npc.type == Mod.Find<ModNPC>("CalamitasRun").Type || 
				    npc.type == Mod.Find<ModNPC>("CalamitasRun2").Type ||
				    npc.type == Mod.Find<ModNPC>("CalamitasRun3").Type ||
				    npc.type == Mod.Find<ModNPC>("Siren").Type ||
				    npc.type == Mod.Find<ModNPC>("PlaguebringerGoliath").Type || 
				    npc.type == Mod.Find<ModNPC>("PlaguebringerShade").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 6f);
					npc.damage = (int)(npc.damage * 4f);
				}
				if (npc.type == Mod.Find<ModNPC>("Leviathan").Type)
				{
					npc.damage = (int)(npc.damage * 4f);
				}
				if (npc.type == Mod.Find<ModNPC>("DevourerofGodsHead").Type || 
				    npc.type == Mod.Find<ModNPC>("DevourerofGodsBody").Type || 
				    npc.type == Mod.Find<ModNPC>("DevourerofGodsTail").Type ||
				    npc.type == Mod.Find<ModNPC>("Yharon").Type ||
				    npc.type == NPCID.MoonLordHead ||
					npc.type == NPCID.MoonLordHand ||
					npc.type == NPCID.MoonLordCore)
				{
					npc.lifeMax = (int)(npc.lifeMax * 4f);
					npc.damage = (int)(npc.damage * 4f);
				}
			}
			else if (bossBuff)
			{
				if (ModLoader.HasMod("ThoriumMod"))
				{
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("TheGrandThunderBird").Type)
					{
						npc.lifeMax += 1000;
						npc.damage += 20;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("ForgottenOne").Type)
					{
						npc.lifeMax += 4000;
						npc.damage += 40;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("QueenJelly").Type)
					{
						npc.lifeMax += 3000;
						npc.damage += 30;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("GraniteEnergyStorm").Type)
					{
						npc.lifeMax += 3500;
						npc.damage += 35;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("StarScouter").Type)
					{
						npc.lifeMax += 7500;
						npc.damage += 45;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("FallenBeholder").Type)
					{
						npc.lifeMax += 8500;
						npc.damage += 50;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("FallenBeholder2").Type)
					{
						npc.lifeMax += 4250;
						npc.damage += 60;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("Lich").Type)
					{
						npc.lifeMax += 12500;
						npc.damage += 80;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("LichHeadless").Type)
					{
						npc.lifeMax += 6250;
						npc.damage += 90;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("SlagFury").Type)
					{
						npc.lifeMax += 80000;
						npc.damage += 120;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("Omnicide").Type)
					{
						npc.lifeMax += 90000;
						npc.damage += 100;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("Aquaius").Type)
					{
						npc.lifeMax += 40000;
						npc.damage += 110;
					}
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
				    npc.type == Mod.Find<ModNPC>("SlimeGod").Type || 
				    npc.type == Mod.Find<ModNPC>("SlimeGodRun").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 5f);
					npc.damage = (int)(npc.damage * 2f);
				}
				if (npc.type == Mod.Find<ModNPC>("DesertScourgeHead").Type || 
				    npc.type == Mod.Find<ModNPC>("DesertScourgeBody").Type || 
				    npc.type == Mod.Find<ModNPC>("DesertScourgeTail").Type ||
				    npc.type == Mod.Find<ModNPC>("HiveMind").Type || 
				    npc.type == Mod.Find<ModNPC>("HiveMindP2").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 6f);
					npc.damage = (int)(npc.damage * 2f);
				}
				if (npc.type == Mod.Find<ModNPC>("PerforatorHeadSmall").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorBodySmall").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorTailSmall").Type ||
				    npc.type == Mod.Find<ModNPC>("PerforatorHeadMedium").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorBodyMedium").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorTailMedium").Type ||
				    npc.type == Mod.Find<ModNPC>("PerforatorHeadLarge").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorBodyLarge").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorTailLarge").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 5f);
					npc.damage = (int)(npc.damage * 2f);
				}
				if (npc.type == Mod.Find<ModNPC>("CryogenP1").Type || 
				    npc.type == Mod.Find<ModNPC>("CryogenP2").Type || 
				    npc.type == Mod.Find<ModNPC>("CryogenP3").Type ||
				    npc.type == Mod.Find<ModNPC>("CryogenP4").Type || 
				    npc.type == Mod.Find<ModNPC>("CryogenP5").Type || 
				    npc.type == Mod.Find<ModNPC>("CryogenP6").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 6f);
					npc.damage = (int)(npc.damage * 2f);
				}
				if (npc.type == NPCID.TheDestroyer ||
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
					npc.type == Mod.Find<ModNPC>("Calamitas").Type || 
				    npc.type == Mod.Find<ModNPC>("CalamitasRun").Type || 
				    npc.type == Mod.Find<ModNPC>("CalamitasRun2").Type ||
				    npc.type == Mod.Find<ModNPC>("CalamitasRun3").Type ||
				    npc.type == Mod.Find<ModNPC>("Siren").Type ||
				    npc.type == Mod.Find<ModNPC>("PlaguebringerGoliath").Type || 
				    npc.type == Mod.Find<ModNPC>("PlaguebringerShade").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 3f);
					npc.damage = (int)(npc.damage * 2f);
				}
				if (npc.type == Mod.Find<ModNPC>("Leviathan").Type)
				{
					npc.damage = (int)(npc.damage * 2f);
				}
				if (npc.type == Mod.Find<ModNPC>("DevourerofGodsHead").Type || 
				    npc.type == Mod.Find<ModNPC>("DevourerofGodsBody").Type || 
				    npc.type == Mod.Find<ModNPC>("DevourerofGodsTail").Type ||
				    npc.type == Mod.Find<ModNPC>("Yharon").Type ||
				    npc.type == NPCID.MoonLordHead ||
					npc.type == NPCID.MoonLordHand ||
					npc.type == NPCID.MoonLordCore)
				{
					npc.lifeMax = (int)(npc.lifeMax * 2f);
					npc.damage = (int)(npc.damage * 2f);
				}
			}
			else if (superBossBuff)
			{
				if (ModLoader.HasMod("ThoriumMod"))
				{
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("TheGrandThunderBird").Type)
					{
						npc.lifeMax += 2000;
						npc.damage += 40;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("ForgottenOne").Type)
					{
						npc.lifeMax += 8000;
						npc.damage += 60;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("QueenJelly").Type)
					{
						npc.lifeMax += 6000;
						npc.damage += 50;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("GraniteEnergyStorm").Type)
					{
						npc.lifeMax += 7000;
						npc.damage += 60;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("StarScouter").Type)
					{
						npc.lifeMax += 15000;
						npc.damage += 60;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("FallenBeholder").Type)
					{
						npc.lifeMax += 17000;
						npc.damage += 80;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("FallenBeholder2").Type)
					{
						npc.lifeMax += 8500;
						npc.damage += 90;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("Lich").Type)
					{
						npc.lifeMax += 25000;
						npc.damage += 100;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("LichHeadless").Type)
					{
						npc.lifeMax += 12500;
						npc.damage += 110;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("SlagFury").Type)
					{
						npc.lifeMax += 160000;
						npc.damage += 180;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("Omnicide").Type)
					{
						npc.lifeMax += 180000;
						npc.damage += 150;
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("Aquaius").Type)
					{
						npc.lifeMax += 80000;
						npc.damage += 170;
					}
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
				    npc.type == Mod.Find<ModNPC>("SlimeGod").Type || 
				    npc.type == Mod.Find<ModNPC>("SlimeGodRun").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 7f);
					npc.damage = (int)(npc.damage * 3f);
				}
				if (npc.type == Mod.Find<ModNPC>("DesertScourgeHead").Type || 
				    npc.type == Mod.Find<ModNPC>("DesertScourgeBody").Type || 
				    npc.type == Mod.Find<ModNPC>("DesertScourgeTail").Type ||
				    npc.type == Mod.Find<ModNPC>("HiveMind").Type || 
				    npc.type == Mod.Find<ModNPC>("HiveMindP2").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 8f);
					npc.damage = (int)(npc.damage * 3f);
				}
				if (npc.type == Mod.Find<ModNPC>("PerforatorHeadSmall").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorBodySmall").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorTailSmall").Type ||
				    npc.type == Mod.Find<ModNPC>("PerforatorHeadMedium").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorBodyMedium").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorTailMedium").Type ||
				    npc.type == Mod.Find<ModNPC>("PerforatorHeadLarge").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorBodyLarge").Type || 
				    npc.type == Mod.Find<ModNPC>("PerforatorTailLarge").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 7f);
					npc.damage = (int)(npc.damage * 3f);
				}
				if (npc.type == Mod.Find<ModNPC>("CryogenP1").Type || 
				    npc.type == Mod.Find<ModNPC>("CryogenP2").Type || 
				    npc.type == Mod.Find<ModNPC>("CryogenP3").Type ||
				    npc.type == Mod.Find<ModNPC>("CryogenP4").Type || 
				    npc.type == Mod.Find<ModNPC>("CryogenP5").Type || 
				    npc.type == Mod.Find<ModNPC>("CryogenP6").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 9f);
					npc.damage = (int)(npc.damage * 3f);
				}
				if (npc.type == NPCID.TheDestroyer ||
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
					npc.type == Mod.Find<ModNPC>("Calamitas").Type || 
				    npc.type == Mod.Find<ModNPC>("CalamitasRun").Type || 
				    npc.type == Mod.Find<ModNPC>("CalamitasRun2").Type ||
				    npc.type == Mod.Find<ModNPC>("CalamitasRun3").Type ||
				    npc.type == Mod.Find<ModNPC>("Siren").Type ||
				    npc.type == Mod.Find<ModNPC>("PlaguebringerGoliath").Type || 
				    npc.type == Mod.Find<ModNPC>("PlaguebringerShade").Type)
				{
					npc.lifeMax = (int)(npc.lifeMax * 4f);
					npc.damage = (int)(npc.damage * 3f);
				}
				if (npc.type == Mod.Find<ModNPC>("Leviathan").Type)
				{
					npc.damage = (int)(npc.damage * 3f);
				}
				if (npc.type == Mod.Find<ModNPC>("DevourerofGodsHead").Type || 
				    npc.type == Mod.Find<ModNPC>("DevourerofGodsBody").Type || 
				    npc.type == Mod.Find<ModNPC>("DevourerofGodsTail").Type ||
				    npc.type == Mod.Find<ModNPC>("Yharon").Type ||
				    npc.type == NPCID.MoonLordHead ||
					npc.type == NPCID.MoonLordHand ||
					npc.type == NPCID.MoonLordCore)
				{
					npc.lifeMax = (int)(npc.lifeMax * 3f);
					npc.damage = (int)(npc.damage * 3f);
				}
			}
		}
		
		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if (Main.expertMode)
			{
				if (npc.type == NPCID.TheDestroyer ||
					npc.type == NPCID.TheDestroyerBody ||
					npc.type == NPCID.TheDestroyerTail)
				{
					if (projectile.penetrate == -1)
					{
						modifiers.FinalDamage /= 5;
					}
					else if (projectile.penetrate >= 4)
					{
                        modifiers.FinalDamage /= 4;
					}
					else if (projectile.penetrate == 3)
					{
                        modifiers.FinalDamage /= 3;
					}
					else if (projectile.penetrate == 2)
					{
                        modifiers.FinalDamage /= 2;
					}
				}
			}
			if (bossBuff)
			{
				if (npc.type == NPCID.EaterofWorldsHead || 
				    npc.type == NPCID.EaterofWorldsBody || 
				    npc.type == NPCID.EaterofWorldsTail)
				{
					if (projectile.penetrate == -1)
					{
                        modifiers.FinalDamage /= 5;
					}
					else if (projectile.penetrate >= 4)
					{
                        modifiers.FinalDamage /= 4;
					}
					else if (projectile.penetrate == 3)
					{
                        modifiers.FinalDamage /= 3;
					}
					else if (projectile.penetrate == 2)
					{
                        modifiers.FinalDamage /= 2;
					}
				}
				if (ModLoader.HasMod("ThoriumMod"))
				{
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("ForgottenOne").Type)
					{
						if (projectile.CountsAsClass(DamageClass.Melee))
						{
                            modifiers.FinalDamage /= 3;
						}
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("GraniteEnergyStorm").Type)
					{
						if (projectile.CountsAsClass(DamageClass.Magic))
						{
                            modifiers.FinalDamage /= 3;
						}
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("StarScouter").Type)
					{
						if (projectile.CountsAsClass(DamageClass.Ranged) || projectile.CountsAsClass(DamageClass.Melee))
						{
                            modifiers.FinalDamage /= 2;
						}
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("FallenBeholder").Type)
					{
						if (projectile.CountsAsClass(DamageClass.Magic) || projectile.minion)
						{
                            modifiers.FinalDamage /= 2;
						}
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("FallenBeholder2").Type)
					{
						if (projectile.CountsAsClass(DamageClass.Magic) || projectile.minion)
						{
                            modifiers.FinalDamage /= 2;
						}
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("Lich").Type)
					{
						if (projectile.CountsAsClass(DamageClass.Magic) || projectile.CountsAsClass(DamageClass.Throwing))
						{
                            modifiers.FinalDamage /= 2;
						}
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("LichHeadless").Type)
					{
						if (projectile.CountsAsClass(DamageClass.Magic) || projectile.CountsAsClass(DamageClass.Throwing))
						{
                            modifiers.FinalDamage /= 2;
						}
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("SlagFury").Type)
					{
						if (projectile.CountsAsClass(DamageClass.Melee) || projectile.CountsAsClass(DamageClass.Throwing))
						{
                            modifiers.FinalDamage /= 2;
						}
					}
					if (npc.type == ModLoader.GetMod("ThoriumMod").Find<ModNPC>("Aquaius").Type)
					{
						if (projectile.CountsAsClass(DamageClass.Magic))
						{
                            modifiers.FinalDamage /= 3;
						}
					}
				}
			}
		}

        /*public override bool CheckDead(NPC npc)
		{
			if (npc.type == mod.NPCType("DesertScourgeBody"))
			{
				Vector2 position = npc.position;
				Vector2 center = Main.player[npc.target].Center;
				float num2 = 1E+08f;
				Vector2 position2 = npc.position;
				for (int k = 0; k < 200; k++)
				{
					if (Main.npc[k].type == npc.type == mod.NPCType("DesertScourgeBody"))
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
				npc.NPCLoot();
				npc.position = position;
			}
		}*/

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.EyeofCthulhu)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new DemonCondition(), ModContent.ItemType<CounterScarf>()));
            }
            if (npc.type == ModContent.NPCType<DesertScourgeHead>())
            {
                npcLoot.Add(ItemDropRule.ByCondition(new DieCondition(), ModContent.ItemType<TorrentialTear>()));
            }
			if (npc.type == ModContent.NPCType<CalamitasRun3>())
            {
                npcLoot.Add(ItemDropRule.ByCondition(new EclipseCondition(), ItemID.BrokenHeroSword, 1));
            }
			if (npc.type == ModContent.NPCType<DevourerofGodsHead>())
            {
                npcLoot.Add(ItemDropRule.ByCondition(new CryCondition(), ModContent.ItemType<Murasama>(), 1));
            }
            if (npc.type == ModContent.NPCType<Yharon.Yharon>())
            {
                npcLoot.Add(ItemDropRule.ByCondition(new CryCondition(), ModContent.ItemType<DrewsWings>(), 1));
            }
            if (npc.type == NPCID.DarkCaster)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<AncientShiv>(), 20, 10));
            }
            if (npc.type == NPCID.BigMimicCorruption || npc.type == NPCID.BigMimicCrimson || npc.type == NPCID.BigMimicHallow || npc.type == NPCID.BigMimicJungle)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CelestialClaymore>(), 8, 5));
            }
            if (npc.type == NPCID.Clinger)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CursedDagger>(), 35, 25));
            }
            if (npc.type == NPCID.Shark)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<DepthBlade>(), 5, 3));
            }
            if (npc.type == NPCID.Vulture)
            {
                npcLoot.Add(new CommonDrop(ModContent.ItemType<DesertFeather>(), 1));
            }
            if (npc.type == NPCID.SkeletonSniper || npc.type == NPCID.TacticalSkeleton || npc.type == NPCID.SkeletonCommando || npc.type == NPCID.Paladin ||
               npc.type == NPCID.GiantCursedSkull || npc.type == NPCID.BoneLee || npc.type == NPCID.DiabolistWhite || npc.type == NPCID.DiabolistRed ||
               npc.type == NPCID.NecromancerArmored || npc.type == NPCID.Necromancer || npc.type == NPCID.RaggedCasterOpenCoat || npc.type == NPCID.RaggedCaster ||
               npc.type == NPCID.HellArmoredBonesSword || npc.type == NPCID.HellArmoredBonesMace || npc.type == NPCID.HellArmoredBonesSpikeShield ||
               npc.type == NPCID.HellArmoredBones || npc.type == NPCID.BlueArmoredBonesSword || npc.type == NPCID.BlueArmoredBonesNoPants ||
               npc.type == NPCID.BlueArmoredBonesMace || npc.type == NPCID.BlueArmoredBones || npc.type == NPCID.RustyArmoredBonesSwordNoArmor ||
               npc.type == NPCID.RustyArmoredBonesSword || npc.type == NPCID.RustyArmoredBonesFlail || npc.type == NPCID.RustyArmoredBonesAxe)
            {
                npcLoot.Add(new CommonDrop(ModContent.ItemType<Ectoblood>(), 2));
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
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<HolidayHalberd>(), 15, 10));
            }
            if (npc.type == NPCID.IchorSticker)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<IchorSpear>(), 35, 25));
            }
            if (npc.type == NPCID.Plantera)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<LivingShard>(), 1, 8, 10));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<LivingShard>(), 1, 6, 8));
            }
			if (npc.type == NPCID.Antlion || npc.type == NPCID.WalkingAntlion || npc.type == NPCID.FlyingAntlion)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MandibleBow>(), 35, 25));
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MandibleClaws>(), 35, 25));
            }
			if (npc.type == NPCID.NebulaBrain || npc.type == NPCID.NebulaSoldier || npc.type == NPCID.NebulaHeadcrab || npc.type == NPCID.NebulaBeast)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<MeldBlob>(), 4, 6, 7));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MeldBlob>(), 4, 3, 4));
            }
            if (npc.type == NPCID.CultistBoss)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<MeldBlob>(), 1, 20, 24));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MeldBlob>(), 1, 24, 28));
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(new CommonDrop(ModContent.ItemType<Meowthrower>(), 5));
            }
            if (npc.type == NPCID.MartianSaucerCore)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<NullificationRifle>(), 10, 7));
            }
            if (npc.type == NPCID.Demon)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BladecrestOathsword>(), 50, 40));
            }
            if (npc.type == NPCID.BoneSerpentHead)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<OldLordOathsword>(), 25, 20));
            }
            if (npc.type == NPCID.MoonLordCore)
            {
                npcLoot.Add(new CommonDrop(ModContent.ItemType<MLGRune2>(), 1));
            }
            if (npc.type == NPCID.Tim)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PlasmaRod>(), 4, 3));
            }
            if (npc.type == NPCID.GoblinSorcerer)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PlasmaRod>(), 40, 30));
            }
            if (npc.type == NPCID.PirateDeadeye)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ProporsePistol>(), 30, 20));
            }
            if (npc.type == NPCID.PirateCrossbower)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<RaidersGlory>(), 30, 25));
            }
            if (npc.type == NPCID.CultistBoss)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<StardustStaff>(), 5, 3));
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<TeardropCleaver>(), 3, 2));
            }
            if (npc.type == NPCID.GoblinSummoner)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<TheFirstShadowflame>(), 6, 4));
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(new CommonDrop(ModContent.ItemType<MLGRune>(), 1));
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<VictoryShard>(), 1, 3, 3));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<VictoryShard>(), 1, 2, 2));
            }
            if (npc.type == NPCID.SandElemental)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<WifeinaBottle>(), 8, 5));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<WifeinaBottlewithBoobs>(), 50));
            }
            if (npc.type == NPCID.Skeleton)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Waraxe>(), 90, 80));
            }
            if (npc.type == NPCID.GoblinWarrior)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Warblade>(), 70, 30));
            }
            if (npc.type == NPCID.MartianWalker)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Wingman>(), 20, 10));
            }
			if (npc.type == NPCID.GiantCursedSkull || npc.type == NPCID.NecromancerArmored || npc.type == NPCID.Necromancer)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<WrathoftheAncients>(), 10, 8));
            }
        }

        public override void OnKill(NPC npc)
		{
			if (npc.type == Mod.Find<ModNPC>("DesertScourgeHead").Type) //boss 1
			{
				if (!CalamityWorld.stopAerialite)
				{
					for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 15E-05); k++)
					{
						int i2 = WorldGen.genRand.Next(0, Main.maxTilesX);
						int j2 = WorldGen.genRand.Next((int)(Main.maxTilesY * .3f), (int)(Main.maxTilesY * .5f));
						WorldGen.OreRunner(i2, j2, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("AerialiteOre").Type);
					}
				}
				CalamityWorld.stopAerialite = true;
				CalamityWorld.downedDesertScourge = true;
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
				if (!CalamityWorld.stopPerennial)
				{
					for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 15E-05); k++)
					{
						int i2 = WorldGen.genRand.Next(0, Main.maxTilesX);
						int j2 = WorldGen.genRand.Next((int)(Main.maxTilesY * .4f), (int)(Main.maxTilesY * .8f));
						if (Main.tile[i2, j2].TileType == 0 || Main.tile[i2, j2].TileType == 1) 
						{
							WorldGen.OreRunner(i2, j2, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("PerennialOre").Type);
						}
					}
				}
				CalamityWorld.stopPerennial = true;
				CalamityWorld.downedSlimeGod = true;
			}
			if (npc.type == Mod.Find<ModNPC>("CryogenP6").Type) //boss 5
			{
				if (!CalamityWorld.stopCryonic)
				{
					for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 15E-05); k++)
					{
						int i2 = WorldGen.genRand.Next(0, Main.maxTilesX);
						int j2 = WorldGen.genRand.Next((int)(Main.maxTilesY * .35f), (int)(Main.maxTilesY * .55f));
						if (Main.tile[i2, j2].TileType == 147 || Main.tile[i2, j2].TileType == 161 || Main.tile[i2, j2].TileType == 163 || Main.tile[i2, j2].TileType == 164 || Main.tile[i2, j2].TileType == 200) 
						{
							WorldGen.OreRunner(i2, j2, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("CryonicOre").Type);
						}
					}
				}
				CalamityWorld.stopCryonic = true;
				CalamityWorld.downedCryogen = true;
			}
			if (npc.type == Mod.Find<ModNPC>("CalamitasRun3").Type) //boss 6
			{
				if (!CalamityWorld.stopChaotic)
			    {
					for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 15E-05); k++)
					{
						int i2 = WorldGen.genRand.Next(0, Main.maxTilesX);
						int j2 = WorldGen.genRand.Next(Main.maxTilesY - 200, Main.maxTilesY);
						WorldGen.OreRunner(i2, j2, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("ChaoticOre").Type);
					}
			    }
				CalamityWorld.stopChaotic = true;
				CalamityWorld.downedCalamitas = true;
			}
			if (npc.type == Mod.Find<ModNPC>("Siren").Type) //boss 7
			{
				CalamityWorld.downedLeviathan = true;
			}
			if (npc.type == Mod.Find<ModNPC>("PlaguebringerGoliath").Type) //boss 8
			{
				if (!CalamityWorld.stopUelibloom)
				{
					int x = Main.maxTilesX;
					int y = Main.maxTilesY;
					for (int k = 0; k < (int)((double)(x * y) * 15E-05); k++)
					{
						int i2 = WorldGen.genRand.Next(0, x);
						int j2 = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .65f));
						if (Main.tile[i2, j2].TileType == 59) 
						{
							WorldGen.OreRunner(i2, j2, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)Mod.Find<ModTile>("UelibloomOre").Type);
						}
					}
			   	}
				CalamityWorld.stopUelibloom = true;
				CalamityWorld.downedPlaguebringer = true;
			}
			if (npc.type == Mod.Find<ModNPC>("ProfanedGuardianBoss").Type) //boss 9
			{
				CalamityWorld.downedGuardians = true;
			}
			if (npc.type == Mod.Find<ModNPC>("Providence").Type) //boss 10
			{
				CalamityWorld.downedProvidence = true;
			}
			if (npc.type == Mod.Find<ModNPC>("StormWeaverHead").Type || npc.type == Mod.Find<ModNPC>("CeaselessVoid").Type || npc.type == Mod.Find<ModNPC>("CosmicWraith").Type) //boss 11
			{
				CalamityWorld.downedSentinel = true;
			}
			if (npc.type == Mod.Find<ModNPC>("DevourerofGodsHead").Type) //boss 12
			{
				CalamityWorld.downedDoG = true;
			}
			if (npc.type == Mod.Find<ModNPC>("Yharon").Type) //boss 13
			{
				CalamityWorld.downedYharon = true;
			}
			if (npc.type == Mod.Find<ModNPC>("SupremeCalamitas").Type) //boss 14
			{
				CalamityWorld.downedSCal = true;
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (bFlames)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("BrimstoneFlame").Type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					if (Main.rand.Next(4) == 0)
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
				Lighting.AddLight(npc.position, 0.05f, 0.01f, 0.01f);
			}
			if (pShred)
			{
				if (Main.rand.Next(3) < 2)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 5, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.1f;
					Main.dust[dust].velocity.Y += 0.25f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
			}
			if (hFlames)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, Mod.Find<ModDust>("HolyFlame").Type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					if (Main.rand.Next(4) == 0)
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
				Lighting.AddLight(npc.position, 0.25f, 0.25f, 0.1f);
			}
			if (pFlames)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 89, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.2f;
					Main.dust[dust].velocity.Y -= 0.15f;
					if (Main.rand.Next(4) == 0)
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
				Lighting.AddLight(npc.position, 0.07f, 0.15f, 0.01f);
			}
			if (gsInferno)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 173, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.2f;
					Main.dust[dust].velocity.Y -= 0.15f;
					if (Main.rand.Next(4) == 0)
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
				Lighting.AddLight(npc.position, 0.1f, 0f, 0.135f);
			}
			if (tSad || cDepth)
			{
				if (Main.rand.Next(6) < 3)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 33, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = false;
					Main.dust[dust].velocity *= 1.2f;
					Main.dust[dust].velocity.Y += 0.15f;
					if (Main.rand.Next(4) == 0)
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
			}
			if (gState)
			{
				drawColor = Color.Cyan;
			}
		}
	}
    public class OnionCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return CalamityGlobalNPC.superBossBuff;
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
    public class DemonCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return CalamityGlobalNPC.bossBuff;
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
    public class DieCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return CalamityGlobalNPC.superBossBuff || CalamityGlobalNPC.bossBuff;
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
    public class CryCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return CalamityGlobalNPC.superBossBuff && CalamityGlobalNPC.bossBuff;
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
            return null;
        }
    }
    public class HallowProvidence : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return Main.player[info.npc.target].ZoneHallow;
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
    public class HellProvidence : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return Main.player[info.npc.target].ZoneUnderworldHeight;
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
}