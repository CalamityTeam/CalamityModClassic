using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class DemonshadeHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Demonshade Helm");
			// Tooltip.SetDefault("30% increased damage and 15% increased critical strike chance, +10 max minions");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.buyPrice(5, 0, 0, 0);
			Item.defense = 50; //15
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 16;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == Mod.Find<ModItem>("DemonshadeBreastplate").Type && legs.type == Mod.Find<ModItem>("DemonshadeGreaves").Type;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
			player.armorEffectDrawOutlines = true;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "100% increased minion damage\n" +
				"All attacks inflict the demon flame debuff\n" +
				"Shadowbeams and demon scythes will fire down when you are hit\n" +
				"A friendly red devil follows you around\n" +
				"Press Y to enrage nearby enemies with a dark magic spell for 10 seconds\n" +
				"This makes them do 25% more damage but they also take 125% more damage";
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.dsSetBonus = true;
			if (player.whoAmI == Main.myPlayer && !modPlayer.chibii)
			{
				modPlayer.redDevil = true;
				if (player.FindBuffIndex(Mod.Find<ModBuff>("RedDevil").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("RedDevil").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("RedDevil").Type] < 1)
				{
					Projectile.NewProjectile(Entity.GetSource_FromThis(null),player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("RedDevil").Type, 10000, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			player.GetDamage(DamageClass.Summon) += 1f;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions += 10;
			player.GetDamage(DamageClass.Melee) += 0.3f;
			CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.3f;
			player.GetDamage(DamageClass.Ranged) += 0.3f;
			player.GetDamage(DamageClass.Magic) += 0.3f;
			player.GetDamage(DamageClass.Summon) += 0.3f;
			player.GetCritChance(DamageClass.Melee) += 15;
			player.GetCritChance(DamageClass.Magic) += 15;
			player.GetCritChance(DamageClass.Ranged) += 15;
			CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 15;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "ShadowspecBar", 8);
			recipe.AddTile(null, "DraedonsForge");
			recipe.Register();
		}
	}
}