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

namespace CalamityModClassicPreTrailer.NPCs.Calamitas
{
	public class LifeSeeker : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Life Seeker");
			Main.npcFrameCount[NPC.type] = 2;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				new FlavorTextBestiaryInfoElement("Sentries for the clone, their lasers prove deadly.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.npcSlots = 1f;
			NPC.damage = 35;
			NPC.width = 44; //324
			NPC.height = 30; //216
			NPC.defense = 20;
			NPC.lifeMax = 200;
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = 30000;
            }
            NPC.aiStyle = 5;
			AIType = 139;
			NPC.knockBackResist = 0.25f;
			AnimationType = 2;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.canGhostHeal = false;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.buffImmune[24] = true;
		}
		
		public override bool PreKill()
		{
			return false;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 120, true);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color), 1f);
			}

			if (NPC.life <= 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					for (int k = 0; k < 20; k++)
					{
						Dust.NewDust(NPC.position, NPC.width, NPC.height, 235, hit.HitDirection, -1f, 0, default(Color),
							1f);
					}

					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("LifeSeeker").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("LifeSeeker2").Type, 1f);
					Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity,
						Mod.Find<ModGore>("LifeSeeker3").Type, 1f);
				}
			}
		}
	}
}