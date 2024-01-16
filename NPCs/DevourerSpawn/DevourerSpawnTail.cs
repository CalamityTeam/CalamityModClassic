using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;

namespace CalamityModClassic1Point1.NPCs.DevourerSpawn
{
	public class DevourerSpawnTail : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
        public override void SetDefaults()
		{
			//NPC.name = "Devourer Spawn Tail");
			//Tooltip.SetDefault("Devourer Spawn");
			NPC.damage = 35;
			NPC.width = 20; //324
			NPC.height = 20; //216
			NPC.defense = 45;
			NPC.lifeMax = 20000;
			NPC.knockBackResist = 0f;
			NPC.alpha = 255;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.netAlways = true;
			NPC.buffImmune[39] = true;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
			NPC.dontCountMe = true;
		}
		
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
		
		public override void AI()
		{
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
			if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.active = false;
            }
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 173, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 173, hit.HitDirection, -1f, 0, default(Color), 1f);
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
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.85f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.Chilled, 180, true);
			}
			else
			{
				target.AddBuff(BuffID.Chilled, 90, true);
			}
		}
	}
}