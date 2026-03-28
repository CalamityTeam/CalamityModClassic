using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class MolluskShelleggings : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mollusk Shelleggings");
            /* Tooltip.SetDefault("5% increased damage and 4% increased critical strike chance\n" +
							   "5% decreased movement speed"); */
		}

		public override void SetDefaults()
		{
            Item.width = 22;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 15, 0, 0);
			Item.rare = 5;
			Item.defense = 15;
		}

        public override void UpdateEquip(Player player)
        {
            const float damageUp = 0.12f;
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
			player.moveSpeed -= 0.07f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("SeaPrism").Type, 20);
            recipe.AddIngredient(Mod.Find<ModItem>("MolluskHusk").Type, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
	}
}
