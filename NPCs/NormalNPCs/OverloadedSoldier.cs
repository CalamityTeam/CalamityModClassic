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
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace CalamityModClassicPreTrailer.NPCs.NormalNPCs
{
	public class OverloadedSoldier : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Overloaded Soldier");
			Main.npcFrameCount[NPC.type] = 15;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
				new FlavorTextBestiaryInfoElement("The corpse of a soldier supercharged with Phantoplasm, and reanimated by vengeful spirits in search for revenge.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 3;
			NPC.damage = 42;
			NPC.width = 18; //324
			NPC.height = 40; //216
			NPC.defense = 28;
			NPC.lifeMax = 90;
			NPC.knockBackResist = 0.5f;
			AnimationType = 77;
			NPC.value = Item.buyPrice(0, 0, 2, 0);
			NPC.HitSound = SoundID.NPCHit2;
			NPC.DeathSound = SoundID.NPCDeath2;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("OverloadedSoldierBanner").Type;
		}

        public override void AI()
        {
            NPC.velocity.X *= 1.05f;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.PlayerSafe || !Main.hardMode || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAbyss ||
				spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
            {
                return 0f;
            }
            return SpawnCondition.Underground.Chance * 0.02f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (CalamityWorldPreTrailer.revenge)
            {
                target.AddBuff(Mod.Find<ModBuff>("MarkedforDeath").Type, 120);
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 60, hit.HitDirection, -1f, 0, default(Color), 1f);
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 15; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 60, hit.HitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }
        
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
	        npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("AncientBoneDust").Type, 1));
	        npcLoot.Add(ItemDropRule.ByCondition(new DownedMoonLord(), Mod.Find<ModItem>("Phantoplasm").Type));
        }
	}
}