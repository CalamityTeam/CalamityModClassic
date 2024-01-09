using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Projectiles;

namespace CalamityModClassic1Point0.NPCs.SlimeSpawnCorrupt2
{
	public class SlimeSpawnCorrupt2 : ModNPC
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
			//NPC.name = "Corrupt Slime Spawn");
			//DisplayName.SetDefault("Corrupt Slime Spawn");
			NPC.aiStyle = 1;
			NPC.damage = 60;
			NPC.width = 40; //324
			NPC.height = 30; //216
			NPC.defense = 20;
			NPC.lifeMax = 150;
			NPC.knockBackResist = 0f;
			AnimationType = 81;
			Main.npcFrameCount[NPC.type] = 2;
			NPC.value = Item.buyPrice(0, 0, 4, 0);
			NPC.alpha = 60;
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound= SoundID.NPCHit1;
			NPC.DeathSound= SoundID.NPCDeath1;
			NPC.buffImmune[24] = true;
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.5f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode || Main.rand.Next(1) == 0)
			{
				target.AddBuff(BuffID.ManaSickness, 300, true);
			}
		}
	}
}