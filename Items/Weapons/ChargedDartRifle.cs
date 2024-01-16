using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ChargedDartRifle : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/ChargedDartRifle");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Charged Dart Blaster");
        Item.damage = 85;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 60;
        Item.height = 24;
        ////Tooltip.SetDefault("Fires a bouncing energy blast that splits apart on tile and enemy hits");
        Item.useTime = 35;
        Item.useAnimation = 35;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 7.5f;
        Item.value = 1050000;
        Item.rare = 9;
        Item.UseSound = new Terraria.Audio.SoundStyle("CalamityModClassic1Point1/Sounds/Item/LaserCannon");
        Item.autoReuse = true;
        Item.shootSpeed = 22f;
        Item.shoot = Mod.Find<ModProjectile>("ChargedBlast").Type;
        Item.useAmmo = 283;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(-5, 0);
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("ChargedBlast").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DartRifle);
        recipe.AddIngredient(ItemID.MartianConduitPlating, 25);
        recipe.AddIngredient(null, "CoreofEleum", 3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DartPistol);
        recipe.AddIngredient(ItemID.MartianConduitPlating, 25);
        recipe.AddIngredient(null, "CoreofEleum", 3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}