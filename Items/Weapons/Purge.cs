using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Purge : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Purge");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Nano Purge");
        Item.damage = 83;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 6;
        Item.width = 20;
        Item.height = 12;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.noUseGraphic = true;
		Item.channel = true;
        Item.knockBack = 3f;
        Item.value = 900000;
        Item.rare = 9;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("Purge").Type; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 24f;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.FragmentVortex, 10);
		recipe.AddIngredient(ItemID.LaserMachinegun);
		recipe.AddIngredient(ItemID.Nanites, 100);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("Purge").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}
}}