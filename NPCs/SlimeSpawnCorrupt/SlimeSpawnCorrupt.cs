using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Projectiles;
using CalamityModClassic1Point0.NPCs.DesertScourge;
using Terraria.GameContent.Bestiary;
using CalamityModClassic1Point0.NPCs.SlimeGod;

namespace CalamityModClassic1Point0.NPCs.SlimeSpawnCorrupt
{
	public class SlimeSpawnCorrupt : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Corrupt Slime Spawn");
			//DisplayName.SetDefault("Corrupt Slime Spawn");
			NPC.aiStyle = 14;
			NPC.damage = 60;
			NPC.width = 40; //324
			NPC.height = 30; //216
			NPC.defense = 20;
			NPC.lifeMax = 120;
			NPC.knockBackResist = 0f;
			AnimationType = 121;
			Main.npcFrameCount[NPC.type] = 4;
			NPC.value = Item.buyPrice(0, 0, 5, 0);
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
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (Main.netMode != 1 && NPC.life <= 0)
			{
				Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
				NPC.NewNPC(NPC.GetSource_Death(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("SlimeSpawnCorrupt2").Type);
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.5f);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            int associatedNPCType = ModContent.NPCType<SlimeGod.SlimeGod>();
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
                new FlavorTextBestiaryInfoElement("Slimes are extraordinarily hardy creatures: if a large slime has chunks of living gelatin severed from it, the chunks tend to continue on, albeit as their own beings. Even as gel, a slime is still technically alive– it simply exists in a state where it cannot function in any meaningful way. ")
            });
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