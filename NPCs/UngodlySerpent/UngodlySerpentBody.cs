using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;

namespace CalamityModClassic1Point1.NPCs.UngodlySerpent
{
	public class UngodlySerpentBody : ModNPC
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
			//NPC.name = "UngodlySerpentBody");
			//Tooltip.SetDefault("Ungodly Serpent");
			NPC.damage = 90; //70
			NPC.npcSlots = 3f;
			NPC.width = 32; //324
			NPC.height = 32; //216
			NPC.defense = 38;
			NPC.lifeMax = 55225; //250000
			NPC.aiStyle = 6; //new
			Main.npcFrameCount[NPC.type] = 1; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.alpha = 255;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.lavaImmune = true;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit8;
			NPC.DeathSound = SoundID.NPCDeath8;
			NPC.netAlways = true;
			NPC.dontCountMe = true;
		}
		
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
		
		public override void AI()
		{
			if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.active = false;
            }
			if (Main.npc[(int)NPC.ai[1]].alpha < 128)
			{
				NPC.alpha -= 42;
				if (NPC.alpha < 0)
				{
					NPC.alpha = 0;
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
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 244, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 30; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 244, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.55f * balance);
			NPC.damage = (int)(NPC.damage * 0.65f);
		}
	}
}