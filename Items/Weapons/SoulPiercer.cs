using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class SoulPiercer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Piercer");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 185;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 19;
            Item.width = 60;
            Item.height = 60;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 8f;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item73;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("SoulPiercer").Type;
            Item.shootSpeed = 6f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "CosmiliteBar", 12);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}