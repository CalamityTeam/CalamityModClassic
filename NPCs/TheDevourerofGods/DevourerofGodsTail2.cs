using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.TheDevourerofGods
{
	public class DevourerofGodsTail2 : ModNPC
	{
        public int invinceTime = 180;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Guardian");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 100;
			NPC.npcSlots = 5f;
			NPC.width = 30; //42
			NPC.height = 50; //42
			NPC.defense = 0;
            NPC.lifeMax = 100000; //192000
            NPC.aiStyle = 6; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.alpha = 255;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.netAlways = true;
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
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.2f, 0.05f, 0.2f);
            if (invinceTime > 0)
            {
                invinceTime--;
                NPC.damage = 0;
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.damage = Main.expertMode ? 200 : 100;
                NPC.dontTakeDamage = false;
            }
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
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 182, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
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

        public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
                float randomSpread = (float)(Main.rand.Next(-100, 100) / 100);
                if (Main.netMode != NetmodeID.Server)
                {
	                Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position,
		                NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("DoT2").Type, 1f);
	                Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position,
		                NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("DoT3").Type, 1f);
	                Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position,
		                NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("DoT4").Type, 1f);
	                Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position,
		                NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("DoT5").Type, 1f);
                }
                NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 50;
				NPC.height = 50;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 10; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 20; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
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
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("GodSlayerInferno").Type, 90, true);
			target.AddBuff(BuffID.Frostburn, 90, true);
			target.AddBuff(BuffID.Darkness, 90, true);
		}
    }
}