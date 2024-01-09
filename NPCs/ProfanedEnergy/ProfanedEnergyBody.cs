using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.ModLoader.Utilities;
using CalamityModClassic1Point2.Items.PlaguebringerGoliath;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point2.Items.Providence;
using CalamityModClassic1Point2.Items.Weapons;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.ProfanedEnergy
{
	public class ProfanedEnergyBody : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Profaned Energy");
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 0;
			NPC.npcSlots = 3f;
			NPC.width = 72; //324
			NPC.height = 36; //216
			NPC.defense = 58;
			NPC.lifeMax = 28000;
			NPC.knockBackResist = 0f;
			AIType = -1;
			NPC.value = Item.buyPrice(0, 0, 10, 0);
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.HitSound = SoundID.NPCHit52;
			NPC.DeathSound = SoundID.NPCDeath55;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement("Nobody knows what the exact method of creation this entity was born from.")

            });
        }
        public override void AI()
		{
			CalamityGlobalNPC.energyFlame = NPC.whoAmI;
			if (Main.netMode != NetmodeID.MultiplayerClient) 
			{
				if (NPC.localAI[0] == 0f) 
				{
					NPC.localAI[0] = 1f;
					for (int num723 = 0; num723 < 2; num723++) 
					{
						NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("ProfanedEnergyLantern").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
					}
				}
			}
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || !NPC.downedMoonlord)
			{
				return 0f;
			}
			if (SpawnCondition.Underworld.Chance > 0f)
			{
				return SpawnCondition.Underworld.Chance / 5f;
			}
			return SpawnCondition.OverworldHallow.Chance / 5f;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.6f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CopperCoin, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CopperCoin, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<UnholyEssence>(), 1, 2, 4));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<EnergyStaff>(), 10));
        }
	}
}