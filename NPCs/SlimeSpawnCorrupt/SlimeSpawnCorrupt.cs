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

namespace CalamityModClassic1Point2.NPCs.SlimeSpawnCorrupt
{
	public class SlimeSpawnCorrupt : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Corrupt Slime Spawn");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 14;
			NPC.damage = 50;
			NPC.width = 40; //324
			NPC.height = 30; //216
			NPC.defense = 10;
			NPC.lifeMax = 50;
			NPC.knockBackResist = 0f;
			AnimationType = 121;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.alpha = 60;
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.buffImmune[24] = true;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
                new FlavorTextBestiaryInfoElement("A winged slime harboring infection.")

            });
        }
        public override bool PreKill()
		{
			return false;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient && NPC.life <= 0)
			{
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("SlimeSpawnCorrupt2").Type);
			}
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(BuffID.ManaSickness, 60, true);
			}
		}
	}
}