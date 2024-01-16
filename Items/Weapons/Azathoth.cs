using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Azathoth : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Azathoth");
		return true;
	}
	
    public override void SetDefaults()
    {
    	Item.CloneDefaults(ItemID.Kraken);
        //Tooltip.SetDefault("Azathoth");
        Item.damage = 250;
        Item.useTime = 20;
        ////Tooltip.SetDefault("Destroy the universe in the blink of an eye\nFires cosmic orbs that blast nearby enemies with lasers\nGreat for impersonating devs!");
        Item.useAnimation = 20;
        Item.useStyle = 5;
        Item.channel = true;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.knockBack = 6f;
        Item.value = 10000000;
        Item.expert = true;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("AzathothProjectile").Type;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, position, velocity,type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
		return false;
	}
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.Terrarian);
        recipe.AddIngredient(null, "ShadowspecBar", 5);
        recipe.AddIngredient(null, "CoreofCalamity", 3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}