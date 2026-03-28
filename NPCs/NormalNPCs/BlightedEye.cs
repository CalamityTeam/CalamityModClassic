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
	public class BlightedEye : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blighted Eye");
			Main.npcFrameCount[NPC.type] = 2;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				new FlavorTextBestiaryInfoElement("An eye similar to the Demon Eye, it behaves much the same.")
			});
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = 2;
			NPC.damage = 32;
			NPC.width = 30; //324
			NPC.height = 32; //216
			NPC.defense = 18;
			NPC.lifeMax = 120;
			NPC.knockBackResist = 0.6f;
			AnimationType = 2;
			NPC.value = Item.buyPrice(0, 0, 2, 0);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("BlightedEyeBanner").Type;
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
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe || !Main.hardMode || spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneSulphur)
			{
				return 0f;
			}
			return SpawnCondition.OverworldNightMonster.Chance * 0.075f;
		}
		
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Weak, 120, true);
		}
		
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("BlightedLens").Type, 2));
		}
	}
}