using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;

namespace CalamityModClassic1Point2.NPCs.Cryogen
{
	public class CryogenIce : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cryogen's Shield");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.canGhostHeal = false;
			NPC.noTileCollide = true;
			NPC.damage = 30;
			NPC.width = 200; //324
			NPC.height = 200; //216
			NPC.defense = 0;
			NPC.lifeMax = 500;
			NPC.alpha = 255;
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath7;
		}
		
		public override void AI()
		{
			NPC.alpha -= 3;
			if (NPC.alpha <= 0)
			{
				NPC.alpha = 0;
			}
			NPC.rotation += 0.15f;
			if (NPC.type == Mod.Find<ModNPC>("CryogenIce").Type)
			{
				int num989 = (int)NPC.ai[0];
				if (Main.npc[num989].active && Main.npc[num989].type == Mod.Find<ModNPC>("Cryogen").Type)
				{
					NPC.velocity = Vector2.Zero;
					NPC.position = Main.npc[num989].Center;
					NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
					NPC.gfxOffY = Main.npc[num989].gfxOffY;
					return;
				}
				NPC.life = 0;
				NPC.HitEffect(0, 10.0);
				NPC.active = false;
				return;
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage *= 2;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.IceRod, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 200;
				NPC.height = 200;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 25; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.IceRod, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 50; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.IceRod, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.IceRod, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
	}
}