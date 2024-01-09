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

namespace CalamityModClassic1Point2.NPCs.NormalNPCs
{
	public class PhantomSpirit : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Phantom Spirit");
			Main.npcFrameCount[NPC.type] = 3;
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 110;
			NPC.width = 20; //324
			NPC.height = 20; //216
			NPC.defense = 80;
			NPC.lifeMax = 3000;
			NPC.knockBackResist = 0.1f;
			AnimationType = 288;
			AIType = -1;
			NPC.value = Item.buyPrice(0, 5, 0, 0);
			NPC.HitSound = SoundID.NPCHit36;
			NPC.DeathSound = SoundID.NPCDeath39;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
                new FlavorTextBestiaryInfoElement("Sometimes the Dungeon's spirits grow a bit too strong.")

            });
        }
        public override void AI()
		{
			NPC.TargetClosest(true);
			Vector2 vector102 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num818 = Main.player[NPC.target].Center.X - vector102.X;
			float num819 = Main.player[NPC.target].Center.Y - vector102.Y;
			float num820 = (float)Math.Sqrt((double)(num818 * num818 + num819 * num819));
			float num821 = 15f;
			num820 = num821 / num820;
			num818 *= num820;
			num819 *= num820;
			NPC.velocity.X = (NPC.velocity.X * 100f + num818) / 101f;
			NPC.velocity.Y = (NPC.velocity.Y * 100f + num819) / 101f;
			NPC.rotation = (float)Math.Atan2((double)num819, (double)num818) - 1.57f;
			int num822 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.RedTorch, 0f, 0f, 0, default(Color), 1f);
			Dust dust = Main.dust[num822];
			dust.velocity *= 0.1f;
			Main.dust[num822].scale = 1.3f;
			Main.dust[num822].noGravity = true;
			return;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.RedTorch, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int num288 = 0; num288 < 50; num288++)
				{
					int num289 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.RedTorch, NPC.velocity.X, NPC.velocity.Y, 0, default(Color), 1f);
					Dust dust = Main.dust[num289];
					dust.velocity *= 2f;
					Main.dust[num289].noGravity = true;
					Main.dust[num289].scale = 1.4f;
				}
			}
		}
		
		public override Color? GetAlpha(Color drawColor)
		{
			return new Color(200, 200, 200, 0);
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<Phantoplasm>(), 1, 1, 2));
        }
	}
}