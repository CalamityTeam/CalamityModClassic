using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class MolluskShellplate : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mollusk Shellplate");
            /* Tooltip.SetDefault("10% increased damage and 6% increased critical strike chance\n" +
							   "15% decreased movement speed"); */
		}

		public override void SetDefaults()
		{
            Item.width = 30;
            Item.height = 22;
			Item.value = Item.buyPrice(0, 20, 0, 0);
			Item.rare = 5;
			Item.defense = 22;
		}

        public override void UpdateEquip(Player player)
        {
            const float damageUp = 0.1f;
            const int critUp = 6;
            player.GetDamage(DamageClass.Melee) += damageUp;
            player.GetDamage(DamageClass.Ranged) += damageUp;
            player.GetDamage(DamageClass.Magic) += damageUp;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += damageUp;
            player.GetDamage(DamageClass.Summon) += damageUp;
            player.GetCritChance(DamageClass.Melee) += critUp;
            player.GetCritChance(DamageClass.Ranged) += critUp;
            player.GetCritChance(DamageClass.Magic) += critUp;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += critUp;
			 player.moveSpeed -= 0.15f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("SeaPrism").Type, 25);
            recipe.AddIngredient(Mod.Find<ModItem>("MolluskHusk").Type, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
