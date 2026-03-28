using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.Yharon
{
	public class DetonatingFlare2 : ModNPC
	{
        float speed = 0f;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Detonating Flame");
			Main.npcFrameCount[NPC.type] = 5;
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
			NPC.damage = 220;
			NPC.width = 50; //324
			NPC.height = 50; //216
			NPC.defense = 150;
			NPC.lifeMax = 13000;
			NPC.knockBackResist = 0f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit52;
			NPC.DeathSound = SoundID.NPCDeath55;
			NPC.alpha = 255;
		}
		
		public override void AI()
		{
			bool revenge = CalamityWorldPreTrailer.revenge;
			NPC.alpha -= 3;
			NPC.TargetClosest(true);
			Vector2 vector98 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num790 = Main.player[NPC.target].Center.X - vector98.X;
			float num791 = Main.player[NPC.target].Center.Y - vector98.Y;
			float num792 = (float)Math.Sqrt((double)(num790 * num790 + num791 * num791));
            if (NPC.localAI[3] == 0f)
            {
                switch (Main.rand.Next(6))
                {
                    case 0: speed = 10f; break;
                    case 1: speed = 11.5f; break;
                    case 2: speed = 13f; break;
                    case 3: speed = 14.5f; break;
                    case 4: speed = 16f; break;
                    case 5: speed = 17.5f; break;
                }
                NPC.localAI[3] = 1f;
            }
			float num793 = speed + (revenge ? 1f : 0f);
			num792 = num793 / num792;
			num790 *= num792;
			num791 *= num792;
			NPC.velocity.X = (NPC.velocity.X * 100f + num790) / 101f;
			NPC.velocity.Y = (NPC.velocity.Y * 100f + num791) / 101f;
			NPC.rotation = (float)Math.Atan2((double)num791, (double)num790) - 1.57f;
			return;
		}
		
		public override Color? GetAlpha(Color drawColor)
		{
			return new Color(255, Main.DiscoG, 53, 0);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (CalamityWorldPreTrailer.revenge)
			{
				target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 300);
			}
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override bool PreKill()
		{
			return false;
		}
	}
}