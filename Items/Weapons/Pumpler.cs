using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Pumpler : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Pumpler");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Pumpler");
        Item.damage = 9;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 50;
        Item.height = 28;
        ////Tooltip.SetDefault("33% chance to not consume ammo");
        Item.useTime = 9;
        Item.useAnimation = 9;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 1.25f;
        Item.value = 50000;
        Item.rare = 2;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 11f;
        Item.useAmmo = 97;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
	    float SpeedX = velocity.X + (float) Main.rand.Next(-10, 11) * 0.05f;
	    float SpeedY = velocity.Y + (float) Main.rand.Next(-10, 11) * 0.05f;
	    Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    return false;
	}
    
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
    	if (Main.rand.Next(0, 100) <= 33)
    		return false;
    	return true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Pumpkin, 30);
        recipe.AddIngredient(ItemID.PumpkinSeed, 5);
        recipe.AddIngredient(ItemID.IllegalGunParts);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}