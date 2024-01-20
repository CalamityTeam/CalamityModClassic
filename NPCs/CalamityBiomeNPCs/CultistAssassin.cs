using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.CalamityBiomeNPCs
{
	public class CultistAssassin : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cultist Assassin");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 3;
			NPC.damage = 100;
			NPC.width = 18; //324
			NPC.height = 40; //216
			NPC.defense = 25;
			NPC.lifeMax = 500;
			NPC.knockBackResist = 0.75f;
			AnimationType = 331;
			AIType = NPCID.ChaosElemental;
			NPC.value = Item.buyPrice(0, 2, 0, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath50;
			if (CalamityWorld1Point2.downedProvidence)
			{
				NPC.damage = 250;
				NPC.defense = 130;
				NPC.lifeMax = 5000;
				NPC.value = Item.buyPrice(0, 10, 0, 0);
            }
            SpawnModBiomes = new int[1] { ModContent.GetInstance<BiomeManagers.BrimstoneCragsBiome>().Type };
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("A cultist who is an assassin, or is it an assassin who assassinates cultists?")

            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
			return (spawnInfo.Player.GetModPlayer<CalamityPlayer1Point2>().ZoneCalamity || spawnInfo.Player.ZoneDungeon) && Main.hardMode ? 0.04f : 0f;
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.65f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorld1Point2.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 300, true);
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<EssenceofChaos>(), 2, 1, 2));
            npcLoot.Add(ItemDropRule.ByCondition(new ProvidenceDowned(), ModContent.ItemType<Bloodstone>(), 2));
        }
	}
}