using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.Polterghast
{
	public class PolterghastHook : ModNPC
	{
        public int despawnTimer = 300;
        public bool phase2 = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Polterghast Hook");
			Main.npcFrameCount[NPC.type] = 2;
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
			NPC.damage = 120;
			NPC.width = 40; //324
			NPC.height = 40; //216
			NPC.defense = 50;
            NPC.lifeMax = CalamityWorldPreTrailer.revenge ? 60000 : 50000;
            if (CalamityWorldPreTrailer.death)
            {
                NPC.lifeMax = 100000;
            }
            NPC.dontTakeDamage = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.HitSound = SoundID.NPCHit34;
			NPC.DeathSound = SoundID.NPCDeath39;
		}

        public override void AI()
        {
            Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.3f, 1f, 1f);
            bool expertMode = Main.expertMode;
            bool revenge = CalamityWorldPreTrailer.revenge;
            bool speedBoost1 = false;
            bool despawnBoost = false;
            if (!Main.npc[CalamityGlobalNPC.ghostBoss].active)
            {
                NPC.active = false;
                NPC.netUpdate = true;
                return;
            }
            if (CalamityGlobalNPC.ghostBoss != -1 && !Main.player[Main.npc[CalamityGlobalNPC.ghostBoss].target].ZoneDungeon && !CalamityWorldPreTrailer.bossRushActive)
            {
                despawnTimer--;
                if (despawnTimer <= 0) { despawnBoost = true; }
                NPC.localAI[0] -= 6f;
                speedBoost1 = true;
            }
            else { despawnTimer = 300; }
            if ((double)Main.npc[CalamityGlobalNPC.ghostBoss].life <= (double)Main.npc[CalamityGlobalNPC.ghostBoss].lifeMax * 0.75)
            {
                if (!phase2)
                {
                    NPC.damage = 0;
                    phase2 = true;
                }
                NPC.TargetClosest(true);
                if (Main.netMode == 1)
                {
                    if (NPC.ai[0] == 0f)
                    {
                        NPC.ai[0] = (float)((int)(NPC.Center.X / 16f));
                    }
                    if (NPC.ai[1] == 0f)
                    {
                        NPC.ai[1] = (float)((int)(NPC.Center.X / 16f));
                    }
                }
                if (Main.netMode != 1)
                {
                    if (NPC.ai[0] == 0f || NPC.ai[1] == 0f)
                    {
                        NPC.localAI[0] = 0f;
                    }
                    NPC.localAI[0] -= 5f;
                    if (speedBoost1)
                    {
                        NPC.localAI[0] -= 10f;
                    }
                    if (!despawnBoost && NPC.localAI[0] <= 0f && NPC.ai[0] != 0f)
                    {
                        int num;
                        for (int num763 = 0; num763 < 200; num763 = num + 1)
                        {
                            if (num763 != NPC.whoAmI && Main.npc[num763].active && Main.npc[num763].type == NPC.type && (Main.npc[num763].velocity.X != 0f || Main.npc[num763].velocity.Y != 0f))
                            {
                                NPC.localAI[0] = (float)Main.rand.Next(60, 300);
                            }
                            num = num763;
                        }
                    }
                    if (NPC.localAI[0] <= 0f)
                    {
                        NPC.localAI[0] = (float)Main.rand.Next(300, 600);
                        bool flag50 = false;
                        int num764 = 0;
                        while (!flag50 && num764 <= 1000)
                        {
                            num764++;
                            int num765 = (int)(Main.player[Main.npc[CalamityGlobalNPC.ghostBoss].target].Center.X / 16f);
                            int num766 = (int)(Main.player[Main.npc[CalamityGlobalNPC.ghostBoss].target].Center.Y / 16f);
                            if (NPC.ai[0] == 0f)
                            {
                                num765 = (int)((Main.player[Main.npc[CalamityGlobalNPC.ghostBoss].target].Center.X + Main.npc[CalamityGlobalNPC.ghostBoss].Center.X) / 32f);
                                num766 = (int)((Main.player[Main.npc[CalamityGlobalNPC.ghostBoss].target].Center.Y + Main.npc[CalamityGlobalNPC.ghostBoss].Center.Y) / 32f);
                            }
                            if (despawnBoost)
                            {
                                num765 = (int)Main.npc[CalamityGlobalNPC.ghostBoss].position.X / 16;
                                num766 = (int)(Main.npc[CalamityGlobalNPC.ghostBoss].position.Y + 400f) / 16;
                            }
                            int num767 = 20;
                            num767 += (int)(100f * ((float)num764 / 1000f));
                            int num768 = num765 + Main.rand.Next(-num767, num767 + 1);
                            int num769 = num766 + Main.rand.Next(-num767, num767 + 1);
                            try
                            {
                                if (WorldGen.SolidTile(num768, num769) || (Main.tile[num768, num769].WallType > 0 && (num764 > 500 || Main.npc[CalamityGlobalNPC.ghostBoss].life < Main.npc[CalamityGlobalNPC.ghostBoss].lifeMax / 2)))
                                {
                                    flag50 = true;
                                    NPC.ai[0] = (float)num768;
                                    NPC.ai[1] = (float)num769;
                                    NPC.netUpdate = true;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                if (NPC.ai[0] > 0f && NPC.ai[1] > 0f)
                {
                    float num772 = 8f;
                    if (expertMode)
                    {
                        num772 += 1f;
                    }
                    if (speedBoost1)
                    {
                        num772 *= 2f;
                    }
                    if (despawnBoost)
                    {
                        num772 *= 2f;
                    }
                    Vector2 vector95 = new Vector2(NPC.Center.X, NPC.Center.Y);
                    float num773 = NPC.ai[0] * 16f - 8f - vector95.X;
                    float num774 = NPC.ai[1] * 16f - 8f - vector95.Y;
                    float num775 = (float)Math.Sqrt((double)(num773 * num773 + num774 * num774));
                    if (num775 < 12f + num772)
                    {
                        NPC.velocity.X = num773;
                        NPC.velocity.Y = num774;
                    }
                    else
                    {
                        num775 = num772 / num775;
                        NPC.velocity.X = num773 * num775;
                        NPC.velocity.Y = num774 * num775;
                    }
                }
                Vector2 vector102 = new Vector2(NPC.Center.X, NPC.Center.Y);
                float num818 = Main.player[NPC.target].Center.X - vector102.X;
                float num819 = Main.player[NPC.target].Center.Y - vector102.Y;
                float num820 = (float)Math.Sqrt((double)(num818 * num818 + num819 * num819));
                float num821 = 10f;
                num820 = num821 / num820;
                num818 *= num820;
                num819 *= num820;
                NPC.rotation = (float)Math.Atan2((double)num819, (double)num818) + 1.57f;
                Vector2 vector17 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float num147 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector17.X;
                float num148 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector17.Y;
                float num149 = (float)Math.Sqrt((double)(num147 * num147 + num148 * num148));
                num149 = 4f / num149;
                num147 *= num149;
                num148 *= num149;
                vector17 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                num147 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector17.X;
                num148 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector17.Y;
                num149 = (float)Math.Sqrt((double)(num147 * num147 + num148 * num148));
                if (num149 > 1200f)
                {
                    NPC.ai[2] = 0f;
                    NPC.ai[3] = 0f;
                    return;
                }
                NPC.ai[2] += 1f;
                if (NPC.ai[3] == 0f)
                {
                    if (NPC.ai[2] > 120f)
                    {
                        NPC.ai[2] = 0f;
                        NPC.ai[3] = 1f;
                        NPC.netUpdate = true;
                        return;
                    }
                }
                else
                {
                    if (NPC.ai[2] > 40f)
                    {
                        NPC.ai[3] = 0f;
                    }
                    if (Main.netMode != 1 && NPC.ai[2] == 20f)
                    {
                        float num151 = 5f;
                        int num152 = 60;
                        int num153 = Mod.Find<ModProjectile>("PhantomHookShot").Type;
                        num149 = num151 / num149;
                        num147 *= num149;
                        num148 *= num149;
                        int num154 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector17.X, vector17.Y, num147, num148, num153, num152, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
                return;
            }
            if (Main.netMode == 1)
            {
                if (NPC.ai[0] == 0f)
                {
                    NPC.ai[0] = (float)((int)(NPC.Center.X / 16f));
                }
                if (NPC.ai[1] == 0f)
                {
                    NPC.ai[1] = (float)((int)(NPC.Center.X / 16f));
                }
            }
            if (Main.netMode != 1)
            {
                if (NPC.ai[0] == 0f || NPC.ai[1] == 0f)
                {
                    NPC.localAI[0] = 0f;
                }
                NPC.localAI[0] -= 5f;
                if (speedBoost1)
                {
                    NPC.localAI[0] -= 10f;
                }
                if (!despawnBoost && NPC.localAI[0] <= 0f && NPC.ai[0] != 0f)
                {
                    int num;
                    for (int num763 = 0; num763 < 200; num763 = num + 1)
                    {
                        if (num763 != NPC.whoAmI && Main.npc[num763].active && Main.npc[num763].type == NPC.type && (Main.npc[num763].velocity.X != 0f || Main.npc[num763].velocity.Y != 0f))
                        {
                            NPC.localAI[0] = (float)Main.rand.Next(60, 300);
                        }
                        num = num763;
                    }
                }
                if (NPC.localAI[0] <= 0f)
                {
                    NPC.localAI[0] = (float)Main.rand.Next(300, 600);
                    bool flag50 = false;
                    int num764 = 0;
                    while (!flag50 && num764 <= 1000)
                    {
                        num764++;
                        int num765 = (int)(Main.player[Main.npc[CalamityGlobalNPC.ghostBoss].target].Center.X / 16f);
                        int num766 = (int)(Main.player[Main.npc[CalamityGlobalNPC.ghostBoss].target].Center.Y / 16f);
                        if (NPC.ai[0] == 0f)
                        {
                            num765 = (int)((Main.player[Main.npc[CalamityGlobalNPC.ghostBoss].target].Center.X + Main.npc[CalamityGlobalNPC.ghostBoss].Center.X) / 32f);
                            num766 = (int)((Main.player[Main.npc[CalamityGlobalNPC.ghostBoss].target].Center.Y + Main.npc[CalamityGlobalNPC.ghostBoss].Center.Y) / 32f);
                        }
                        if (despawnBoost)
                        {
                            num765 = (int)Main.npc[CalamityGlobalNPC.ghostBoss].position.X / 16;
                            num766 = (int)(Main.npc[CalamityGlobalNPC.ghostBoss].position.Y + 400f) / 16;
                        }
                        int num767 = 20;
                        num767 += (int)(100f * ((float)num764 / 1000f));
                        int num768 = num765 + Main.rand.Next(-num767, num767 + 1);
                        int num769 = num766 + Main.rand.Next(-num767, num767 + 1);
                        try
                        {
                            if (WorldGen.SolidTile(num768, num769) || (Main.tile[num768, num769].WallType > 0 && (num764 > 500 || Main.npc[CalamityGlobalNPC.ghostBoss].life < Main.npc[CalamityGlobalNPC.ghostBoss].lifeMax / 2)))
                            {
                                flag50 = true;
                                NPC.ai[0] = (float)num768;
                                NPC.ai[1] = (float)num769;
                                NPC.netUpdate = true;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            if (NPC.ai[0] > 0f && NPC.ai[1] > 0f)
            {
                float num772 = 11f;
                if (expertMode)
                {
                    num772 += 1f;
                }
                if (speedBoost1)
                {
                    num772 *= 2f;
                }
                if (despawnBoost)
                {
                    num772 *= 2f;
                }
                Vector2 vector95 = new Vector2(NPC.Center.X, NPC.Center.Y);
                float num773 = NPC.ai[0] * 16f - 8f - vector95.X;
                float num774 = NPC.ai[1] * 16f - 8f - vector95.Y;
                float num775 = (float)Math.Sqrt((double)(num773 * num773 + num774 * num774));
                if (num775 < 12f + num772)
                {
                    NPC.velocity.X = num773;
                    NPC.velocity.Y = num774;
                }
                else
                {
                    num775 = num772 / num775;
                    NPC.velocity.X = num773 * num775;
                    NPC.velocity.Y = num774 * num775;
                }
                Vector2 vector96 = new Vector2(NPC.Center.X, NPC.Center.Y);
                float num776 = Main.npc[CalamityGlobalNPC.ghostBoss].Center.X - vector96.X;
                float num777 = Main.npc[CalamityGlobalNPC.ghostBoss].Center.Y - vector96.Y;
                NPC.rotation = (float)Math.Atan2((double)num777, (double)num776) - 1.57f;
            }
        }
		
		public override void FindFrame(int frameHeight)
		{
            if (phase2)
            {
                if (NPC.ai[3] == 0f)
                {
                    if (NPC.frame.Y < 1)
                    {
                        NPC.frameCounter += 1.0;
                        if (NPC.frameCounter > 4.0)
                        {
                            NPC.frameCounter = 0.0;
                            NPC.frame.Y = NPC.frame.Y + frameHeight;
                        }
                    }
                }
                else if (NPC.frame.Y > 0)
                {
                    NPC.frameCounter += 1.0;
                    if (NPC.frameCounter > 4.0)
                    {
                        NPC.frameCounter = 0.0;
                        NPC.frame.Y = NPC.frame.Y - frameHeight;
                    }
                }
                return;
            }
            if (NPC.velocity.X == 0f && NPC.velocity.Y == 0f)
			{
				if (NPC.frame.Y < 1)
				{
					NPC.frameCounter += 1.0;
					if (NPC.frameCounter > 4.0)
					{
						NPC.frameCounter = 0.0;
						NPC.frame.Y = NPC.frame.Y + frameHeight;
					}
				}
			}
			else if (NPC.frame.Y > 0)
			{
				NPC.frameCounter += 1.0;
				if (NPC.frameCounter > 4.0)
				{
					NPC.frameCounter = 0.0;
					NPC.frame.Y = NPC.frame.Y - frameHeight;
				}
			}
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (Main.npc[CalamityGlobalNPC.ghostBoss].active && !phase2) 
			{
				Vector2 center = new Vector2(NPC.Center.X, NPC.Center.Y);
				float bossCenterX = Main.npc[CalamityGlobalNPC.ghostBoss].Center.X - center.X;
				float bossCenterY = Main.npc[CalamityGlobalNPC.ghostBoss].Center.Y - center.Y;
				float rotation2 = (float)Math.Atan2((double)bossCenterY, (double)bossCenterX) - 1.57f;
				bool draw = true;
				while (draw) 
				{
					int chainWidth = 20; //16 24
					int chainHeight = 52; //32 16
					float num10 = (float)Math.Sqrt((double)(bossCenterX * bossCenterX + bossCenterY * bossCenterY));
					if (num10 < (float)chainHeight) 
					{
						chainWidth = (int)num10 - chainHeight + chainWidth;
						draw = false;
					}
					num10 = (float)chainWidth / num10;
					bossCenterX *= num10;
					bossCenterY *= num10;
					center.X += bossCenterX;
					center.Y += bossCenterY;
					bossCenterX = Main.npc[CalamityGlobalNPC.ghostBoss].Center.X - center.X;
					bossCenterY = Main.npc[CalamityGlobalNPC.ghostBoss].Center.Y - center.Y;
					Microsoft.Xna.Framework.Color color2 = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16f));
					Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Polterghast/PolterghastChain").Value, new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y), 
						new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Polterghast/PolterghastChain").Value.Width, chainWidth)), color2, rotation2, 
						new Vector2((float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Polterghast/PolterghastChain").Value.Width * 0.5f, (float)ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/Polterghast/PolterghastChain").Value.Height * 0.5f), 1f, SpriteEffects.None, 0f);
				}
			}
			return true;
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
            if (CalamityWorldPreTrailer.revenge)
			    target.AddBuff(Mod.Find<ModBuff>("Horror").Type, 180, true);
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 180, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 180, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}