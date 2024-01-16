using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using CalamityModClassic1Point1.Items;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.EbonianBlightSlime
{
	public class EbonianBlightSlime : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Ebonian Blight Slime");
			//Tooltip.SetDefault("Ebonian Blight Slime");
			NPC.aiStyle = 1;
			NPC.damage = 65;
			NPC.width = 60; //324
			NPC.height = 42; //216
			NPC.defense = 12;
			NPC.lifeMax = 440;
			NPC.knockBackResist = 0.3f;
			AnimationType = 244;
			Main.npcFrameCount[NPC.type] = 4;
			NPC.value = Item.buyPrice(0, 0, 5, 0);
			NPC.alpha = 105;
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.buffImmune[24] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
                new FlavorTextBestiaryInfoElement("A purple slime that drops purple gel. What more could you ask for!")

            });
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 14, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 40; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 14, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return spawnInfo.Player.ZoneCorrupt && NPC.downedBoss3 && !Main.snowMoon && !Main.pumpkinMoon ? 0.1f : 0f; 
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.65f * balance);
			NPC.damage = (int)(NPC.damage * 0.65f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.ManaSickness, 100, true);
				target.AddBuff(BuffID.Weak, 260, true);
			}
			else
			{
				target.AddBuff(BuffID.ManaSickness, 80, true);
				target.AddBuff(BuffID.Weak, 200, true);
			}
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<EbonianGel>(), 1, 15, 16));
            npcLoot.Add(new CommonDrop(ItemID.Gel, 1, 10, 14));
        }
    }
}