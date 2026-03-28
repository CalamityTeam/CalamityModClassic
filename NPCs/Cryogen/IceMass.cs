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

namespace CalamityModClassicPreTrailer.NPCs.Cryogen
{
	public class IceMass : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aurora Spirit");
			Main.npcFrameCount[NPC.type] = 6;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
				new FlavorTextBestiaryInfoElement("The souls of those who passed away in the deadly tundra, they now seek to freeze others and doom them to a similar fate.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 86;
			NPC.damage = 40;
			NPC.width = 40; //324
			NPC.height = 24; //216
			NPC.defense = 5;
			NPC.alpha = 100;
			NPC.lifeMax = 220;
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = 30000;
            }
            NPC.knockBackResist = 0f;
			AnimationType = 472;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath15;
		}

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Frostburn, 90, true);
            target.AddBuff(BuffID.Chilled, 60, true);
        }

        public override void AI()
		{
			Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0.01f, 0.35f, 0.35f);
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 67, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					for (int k = 0; k < 20; k++)
					{
						Dust.NewDust(NPC.position, NPC.width, NPC.height, 67, hit.HitDirection, -1f, 0, default(Color),
							1f);
					}

					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("CryoSpirit").Type, 1f);
				}
			}
		}
	}
}