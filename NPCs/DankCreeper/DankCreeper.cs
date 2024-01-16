using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.DankCreeper
{
	public class DankCreeper : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Dank Creeper");
			//Tooltip.SetDefault("Dank Creeper");
			NPC.npcSlots = 0.3f;
			NPC.damage = 35;
			NPC.width = 74; //324
			NPC.height = 74; //216
			NPC.scale = 0.8f;
			NPC.defense = 5;
			NPC.lifeMax = 100;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
            Main.npcFrameCount[NPC.type] = 1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0.3f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
                new FlavorTextBestiaryInfoElement("A spiky glob of corruption that leaves behind rain clouds when killed.")

            });
        }

        public override void AI()
		{
			NPC.TargetClosest(true);
			float num1372 = 11f;
			Vector2 vector167 = new Vector2(NPC.Center.X + (float)(NPC.direction * 20), NPC.Center.Y + 6f);
			float num1373 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector167.X;
			float num1374 = Main.player[NPC.target].Center.Y - vector167.Y;
			float num1375 = (float)Math.Sqrt((double)(num1373 * num1373 + num1374 * num1374));
			float num1376 = num1372 / num1375;
			num1373 *= num1376;
			num1374 *= num1376;
			NPC.ai[0] -= 1f;
			if (num1375 < 200f || NPC.ai[0] > 0f)
			{
				if (num1375 < 200f)
				{
					NPC.ai[0] = 20f;
				}
				if (NPC.velocity.X < 0f)
				{
					NPC.direction = -1;
				}
				else
				{
					NPC.direction = 1;
				}
				return;
			}
			NPC.velocity.X = (NPC.velocity.X * 50f + num1373) / 51f;
			NPC.velocity.Y = (NPC.velocity.Y * 50f + num1374) / 51f;
			if (num1375 < 350f)
			{
				NPC.velocity.X = (NPC.velocity.X * 10f + num1373) / 11f;
				NPC.velocity.Y = (NPC.velocity.Y * 10f + num1374) / 11f;
			}
			if (num1375 < 300f)
			{
				NPC.velocity.X = (NPC.velocity.X * 7f + num1373) / 8f;
				NPC.velocity.Y = (NPC.velocity.Y * 7f + num1374) / 8f;
			}
			return;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.55f * balance);
			NPC.damage = (int)(NPC.damage * 0.75f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 13, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 13, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void OnKill()
		{
			if (Main.expertMode)
			{
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ShadeNimbusHostile").Type, 14, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(Mod.Find<ModBuff>("BrainRot").Type, 60, true);
			}
			else
			{
				target.AddBuff(Mod.Find<ModBuff>("BrainRot").Type, 60, true);
			}
		}
	}
}