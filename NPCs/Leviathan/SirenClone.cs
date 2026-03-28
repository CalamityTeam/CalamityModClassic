using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.Leviathan
{
	public class SirenClone : ModNPC
	{
		public int timer = 0;
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Siren Clone");
			Main.npcFrameCount[NPC.type] = 6;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 0;
			NPC.width = 70; //324
			NPC.height = 120; //216
			NPC.defense = 0;
			NPC.lifeMax = 3000;
			NPC.knockBackResist = 0f;
			NPC.noGravity = true;
			NPC.chaseable = false;
			NPC.dontTakeDamage = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.alpha = 255;
		}
		
		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.1f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0f, 0.5f, 0.3f);
			if (NPC.alpha > 50)
			{
				NPC.alpha -= 5;
			}
			NPC.TargetClosest(true);
			Vector2 center = NPC.Center;
			int num14 = Math.Sign(Main.player[NPC.target].Center.X - center.X);
			if (num14 != 0)
			{
				NPC.direction = (NPC.spriteDirection = num14);
			}
			Vector2 direction = Main.player[NPC.target].Center - center;
			direction.Normalize();
			direction *= (CalamityWorldPreTrailer.death ? 15f : 11f); //9
			timer++;
			if (timer > (CalamityWorldPreTrailer.death ? 30 : 60))
			{
				if (Main.netMode != 1)
				{
					int type = Mod.Find<ModProjectile>("WaterSpear").Type;
                    switch (Main.rand.Next(6))
                    {
                        case 0: type = Mod.Find<ModProjectile>("FrostMist").Type; break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5: type = Mod.Find<ModProjectile>("WaterSpear").Type; break;
                    }
                    int damage = Main.expertMode ? 26 : 32;
					Projectile.NewProjectile(NPC.GetSource_FromThis(null), center.X, center.Y, direction.X, direction.Y, type, damage, 1f, NPC.target);
				}
				timer = 0;
			}
			if (NPC.CountNPCS(Mod.Find<ModNPC>("Siren").Type) < 1)
			{
				NPC.active = false;
                NPC.netUpdate = true;
				return;
			}
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = 3000;
			NPC.damage = 0;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 67, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}