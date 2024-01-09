using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Projectiles;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point0.NPCs.SlimeSpawnCrimson
{
	public class SlimeSpawnCrimson : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Crimson Slime Spawn");
			//DisplayName.SetDefault("Crimson Slime Spawn");
			NPC.aiStyle = 1;
			NPC.damage = 90;
			NPC.width = 40; //324
			NPC.height = 30; //216
			NPC.defense = 25;
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
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            int associatedNPCType = ModContent.NPCType<SlimeGod.SlimeGod>();
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
                new FlavorTextBestiaryInfoElement("Slimes are extraordinarily hardy creatures: if a large slime has chunks of living gelatin severed from it, the chunks tend to continue on, albeit as their own beings.They even retain the memories of the original slime, and so continue to work in the interest of their \"parent\".")
           });
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
			NPC.damage = (int)(NPC.damage * 0.5f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode || Main.rand.Next(3) == 0)
			{
				target.AddBuff(BuffID.ManaSickness, 300, true);
				target.AddBuff(BuffID.Cursed, 150, true);
			}
		}
	}
}