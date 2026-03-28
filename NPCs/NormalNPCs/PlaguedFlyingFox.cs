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
	public class PlaguedFlyingFox : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Melter");
			Main.npcFrameCount[NPC.type] = 4;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
				new FlavorTextBestiaryInfoElement("A flying fox overtaken by plague nanomachines, it now serves the plague completely.")
			});
		}
		
		public override void SetDefaults()
		{
            NPC.npcSlots = 0.5f;
            NPC.aiStyle = 14;
            AIType = 152;
            AnimationType = 152;
			NPC.damage = 55;
			NPC.width = 38; //324
			NPC.height = 34; //216
			NPC.defense = 35;
			NPC.lifeMax = 500;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 10, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath4;
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
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("MelterBanner").Type;
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            if (spawnInfo.PlayerSafe || !NPC.downedGolemBoss || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSunkenSea)
            {
                return 0f;
            }
            return SpawnCondition.HardmodeJungle.Chance * 0.09f;
        }
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 300, true);
        }
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 46, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 46, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("PlagueCellCluster").Type, 1, 1, 3));
		}
	}
}