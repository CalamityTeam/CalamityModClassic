using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class TitaniumRailgun : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/TitaniumRailgun");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Titanium Railgun");
        Item.damage = 43;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 62;
        Item.height = 26;
        ////Tooltip.SetDefault("Fires a powerful blast of light energy");
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 3.5f;
        Item.value = 550000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item72;
        Item.autoReuse = true;
        Item.shootSpeed = 6f;
        Item.shoot = Mod.Find<ModProjectile>("TitRail").Type;
        Item.useAmmo = 97;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(-5, 0);
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("TitRail").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.TitaniumBar, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}