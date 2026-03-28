using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.Crabulon
{
	public class CrabShroom : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crab Shroom");
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
	            Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 25;
			NPC.width = 14; //324
			NPC.height = 14; //216
			NPC.lifeMax = 25;
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = 90000;
            }
            AIType = -1;
			NPC.knockBackResist = 0.75f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
		}

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void AI()
		{
            Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0f, 0.2f, 0.4f);
            bool revenge = (CalamityWorldPreTrailer.revenge || CalamityWorldPreTrailer.bossRushActive);
			float speed = revenge ? 1.5f : 1f;
			Player player = Main.player[NPC.target];
			NPC.velocity.Y = NPC.velocity.Y + 0.02f;
			if (NPC.velocity.Y > speed) 
			{
				NPC.velocity.Y = speed;
			}
			NPC.TargetClosest(true);
			if (NPC.position.X + (float)NPC.width < player.position.X) 
			{
				if (NPC.velocity.X < 0f) 
				{
					NPC.velocity.X = NPC.velocity.X * 0.98f;
				}
				NPC.velocity.X = NPC.velocity.X + 0.1f;
			} 
			else if (NPC.position.X > player.position.X + (float)player.width) 
			{
				if (NPC.velocity.X > 0f) 
				{
					NPC.velocity.X = NPC.velocity.X * 0.98f;
				}
				NPC.velocity.X = NPC.velocity.X - 0.1f;
			}
			if (NPC.velocity.X > 5f || NPC.velocity.X < -5f) 
			{
				NPC.velocity.X = NPC.velocity.X * 0.97f;
			}
			NPC.rotation = NPC.velocity.X * 0.1f;
			return;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 56, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 56, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}