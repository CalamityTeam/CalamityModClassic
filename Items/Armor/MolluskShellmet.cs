using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class MolluskShellmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mollusk Shellmet");
			/* Tooltip.SetDefault("5% increased damage and 4% increased critical strike chance\n" +
							   "You can move freely through liquids"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 22;
			Item.value = Item.buyPrice(0, 25, 0, 0);
			Item.rare = 5;
			Item.defense = 18;
		}

		public override void UpdateEquip(Player player)
		{
			player.ignoreWater = true;
			const float damageUp = 0.05f;
			const int critUp = 4;
			player.GetDamage(DamageClass.Melee) += damageUp;
			player.GetDamage(DamageClass.Ranged) += damageUp;
			player.GetDamage(DamageClass.Magic) += damageUp;
			CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += damageUp;
			player.GetDamage(DamageClass.Summon) += damageUp;
			player.GetCritChance(DamageClass.Melee) += critUp;
			player.GetCritChance(DamageClass.Ranged) += critUp;
			player.GetCritChance(DamageClass.Magic) += critUp;
			CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += critUp;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == Mod.Find<ModItem>("MolluskShellplate").Type && legs.type == Mod.Find<ModItem>("MolluskShelleggings").Type;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Two shellfishes aid you in combat\n" +
							  "10% increased damage\n" +
							  "Your horizontal movement is slowed";
			const float damageUp = 0.10f;
			player.GetDamage(DamageClass.Melee) += damageUp;
			player.GetDamage(DamageClass.Ranged) += damageUp;
			player.GetDamage(DamageClass.Magic) += damageUp;
			CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += damageUp;
			player.GetDamage(DamageClass.Summon) += damageUp;
			player.GetModPlayer<CalamityPlayerPreTrailer>().molluskSet = true;
			player.maxMinions += 4;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(Mod.Find<ModBuff>("Shellfish").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("Shellfish").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Shellfish").Type] < 2)
				{
					Projectile.NewProjectile(Entity.GetSource_FromThis(null),player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("Shellfish").Type, (int)((double)1500 * player.GetDamage(DamageClass.Summon).Multiplicative), 0f, Main.myPlayer, 0f, 0f);
				}
			}
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("SeaPrism").Type, 15);
			recipe.AddIngredient(Mod.Find<ModItem>("MolluskHusk").Type, 6);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
