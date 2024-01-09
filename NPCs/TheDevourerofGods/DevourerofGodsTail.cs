using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;

namespace CalamityModClassic1Point2.NPCs.TheDevourerofGods
{
	public class DevourerofGodsTail : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Devourer of Gods");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
		
		public override void SetDefaults()
		{
			NPC.damage = 200;
			NPC.npcSlots = 5f;
			NPC.width = 38; //42
			NPC.height = 38; //42
			NPC.defense = 0;
			NPC.lifeMax = CalamityWorld.revenge ? 800000 : 750000;
			if (NPC.CountNPCS(Mod.Find<ModNPC>("DevourerofGodsHead").Type) > 0)
			{
				NPC.lifeMax = 600000;
			}
			NPC.aiStyle = 6; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.scale = 1.4f;
			NPC.alpha = 255;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.netAlways = true;
			NPC.boss = true;
			NPC.takenDamageMultiplier = 2f;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			Music = MusicLoader.GetMusicSlot(Mod, "CalamityModClassic1Point2/Sounds/Music/CaveStoryBossBattle");
			NPC.dontCountMe = true;
			if (Main.expertMode)
			{
				NPC.scale = 1.5f;
			}
		}
		
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
		
		public override void AI()
		{
			if (Main.player[(int)Player.FindClosest(NPC.position, NPC.width, NPC.height)].GetModPlayer<CalamityPlayer>().warped)
			{
				NPC.dontTakeDamage = true;
				NPC.chaseable = false;
			}
			else
			{
				NPC.dontTakeDamage = false;
				NPC.chaseable = true;
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
						int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.TheDestroyer, 0f, 0f, 100, default(Color), 2f);
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
		
		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			Player player = Main.player[NPC.target];
			if (player.vortexStealthActive && projectile.CountsAsClass(DamageClass.Ranged))
			{
				modifiers.FinalDamage.Base /= 2;
				modifiers.DisableCrit();
			}
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
				Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DoGTail").Type, 1f);
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 50;
				NPC.height = 50;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 10; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 20; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.ShadowbeamStaff, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			if (modifiers.FinalDamage.Base > NPC.lifeMax / 2)
			{
				modifiers.SetMaxDamage(1);
			}
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 150, true);
			int num = Main.rand.Next(2);
			string key = "Are you honestly that bad at dodging?";
			if (num == 0)
			{
				key = "Are you honestly that bad at dodging?";
			}
			else if (num == 1)
			{
				key = "Of all my segments to get hit by...";
			}
			Color messageColor = Color.Cyan;
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				Main.NewText((key), messageColor);
			}
			else if (Main.netMode == NetmodeID.Server)
			{
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(key), messageColor);
			}
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Frostburn, 100, true);
				target.AddBuff(BuffID.Darkness, 100, true);
			}
		}
	}
}