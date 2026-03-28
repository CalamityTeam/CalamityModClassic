using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.PlaguebringerGoliath
{
	public class PlagueHomingMissile : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plague Homing Missile");
			Main.npcFrameCount[NPC.type] = 4;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 90;
			NPC.width = 22; //324
			NPC.height = 22; //216
			NPC.defense = 10;
			NPC.lifeMax = 200;
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.knockBackResist = 0f;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.noGravity = true;
			NPC.canGhostHeal = false;
			NPC.buffImmune[189] = true;
			NPC.buffImmune[153] = true;
			NPC.buffImmune[70] = true;
			NPC.buffImmune[69] = true;
			NPC.buffImmune[44] = true;
			NPC.buffImmune[39] = true;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
		}
		
		public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.015f, 0.1f, 0f);
            if (Math.Abs(NPC.velocity.X) >= 3f || Math.Abs(NPC.velocity.Y) >= 3f)
            {
                float num247 = 0f;
                float num248 = 0f;
                if (Main.rand.Next(2) == 1)
                {
                    num247 = NPC.velocity.X * 0.5f;
                    num248 = NPC.velocity.Y * 0.5f;
                }
                int num249 = Dust.NewDust(new Vector2(NPC.position.X + 3f + num247, NPC.position.Y + 3f + num248) - NPC.velocity * 0.5f, NPC.width - 8, NPC.height - 8, 6, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num249].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                Main.dust[num249].velocity *= 0.2f;
                Main.dust[num249].noGravity = true;
                num249 = Dust.NewDust(new Vector2(NPC.position.X + 3f + num247, NPC.position.Y + 3f + num248) - NPC.velocity * 0.5f, NPC.width - 8, NPC.height - 8, 31, 0f, 0f, 100, default(Color), 0.25f);
                Main.dust[num249].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                Main.dust[num249].velocity *= 0.05f;
            }
            else if (Main.rand.Next(4) == 0)
            {
                int num252 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 31, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num252].scale = 0.1f + (float)Main.rand.Next(5) * 0.1f;
                Main.dust[num252].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
                Main.dust[num252].noGravity = true;
                Main.dust[num252].position = NPC.Center + new Vector2(0f, (float)(-(float)NPC.height / 2)).RotatedBy((double)NPC.rotation, default(Vector2)) * 1.1f;
                Main.rand.Next(2);
                num252 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 6, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num252].scale = 1f + (float)Main.rand.Next(5) * 0.1f;
                Main.dust[num252].noGravity = true;
                Main.dust[num252].position = NPC.Center + new Vector2(0f, (float)(-(float)NPC.height / 2 - 6)).RotatedBy((double)NPC.rotation, default(Vector2)) * 1.1f;
            }
            NPC.rotation = NPC.velocity.ToRotation() + 1.57079637f;
            if (NPC.ai[2] < 90f)
            {
                NPC.ai[2] += 1f;
                return;
            }
            int num3;
            if (NPC.ai[0] == 0f && NPC.ai[1] > 0f)
            {
                float[] var_2_2065C_cp_0 = NPC.ai;
                int var_2_2065C_cp_1 = 1;
                float num73 = var_2_2065C_cp_0[var_2_2065C_cp_1];
                var_2_2065C_cp_0[var_2_2065C_cp_1] = num73 - 1f;
            }
            else if (NPC.ai[0] == 0f && NPC.ai[1] == 0f)
            {
                NPC.ai[0] = 1f;
                NPC.ai[1] = (float)Player.FindClosest(NPC.position, NPC.width, NPC.height);
                NPC.netUpdate = true;
                float num754 = NPC.velocity.Length();
                NPC.velocity = Vector2.Normalize(NPC.velocity) * (num754 + 1f);
            }
            else if (NPC.ai[0] == 1f)
            {
                float[] var_2_2087A_cp_0 = NPC.localAI;
                int var_2_2087A_cp_1 = 1;
                float num73 = var_2_2087A_cp_0[var_2_2087A_cp_1];
                var_2_2087A_cp_0[var_2_2087A_cp_1] = num73 + 1f;
                float num757 = 600f;
                float num758 = 0f;
                float num759 = 300f;
                if (NPC.localAI[1] == num757)
                {
                    CheckDead();
                    NPC.life = 0;
                    return;
                }
                if (NPC.localAI[1] >= num758 && NPC.localAI[1] < num758 + num759)
                {
                    NPC.noTileCollide = true;
                    Vector2 v3 = Main.player[(int)NPC.ai[1]].Center - NPC.Center;
                    float num760 = NPC.velocity.ToRotation();
                    float num761 = v3.ToRotation();
                    double num762 = (double)(num761 - num760);
                    if (num762 > 3.1415926535897931)
                    {
                        num762 -= 6.2831853071795862;
                    }
                    if (num762 < -3.1415926535897931)
                    {
                        num762 += 6.2831853071795862;
                    }
                    NPC.velocity = NPC.velocity.RotatedBy(num762 * 0.20000000298023224, default(Vector2));
                }
                else
                {
                    NPC.noTileCollide = false;
                }
            }
            for (int num767 = 0; num767 < 255; num767 = num3 + 1)
            {
                Player player5 = Main.player[num767];
                if (player5.active && !player5.dead && Vector2.Distance(player5.Center, NPC.Center) <= 42f)
                {
                    CheckDead();
                    NPC.life = 0;
                    return;
                }
                num3 = num767;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override bool CheckDead()
        {
            SoundEngine.PlaySound(SoundID.Item14, NPC.position);
            NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
            NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
            NPC.width = (NPC.height = 216);
            NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
            NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
            for (int num621 = 0; num621 < 15; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 89, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num622].velocity *= 3f;
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num622].scale = 0.5f;
                    Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                }
                Main.dust[num622].noGravity = true;
            }
            for (int num623 = 0; num623 < 30; num623++)
            {
                int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 89, 0f, 0f, 100, default(Color), 3f);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 5f;
                num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 89, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num624].velocity *= 2f;
                Main.dust[num624].noGravity = true;
            }
            return true;
        }

        public override bool PreKill()
		{
			return false;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 180, true);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 46, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 46, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}