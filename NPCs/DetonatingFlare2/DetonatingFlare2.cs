using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point2.NPCs.DetonatingFlare2
{
	public class DetonatingFlare2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Detonating Flame");
			Main.npcFrameCount[NPC.type] = 3;
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.damage = 300;
			NPC.width = 50; //324
			NPC.height = 50; //216
			NPC.defense = 150;
			NPC.lifeMax = 25000;
			NPC.knockBackResist = 0f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit52;
			NPC.DeathSound = SoundID.NPCDeath55;
			NPC.alpha = 255;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
                new FlavorTextBestiaryInfoElement("Fire2.")

            });
        }

        public override void AI()
		{
			bool revenge = CalamityWorld.revenge;
			NPC.alpha -= 3;
			NPC.TargetClosest(true);
			Vector2 vector98 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num790 = Main.player[NPC.target].Center.X - vector98.X;
			float num791 = Main.player[NPC.target].Center.Y - vector98.Y;
			float num792 = (float)Math.Sqrt((double)(num790 * num790 + num791 * num791));
			float num793 = NPC.localAI[3] + (revenge ? 2f : 0f);
			num792 = num793 / num792;
			float random = (float)Main.rand.Next(3, 7);
			float randomXVelocity = random / 100;
			num790 *= num792 + randomXVelocity;
			num791 *= num792;
			NPC.velocity.X = (NPC.velocity.X * 100f + num790) / 101f;
			NPC.velocity.Y = (NPC.velocity.Y * 100f + num791) / 101f;
			NPC.rotation = (float)Math.Atan2((double)num791, (double)num790) - 1.57f;
			return;
		}
		
		public override Color? GetAlpha(Color drawColor)
		{
			return new Color(255, Main.DiscoG, 53, 0);
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
	}
}