using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.StormWeaver
{
	public class StormWeaverBody : ModNPC
	{
		public int spawn = 0;
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Storm Weaver");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 100; //70
			NPC.npcSlots = 5f;
			NPC.width = 40; //324
			NPC.height = 40; //216
			NPC.defense = 99999;
            NPC.lifeMax = 20000;
            Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/ScourgeofTheUniverse");
            if (CalamityWorldPreTrailer.DoGSecondStageCountdown <= 0)
            {
	            Music = MusicLoader.GetMusicSlot("CalamityModClassicPreTrailer/Sounds/Music/Weaver");
                NPC.lifeMax = 100000;
            }
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = 170000;
            }
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			NPC.lifeMax += (int)((double)NPC.lifeMax * HPBoost);
			NPC.aiStyle = 6; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.alpha = 255;
			NPC.boss = true;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.netAlways = true;
            NPC.chaseable = false;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
            NPC.dontCountMe = true;
		}
		
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
		
		public override void AI()
		{
			bool expertMode = Main.expertMode;
            if (NPC.defense < 99999 && CalamityWorldPreTrailer.DoGSecondStageCountdown <= 0)
            {
                NPC.defense = 99999;
            }
            else
            {
                NPC.defense = 0;
            }
            Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.2f, 0.05f, 0.2f);
			if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.active = false;
            }
			if (Main.npc[(int)NPC.ai[1]].alpha < 128)
			{
				if (NPC.alpha != 0)
				{
					for (int num934 = 0; num934 < 2; num934++)
					{
						int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 182, 0f, 0f, 100, default(Color), 2f);
						Main.dust[num935].noGravity = true;
						Main.dust[num935].noLight = true;
					}
				}
				NPC.alpha -= 42;
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
				}
			}
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			modifiers.SetMaxDamage(1);
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			if (projectile.penetrate == -1 && !projectile.minion)
			{
				projectile.penetrate = 1;
			}
			else if (projectile.penetrate >= 1)
			{
				projectile.penetrate = 1;
			}
		}

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
	            if (Main.netMode != NetmodeID.Server)
	            {
		            Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
			            Mod.Find<ModGore>("SWArmor2").Type, 1f);
		            Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
			            Mod.Find<ModGore>("SWArmor3").Type, 1f);
		            Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
			            Mod.Find<ModGore>("SWArmor4").Type, 1f);
	            }
	            NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
                NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
                NPC.width = 30;
                NPC.height = 30;
                NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
                NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
                for (int num621 = 0; num621 < 20; num621++)
                {
                    int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num622].velocity *= 3f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num622].scale = 0.5f;
                        Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int num623 = 0; num623 < 40; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 5f;
                    num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num624].velocity *= 2f;
                }
            }
            if (NPC.CountNPCS(Mod.Find<ModNPC>("StasisProbe").Type) < 3)
            {
                if (NPC.life > 0 && Main.netMode != 1 && spawn == 0 && Main.rand.Next(15) == 0)
                {
                    spawn = 1;
                    int num660 = NPC.NewNPC(NPC.GetSource_FromThis(null), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), Mod.Find<ModNPC>("StasisProbe").Type, 0, 0f, 0f, 0f, 0f, 255);
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(23, -1, -1, null, num660, 0f, 0f, 0f, 0, 0, 0);
                    }
                    NPC.netUpdate = true;
                }
            }
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
	}
}