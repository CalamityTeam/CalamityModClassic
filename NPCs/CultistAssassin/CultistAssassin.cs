using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Projectiles;
using CalamityModClassic1Point1.Items;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.CultistAssassin
{
	public class CultistAssassin : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Cultist Assassin");
			//Tooltip.SetDefault("Cultist Assassin");
			NPC.aiStyle = 3;
			NPC.damage = 100;
			NPC.width = 18; //324
			NPC.height = 40; //216
			NPC.defense = 25;
			NPC.lifeMax = 500;
			NPC.knockBackResist = 0.75f;
			AnimationType = 331;
			AIType = NPCID.ChaosElemental;
			Main.npcFrameCount[NPC.type] = 4;
			NPC.value = Item.buyPrice(0, 2, 0, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath50;
		}

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("So are they cultists that are assassins or assassins that assassinate cultists?")

            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
			return (spawnInfo.Player.ZoneDungeon || spawnInfo.Player.GetModPlayer<CalamityPlayer>().ZoneCalamity) && Main.hardMode ? 0.035f : 0f;
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
			NPC.damage = (int)(NPC.damage * 0.65f);
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 5, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<EssenceofChaos>(), 1));
        }
    }
}