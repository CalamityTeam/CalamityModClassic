using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassicPreTrailer.NPCs.Leviathan
{
	public class Parasea : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Parasea");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
				new FlavorTextBestiaryInfoElement("These strange serpents are some of the Leviathan's spawn.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			NPC.damage = 45;
			NPC.width = 90; //324
			NPC.height = 20; //216
			NPC.defense = 9;
			NPC.lifeMax = 650;
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = 50000;
            }
            NPC.knockBackResist = 0f;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("ParaseaBanner").Type;
		}

        public override void AI()
		{
			bool revenge = CalamityWorldPreTrailer.revenge;
			NPC.TargetClosest(true);
			Vector2 vector145 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num1258 = Main.player[NPC.target].Center.X - vector145.X;
			float num1259 = Main.player[NPC.target].Center.Y - vector145.Y;
			float num1260 = (float)Math.Sqrt((double)(num1258 * num1258 + num1259 * num1259));
			float num1261 = revenge ? 16f : 13f;
			num1260 = num1261 / num1260;
			num1258 *= num1260;
			num1259 *= num1260;
			NPC.velocity.X = (NPC.velocity.X * 100f + num1258) / 101f;
			NPC.velocity.Y = (NPC.velocity.Y * 100f + num1259) / 101f;
			NPC.rotation = (float)Math.Atan2((double)num1259, (double)num1258) + 3.14f; //1.57
			return;
		}
		
		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Wet, 60, true);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}