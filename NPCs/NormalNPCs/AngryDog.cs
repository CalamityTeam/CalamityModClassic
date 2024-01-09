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
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.NormalNPCs
{
	public class AngryDog : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Angry Dog");
			Main.npcFrameCount[NPC.type] = 10;
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 26;
			NPC.damage = 25;
			NPC.width = 46; //324
			NPC.height = 30; //216
			NPC.defense = 6;
			NPC.lifeMax = 500;
			NPC.knockBackResist = 0.3f;
			AnimationType = 329;
			AIType = NPCID.Wolf;
			NPC.value = Item.buyPrice(0, 0, 8, 0);
			NPC.HitSound = SoundID.NPCHit6;
			NPC.DeathSound = SoundID.NPCDeath5;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                new FlavorTextBestiaryInfoElement("What feels like an illusion manifested from anxiety. These dogs are not happy to say the least.")

            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneSnow && 
            	!spawnInfo.Player.ZoneTowerStardust &&
            	!spawnInfo.Player.ZoneTowerSolar &&
            	!spawnInfo.Player.ZoneTowerVortex &&
            	!spawnInfo.Player.ZoneTowerNebula &&
            	!spawnInfo.PlayerInTown && !spawnInfo.Player.ZoneOldOneArmy && !Main.snowMoon && !Main.pumpkinMoon ? 0.01f : 0f;
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Bleeding, 300, true);
			}
			else
			{
				target.AddBuff(BuffID.Bleeding, 160, true);
			}
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
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ItemID.Leather, 2, 1, 2));
        }
	}
}