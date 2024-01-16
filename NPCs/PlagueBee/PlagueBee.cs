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
using CalamityModClassic1Point1.Items.PlaguebringerGoliath;
using Terraria.GameContent.Bestiary;

namespace CalamityModClassic1Point1.NPCs.PlagueBee
{
	public class PlagueBee : ModNPC
	{
		public override void SetDefaults()
		{
			//NPC.name = "Plague Bee");
			//Tooltip.SetDefault("Plague Charger");
			NPC.damage = 48;
			NPC.width = 36; //324
			NPC.height = 30; //216
			NPC.defense = 20;
			NPC.scale = 0.5f;
			NPC.lifeMax = 600;
			NPC.aiStyle = 5;
			AIType = 210;
			NPC.knockBackResist = 0f;
			Main.npcFrameCount[NPC.type] = 4;
			AnimationType = 210;
			NPC.value = Item.buyPrice(0, 0, 8, 0);
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.noGravity = true;
			NPC.noTileCollide = false;
			NPC.buffImmune[189] = true;
			NPC.buffImmune[153] = true;
			NPC.buffImmune[70] = true;
			NPC.buffImmune[69] = true;
			NPC.buffImmune[44] = true;
			NPC.buffImmune[39] = true;
			NPC.buffImmune[24] = true;
			NPC.buffImmune[20] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = true;
			NPC.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
                new FlavorTextBestiaryInfoElement("A common hornet overtaken by the plague.")

            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(ModContent.ItemType<PlagueCellCluster>(), 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return spawnInfo.Player.ZoneJungle && NPC.downedGolemBoss ? 0.025f : 0f; 
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.65f * balance);
			NPC.damage = (int)(NPC.damage * 0.55f);
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
				target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 280, true);
			}
			else
			{
				target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 260, true);
			}
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 46, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 46, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}