using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Contagion : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Contagion");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Contagion");
        Item.damage = 300;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 22;
        Item.height = 50;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.noUseGraphic = true;
		Item.channel = true;
        Item.knockBack = 5f;
        Item.value = 10000000;
        Item.expert = true;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("Contagion").Type; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 20f;
        Item.useAmmo = 40;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "ShadowspecBar", 5);
		recipe.AddIngredient(ItemID.Phantasm);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("Contagion").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}
}}