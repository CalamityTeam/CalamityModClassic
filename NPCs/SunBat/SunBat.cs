using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using CalamityModClassic1Point1.Items.DevourerMunsters;
using CalamityModClassic1Point1.Items.Weapons;
using Terraria.GameContent.ItemDropRules;
using CalamityModClassic1Point1.Items;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.SunBat
{
	public class SunBat : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Sun Bat");
			//Tooltip.SetDefault("Sun Bat");
			NPC.npcSlots = 0.5f;
			NPC.aiStyle = 14;
			NPC.damage = 56;
			NPC.width = 26; //324
			NPC.height = 20; //216
			NPC.defense = 20;
			NPC.lifeMax = 250;
			NPC.knockBackResist = 0.65f;
			AnimationType = 93;
			Main.npcFrameCount[NPC.type] = 4;
			NPC.value = Item.buyPrice(0, 0, 10, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath4;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
                new FlavorTextBestiaryInfoElement("A radiant bat. It is unknown why they are so bright.")

            });
        }
        public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.25f, 0.25f, 0f);
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int x = spawnInfo.SpawnTileX;
            int y = spawnInfo.SpawnTileY;
            int tile = (int)Main.tile[x, y].TileType;
            bool oUnderworld = (y <= (Main.maxTilesY * 0.8f));
            bool oRockLayer = (y >= (Main.maxTilesY * 0.4f));
            return oUnderworld && oRockLayer && Main.hardMode && Main.SceneMetrics.EvilTileCount < 80 && Main.SceneMetrics.SandTileCount < 80 && Main.SceneMetrics.JungleTileCount < 80 && Main.SceneMetrics.DungeonTileCount < 80 ? 0.06f : 0f;
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.OnFire, 300, true);
				target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 100, true);
			}
			else
			{
				target.AddBuff(BuffID.OnFire, 160, true);
				target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 50, true);
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 64, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 64, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<EssenceofCinder>(), 2));
        }
	}
}