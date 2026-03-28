using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.Bumblefuck
{
	public class Bumblefuck2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bumblebirb");
			Main.npcFrameCount[NPC.type] = 10;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 1f;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 110;
			NPC.width = 80; //324
			NPC.height = 80; //216
			NPC.scale = 0.66f;
			NPC.defense = 25;
			NPC.lifeMax = 30000;
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = 60000;
            }
            NPC.knockBackResist = 0f;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
            }
			NPC.buffImmune[BuffID.Ichor] = false;
			NPC.buffImmune[BuffID.CursedInferno] = false;
			NPC.buffImmune[Mod.Find<ModBuff>("ExoFreeze").Type] = false;
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit51;
			NPC.DeathSound = SoundID.NPCDeath46;
		}

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            bool revenge = CalamityWorldPreTrailer.revenge;
            Vector2 vector = NPC.Center;
            NPC.damage = NPC.defDamage;
            if (Vector2.Distance(player.Center, vector) > 5600f)
            {
                if (NPC.timeLeft > 5)
                {
                    NPC.timeLeft = 5;
                }
            }
            NPC.noTileCollide = false;
            NPC.noGravity = true;
            NPC.rotation = (NPC.rotation * 9f + NPC.velocity.X * 0.05f) / 10f;
            if (NPC.ai[0] == 0f || NPC.ai[0] == 1f)
            {
                int num;
                for (int num1376 = 0; num1376 < 200; num1376 = num + 1)
                {
                    if (num1376 != NPC.whoAmI && Main.npc[num1376].active && Main.npc[num1376].type == NPC.type)
                    {
                        Vector2 value42 = Main.npc[num1376].Center - NPC.Center;
                        if (value42.Length() < (float)(NPC.width + NPC.height))
                        {
                            value42.Normalize();
                            value42 *= -0.1f;
                            NPC.velocity += value42;
                            NPC nPC6 = Main.npc[num1376];
                            nPC6.velocity -= value42;
                        }
                    }
                    num = num1376;
                }
            }
            if (NPC.target < 0 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
                Vector2 vector240 = Main.player[NPC.target].Center - NPC.Center;
                if (Main.player[NPC.target].dead || vector240.Length() > 5600f)
                {
                    NPC.ai[0] = -1f;
                }
            }
            else
            {
                Vector2 vector241 = Main.player[NPC.target].Center - NPC.Center;
                if (NPC.ai[0] > 1f && vector241.Length() > 1000f)
                {
                    NPC.ai[0] = 1f;
                }
            }
            if (NPC.ai[0] == -1f)
            {
                Vector2 value43 = new Vector2(0f, -8f);
                NPC.velocity = (NPC.velocity * 21f + value43) / 10f;
                NPC.noTileCollide = true;
                NPC.dontTakeDamage = true;
                return;
            }
            if (NPC.ai[0] == 0f)
            {
                NPC.TargetClosest(true);
                NPC.spriteDirection = NPC.direction;
                if (NPC.collideX)
                {
                    NPC.velocity.X = NPC.velocity.X * (-NPC.oldVelocity.X * 0.5f);
                    if (NPC.velocity.X > 21f)
                    {
                        NPC.velocity.X = 21f;
                    }
                    if (NPC.velocity.X < -21f)
                    {
                        NPC.velocity.X = -21f;
                    }
                }
                if (NPC.collideY)
                {
                    NPC.velocity.Y = NPC.velocity.Y * (-NPC.oldVelocity.Y * 0.5f);
                    if (NPC.velocity.Y > 21f)
                    {
                        NPC.velocity.Y = 21f;
                    }
                    if (NPC.velocity.Y < -21f)
                    {
                        NPC.velocity.Y = -21f;
                    }
                }
                Vector2 value44 = Main.player[NPC.target].Center - NPC.Center;
                if (value44.Length() > 1600f)
                {
                    NPC.ai[0] = 1f;
                    NPC.ai[1] = 0f;
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                }
                else if (value44.Length() > 400f)
                {
                    float scaleFactor20 = 5.5f + value44.Length() / 100f + NPC.ai[1] / 15f;
                    float num1377 = 30f;
                    value44.Normalize();
                    value44 *= scaleFactor20;
                    NPC.velocity = (NPC.velocity * (num1377 - 1f) + value44) / 29.9f; //39
                }
                else if (NPC.velocity.Length() > 20f)
                {
                    NPC.velocity *= 0.95f;
                }
                else if (NPC.velocity.Length() < 10f)
                {
                    NPC.velocity *= 1.05f;
                }
                NPC.ai[1] += 1f;
                if (NPC.ai[1] >= 90f)
                {
                    NPC.ai[1] = 0f;
                    NPC.ai[0] = 2f;
                    return;
                }
            }
            else
            {
                if (NPC.ai[0] == 1f)
                {
                    NPC.collideX = false;
                    NPC.collideY = false;
                    NPC.noTileCollide = true;
                    NPC.knockBackResist = 0f;
                    if (NPC.target < 0 || !Main.player[NPC.target].active || Main.player[NPC.target].dead)
                    {
                        NPC.TargetClosest(true);
                    }
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.direction = -1;
                    }
                    else if (NPC.velocity.X > 0f)
                    {
                        NPC.direction = 1;
                    }
                    NPC.spriteDirection = NPC.direction;
                    NPC.rotation = (NPC.rotation * 9f + NPC.velocity.X * 0.04f) / 10f;
                    Vector2 value45 = Main.player[NPC.target].Center - NPC.Center;
                    if (value45.Length() < 1200f && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                    {
                        NPC.ai[0] = 0f;
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                        NPC.ai[3] = 0f;
                    }
                    NPC.ai[2] += 0.0166666675f;
                    float scaleFactor21 = 5.5f + NPC.ai[2] + value45.Length() / 150f;
                    float num1378 = 26f;
                    value45.Normalize();
                    value45 *= scaleFactor21;
                    NPC.velocity = (NPC.velocity * (num1378 - 1f) + value45) / 25.9f; //34.5
                    return;
                }
                if (NPC.ai[0] == 2f)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.direction = -1;
                    }
                    else if (NPC.velocity.X > 0f)
                    {
                        NPC.direction = 1;
                    }
                    NPC.spriteDirection = NPC.direction;
                    NPC.rotation = (NPC.rotation * 7f + NPC.velocity.X * 0.05f) / 8f;
                    NPC.knockBackResist = 0f;
                    NPC.noTileCollide = true;
                    Vector2 vector242 = Main.player[NPC.target].Center - NPC.Center;
                    vector242.Y -= 8f;
                    float scaleFactor22 = 21f; //9
                    float num1379 = 8f;
                    vector242.Normalize();
                    vector242 *= scaleFactor22;
                    NPC.velocity = (NPC.velocity * (num1379 - 1f) + vector242) / 8f; //7.9
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.direction = -1;
                    }
                    else
                    {
                        NPC.direction = 1;
                    }
                    NPC.spriteDirection = NPC.direction;
                    NPC.ai[1] += 1f;
                    if (NPC.ai[1] > 10f)
                    {
                        NPC.velocity = vector242;
                        if (NPC.velocity.X < 0f)
                        {
                            NPC.direction = -1;
                        }
                        else
                        {
                            NPC.direction = 1;
                        }
                        NPC.ai[0] = 2.1f;
                        NPC.ai[1] = 0f;
                        return;
                    }
                }
                else if (NPC.ai[0] == 2.1f)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.direction = -1;
                    }
                    else if (NPC.velocity.X > 0f)
                    {
                        NPC.direction = 1;
                    }
                    NPC.spriteDirection = NPC.direction;
                    NPC.velocity *= 1.01f;
                    NPC.knockBackResist = 0f;
                    NPC.noTileCollide = true;
                    NPC.ai[1] += 1f;
                    int num1380 = 30; //45
                    if (NPC.ai[1] > (float)num1380)
                    {
                        if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                        {
                            NPC.ai[0] = 0f;
                            NPC.ai[1] = 0f;
                            NPC.ai[2] = 0f;
                            return;
                        }
                        if (NPC.ai[1] > (float)(num1380 * 2))
                        {
                            NPC.ai[0] = 1f;
                            NPC.ai[1] = 0f;
                            NPC.ai[2] = 0f;
                            return;
                        }
                    }
                }
            }
        }

        public override bool PreKill()
        {
            return false;
        }
		
		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += (double)(NPC.velocity.Length() / 4f);
			NPC.frameCounter += 1.0;
			if (NPC.ai[0] < 4f)
			{
				if (NPC.frameCounter >= 6.0)
				{
					NPC.frameCounter = 0.0;
					NPC.frame.Y = NPC.frame.Y + frameHeight;
				}
				if (NPC.frame.Y / frameHeight > 4)
				{
					NPC.frame.Y = 0;
				}
			}
			else
			{
				if (NPC.frameCounter >= 6.0)
				{
					NPC.frameCounter = 0.0;
					NPC.frame.Y = NPC.frame.Y + frameHeight;
				}
				if (NPC.frame.Y / frameHeight > 9)
				{
					NPC.frame.Y = frameHeight * 5;
				}
			}
        }
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 244, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 50; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 244, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}