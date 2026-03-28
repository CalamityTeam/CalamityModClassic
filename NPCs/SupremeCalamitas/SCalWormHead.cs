using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Generation;
using CalamityModClassicPreTrailer.Tiles;
using CalamityModClassicPreTrailer;
using Terraria.GameContent.Bestiary;
using Terraria.WorldBuilding;

namespace CalamityModClassicPreTrailer.NPCs.SupremeCalamitas
{
	public class SCalWormHead : ModNPC
	{
        private bool flies = true;
        private const int minLength = 51;
        private const int maxLength = 52;
        private float speed = 10f;
        private float turnSpeed = 0.15f;
        private float passedVar = 0f;
        private bool TailSpawned = false;
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sepulcher");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Scale = 0.8f,
                PortraitScale = new float?(0.8f),
                CustomTexturePath = "CalamityModClassicPreTrailer/NPCs/SupremeCalamitas/SCalWorm_Bestiary",
                PortraitPositionXOverride = new float?((float)40),
                PortraitPositionYOverride = new float?((float)40)
            };
            value.Position.X = value.Position.X + 50f;
            value.Position.Y = value.Position.Y + 35f;
            NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		}
        
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("A mechanical abomination summoned by the Brimstone Witch. It's almost invincible, lest for the brimstone hearts holding it in one piece.")
            });
        }
        
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            modifiers.SetMaxDamage(1);
        }
		
		public override void SetDefaults()
		{
			NPC.damage = 0; //150
			NPC.npcSlots = 5f;
			NPC.width = 56; //324
			NPC.height = 62; //216
			NPC.defense = 0;
            NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 1200000 : 1000000;
            if (CalamityWorldPreTrailer.death)
            {
                NPC.lifeMax = 2000000;
            }
            NPC.aiStyle = 6; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.scale = 1.2f;
			if (Main.expertMode)
			{
				NPC.scale = 1.35f;
			}
			NPC.alpha = 255;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
            NPC.chaseable = false;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.netAlways = true;
		}

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void AI()
		{
            if (NPC.CountNPCS(Mod.Find<ModNPC>("SCalWormHeart").Type) == 0)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.active = false;
            }
            if (NPC.ai[3] > 0f)
			{
				NPC.realLife = (int)NPC.ai[3];
			}
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			NPC.velocity.Length();
			if (Main.netMode != 1)
			{
				if (!TailSpawned && NPC.ai[0] == 0f)
	            {
	                int Previous = NPC.whoAmI;
	                for (int num36 = 0; num36 < maxLength; num36++)
	                {
	                    int lol = 0;
                        if (num36 >= 0 && num36 < minLength && num36 % 2 == 0)
                        {
                            lol = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("SCalWormBodyWeak").Type, NPC.whoAmI);
                            Main.npc[lol].localAI[0] += passedVar;
                            passedVar += 36f;
                        }
                        else if (num36 >= 0 && num36 < minLength)
	                    {
	                        lol = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("SCalWormBody").Type, NPC.whoAmI);
                            if (NPC.localAI[0] % 2 == 0)
                            {
                                Main.npc[lol].localAI[3] = 1f;
                                NPC.localAI[0] = 1f;
                            }
                            else
                            {
                                NPC.localAI[0] = 2f;
                            }
                        }
                        else
	                    {
	                        lol = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)NPC.position.X + (NPC.width / 2), (int)NPC.position.Y + (NPC.height / 2), Mod.Find<ModNPC>("SCalWormTail").Type, NPC.whoAmI);
	                    }
                        Main.npc[lol].realLife = NPC.whoAmI;
	                    Main.npc[lol].ai[2] = (float)NPC.whoAmI;
	                    Main.npc[lol].ai[1] = (float)Previous;
	                    Main.npc[Previous].ai[0] = (float)lol;
	                    Previous = lol;
	                }
	                TailSpawned = true;
	            }
				if (!NPC.active && Main.netMode == 2)
				{
					NetMessage.SendData(28, -1, -1, null, NPC.whoAmI, -1f, 0f, 0f, 0, 0, 0);
				}
			}
            int num180 = (int)(NPC.position.X / 16f) - 1;
            int num181 = (int)((NPC.position.X + (float)NPC.width) / 16f) + 2;
            int num182 = (int)(NPC.position.Y / 16f) - 1;
            int num183 = (int)((NPC.position.Y + (float)NPC.height) / 16f) + 2;
            if (num180 < 0)
            {
                num180 = 0;
            }
            if (num181 > Main.maxTilesX)
            {
                num181 = Main.maxTilesX;
            }
            if (num182 < 0)
            {
                num182 = 0;
            }
            if (num183 > Main.maxTilesY)
            {
                num183 = Main.maxTilesY;
            }
            NPC.localAI[1] = 0f;
            bool canFly = flies;
            if (Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(false);
                canFly = false;
                NPC.alpha += 5;
                if (NPC.alpha >= 255)
                {
                    NPC.alpha = 255;
                    for (int num957 = 0; num957 < 200; num957++)
                    {
                        if (Main.npc[num957].aiStyle == NPC.aiStyle)
                        {
                            Main.npc[num957].active = false;
                        }
                    }
                }
            }
            else
            {
                NPC.alpha -= 42;
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
            }
            if (Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 10000f)
            {
                for (int num957 = 0; num957 < 200; num957++)
                {
                    if (Main.npc[num957].aiStyle == NPC.aiStyle)
                    {
                        Main.npc[num957].active = false;
                    }
                }
            }
            float num188 = speed;
            float num189 = turnSpeed;
            Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float num191 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
            float num192 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
            int num42 = -1;
            int num43 = (int)(Main.player[NPC.target].Center.X / 16f);
            int num44 = (int)(Main.player[NPC.target].Center.Y / 16f);
            for (int num45 = num43 - 2; num45 <= num43 + 2; num45++)
            {
                for (int num46 = num44; num46 <= num44 + 15; num46++)
                {
                    if (WorldGen.SolidTile2(num45, num46))
                    {
                        num42 = num46;
                        break;
                    }
                }
                if (num42 > 0)
                {
                    break;
                }
            }
            if (num42 > 0)
            {
                num42 *= 16;
                float num47 = (float)(num42 - 400); //800
                if (Main.player[NPC.target].position.Y > num47)
                {
                    num192 = num47;
                    if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < 500f)
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            num191 = Main.player[NPC.target].Center.X + 600f;
                        }
                        else
                        {
                            num191 = Main.player[NPC.target].Center.X - 600f;
                        }
                    }
                }
            }
            float num48 = num188 * 1.3f;
            float num49 = num188 * 0.7f;
            float num50 = NPC.velocity.Length();
            if (num50 > 0f)
            {
                if (num50 > num48)
                {
                    NPC.velocity.Normalize();
                    NPC.velocity *= num48;
                }
                else if (num50 < num49)
                {
                    NPC.velocity.Normalize();
                    NPC.velocity *= num49;
                }
            }
            if (num42 > 0)
            {
                for (int num51 = 0; num51 < 200; num51++)
                {
                    if (Main.npc[num51].active && Main.npc[num51].type == NPC.type && num51 != NPC.whoAmI)
                    {
                        Vector2 vector3 = Main.npc[num51].Center - NPC.Center;
                        if (vector3.Length() < 400f)
                        {
                            vector3.Normalize();
                            vector3 *= 1000f;
                            num191 -= vector3.X;
                            num192 -= vector3.Y;
                        }
                    }
                }
            }
            else
            {
                for (int num52 = 0; num52 < 200; num52++)
                {
                    if (Main.npc[num52].active && Main.npc[num52].type == NPC.type && num52 != NPC.whoAmI)
                    {
                        Vector2 vector4 = Main.npc[num52].Center - NPC.Center;
                        if (vector4.Length() < 60f)
                        {
                            vector4.Normalize();
                            vector4 *= 200f;
                            num191 -= vector4.X;
                            num192 -= vector4.Y;
                        }
                    }
                }
            }
            num191 = (float)((int)(num191 / 16f) * 16);
            num192 = (float)((int)(num192 / 16f) * 16);
            vector18.X = (float)((int)(vector18.X / 16f) * 16);
            vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
            num191 -= vector18.X;
            num192 -= vector18.Y;
            float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
            if (NPC.ai[1] > 0f && NPC.ai[1] < (float)Main.npc.Length)
            {
                try
                {
                    vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    num191 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - vector18.X;
                    num192 = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - vector18.Y;
                }
                catch
                {
                }
                NPC.rotation = (float)System.Math.Atan2((double)num192, (double)num191) + 1.57f;
                num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                int num194 = NPC.width;
                num193 = (num193 - (float)num194) / num193;
                num191 *= num193;
                num192 *= num193;
                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + num191;
                NPC.position.Y = NPC.position.Y + num192;
            }
            else
            {
                num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                float num196 = System.Math.Abs(num191);
                float num197 = System.Math.Abs(num192);
                float num198 = num188 / num193;
                num191 *= num198;
                num192 *= num198;
                if ((NPC.velocity.X > 0f && num191 > 0f) || (NPC.velocity.X < 0f && num191 < 0f) || (NPC.velocity.Y > 0f && num192 > 0f) || (NPC.velocity.Y < 0f && num192 < 0f))
                {
                    if (NPC.velocity.X < num191)
                    {
                        NPC.velocity.X = NPC.velocity.X + num189;
                    }
                    else
                    {
                        if (NPC.velocity.X > num191)
                        {
                            NPC.velocity.X = NPC.velocity.X - num189;
                        }
                    }
                    if (NPC.velocity.Y < num192)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num189;
                    }
                    else
                    {
                        if (NPC.velocity.Y > num192)
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num189;
                        }
                    }
                    if ((double)System.Math.Abs(num192) < (double)num188 * 0.2 && ((NPC.velocity.X > 0f && num191 < 0f) || (NPC.velocity.X < 0f && num191 > 0f)))
                    {
                        if (NPC.velocity.Y > 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num189 * 2f;
                        }
                        else
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num189 * 2f;
                        }
                    }
                    if ((double)System.Math.Abs(num191) < (double)num188 * 0.2 && ((NPC.velocity.Y > 0f && num192 < 0f) || (NPC.velocity.Y < 0f && num192 > 0f)))
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num189 * 2f; //changed from 2
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X - num189 * 2f; //changed from 2
                        }
                    }
                }
                else
                {
                    if (num196 > num197)
                    {
                        if (NPC.velocity.X < num191)
                        {
                            NPC.velocity.X = NPC.velocity.X + num189 * 1.1f; //changed from 1.1
                        }
                        else if (NPC.velocity.X > num191)
                        {
                            NPC.velocity.X = NPC.velocity.X - num189 * 1.1f; //changed from 1.1
                        }
                        if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
                        {
                            if (NPC.velocity.Y > 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + num189;
                            }
                            else
                            {
                                NPC.velocity.Y = NPC.velocity.Y - num189;
                            }
                        }
                    }
                    else
                    {
                        if (NPC.velocity.Y < num192)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num189 * 1.1f;
                        }
                        else if (NPC.velocity.Y > num192)
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num189 * 1.1f;
                        }
                        if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
                        {
                            if (NPC.velocity.X > 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X + num189;
                            }
                            else
                            {
                                NPC.velocity.X = NPC.velocity.X - num189;
                            }
                        }
                    }
                }
            }
            NPC.rotation = (float)System.Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
        }

		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if ((projectile.penetrate == -1 || projectile.penetrate > 1) && !projectile.minion)
			{
				projectile.penetrate = 1;
			}
		}

		public override bool CheckActive()
		{
			return false;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 50;
				NPC.height = 50;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 5; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 10; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 235, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
	}
}