using System;
using System.Collections.Generic;
using System.IO;
using CalamityModClassic1Point1.Items;
using CalamityModClassic1Point1.Items.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.NPCs.Horse
{
	public class Horse : ModNPC
	{
        int chargetimer = 0;
        int basespeed = 5; //customize
		public override void SetDefaults()
		{
			//NPC.name = "Horse");
			//Tooltip.SetDefault("Earth Elemental");
			NPC.damage = 50;
			NPC.width = 200; 
			NPC.height = 200; 
			NPC.defense = 20;
            NPC.lifeMax = 4000;
			NPC.aiStyle = -1;
			AIType = -1;
            NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 2, 0, 0);
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
            Main.npcFrameCount[NPC.type] = 6;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
                new FlavorTextBestiaryInfoElement("Knights are an underrated chess piece.")

            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int x = spawnInfo.SpawnTileX;
            int y = spawnInfo.SpawnTileY;
            int tile = (int)Main.tile[x, y].TileType;
            bool oUnderworld = (y <= (Main.maxTilesY * 0.75f));
            bool oRockLayer = (y >= (Main.maxTilesY * 0.4f));
            return oUnderworld && oRockLayer && Main.hardMode && !NPC.AnyNPCs(Mod.Find<ModNPC>("Horse").Type) && Main.SceneMetrics.EvilTileCount < 80 && Main.SceneMetrics.SandTileCount < 80 && Main.SceneMetrics.JungleTileCount < 80 && Main.SceneMetrics.DungeonTileCount < 80 ? 0.002f : 0f;
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.65f);
		}
		
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 8)
            {
                NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
                NPC.frameCounter = 0;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			npcLoot.Add(new CommonDrop(ModContent.ItemType<SlagMagnum>(), 4));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<Aftershock>(), 4));
            npcLoot.Add(new CommonDrop(ModContent.ItemType<EarthenPike>(), 4));
        }
        
        public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 15; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 31, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				SoundEngine.PlaySound(SoundID.Item14, NPC.position);
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 160;
				NPC.height = 160;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 40; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 31, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 70; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 6, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 6, 0f, 0f, 100, default(Color), 2f);
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
					int num626 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num626].velocity *= scaleFactor10;
					Gore expr_13AB6_cp_0 = Main.gore[num626];
					expr_13AB6_cp_0.velocity.X = expr_13AB6_cp_0.velocity.X + 1f;
					Gore expr_13AD6_cp_0 = Main.gore[num626];
					expr_13AD6_cp_0.velocity.Y = expr_13AD6_cp_0.velocity.Y + 1f;
					num626 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num626].velocity *= scaleFactor10;
					Gore expr_13B79_cp_0 = Main.gore[num626];
					expr_13B79_cp_0.velocity.X = expr_13B79_cp_0.velocity.X - 1f;
					Gore expr_13B99_cp_0 = Main.gore[num626];
					expr_13B99_cp_0.velocity.Y = expr_13B99_cp_0.velocity.Y + 1f;
					num626 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num626].velocity *= scaleFactor10;
					Gore expr_13C3C_cp_0 = Main.gore[num626];
					expr_13C3C_cp_0.velocity.X = expr_13C3C_cp_0.velocity.X + 1f;
					Gore expr_13C5C_cp_0 = Main.gore[num626];
					expr_13C5C_cp_0.velocity.Y = expr_13C5C_cp_0.velocity.Y - 1f;
					num626 = Gore.NewGore(NPC.GetSource_FromThis(), new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num626].velocity *= scaleFactor10;
					Gore expr_13CFF_cp_0 = Main.gore[num626];
					expr_13CFF_cp_0.velocity.X = expr_13CFF_cp_0.velocity.X - 1f;
					Gore expr_13D1F_cp_0 = Main.gore[num626];
					expr_13D1F_cp_0.velocity.Y = expr_13D1F_cp_0.velocity.Y - 1f;
				}
			}
		}
        
        public override bool PreAI()
        {
        	if ((double)Math.Abs(NPC.velocity.X) > 0.2)
			{
				NPC.spriteDirection = NPC.direction;
        	}
        	bool expertMode = Main.expertMode;
            NPC.TargetClosest(true);
            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize(); // direction is something of a placeholder for velocity
            #region charging
            chargetimer += expertMode ? 2 : 1; // increases timer
            if (chargetimer >= Math.Sqrt(NPC.life) * 14) //sqrt is to indirect scaling (IE not DIRECTLY proportional to life) 7 is arbituary
            {
                if (Main.rand.Next(25) == 1)
                {
                    direction.X = direction.X * Main.rand.Next(20, 24); //charge goes into action, but only for direction
                    direction.Y = direction.Y * Main.rand.Next(20, 24);
                    NPC.velocity = direction; //direction is assigned to npc.velocity. This is to prevent unpredictable AI spiral
                    //npc.rotation = direction.ToRotation() + 3.14f;
                    chargetimer = 0; //of course the timer has to be reset :P
                }
            }
            if (Math.Sqrt((NPC.velocity.X * NPC.velocity.X) + (NPC.velocity.Y * NPC.velocity.Y)) > basespeed) 
            {
                NPC.velocity *= 0.985f; //slowing down charges
            }
            #endregion
            #region normal movement
            if (Math.Sqrt((NPC.velocity.X * NPC.velocity.X) + (NPC.velocity.Y * NPC.velocity.Y)) <= basespeed * 1.15) //1.15 is leeway for small programming errors
            {
                NPC.velocity = direction * basespeed; //adjusting speed, nothing big
                //npc.rotation = direction.ToRotation() + 3.14f;
            }
            #endregion
           
            return false;

        }
    }
}