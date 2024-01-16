using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ClockGatlignum : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/ClockGatlignum");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Clock Gatlignum");
        Item.damage = 45;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 66;
        Item.height = 34;
        ////Tooltip.SetDefault("Fires a triple burst of high velocity bullets in quick succession\n33% chance to not consume ammo");
        Item.useTime = 2;
        Item.reuseDelay = 10;
        Item.useAnimation = 6;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 3.75f;
        Item.value = 1200000;
        Item.rare = 8;
        Item.UseSound = SoundID.Item31;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 20f;
        Item.useAmmo = 97;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(-5, 0);
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
	    float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
	    float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
	    Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), 242, damage, knockback, player.whoAmI, 0.0f, 0.0f);
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
        recipe.AddIngredient(ItemID.Gatligator);
        recipe.AddIngredient(ItemID.VenusMagnum);
        recipe.AddIngredient(ItemID.HallowedBar, 5);
        recipe.AddIngredient(ItemID.Ectoplasm, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}