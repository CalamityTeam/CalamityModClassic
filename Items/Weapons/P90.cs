using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class P90 : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/P90");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("P90");
        Item.damage = 3;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 60;
        Item.height = 28;
        ////Tooltip.SetDefault("A lead storm\n33% chance to not consume ammo");
        Item.useTime = 1;
        Item.useAnimation = 3;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 1.5f;
        Item.value = 750000;
        Item.rare = 8;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 18f;
        Item.useAmmo = 97;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
	    float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
	    float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
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
        recipe.AddIngredient(ItemID.IronBar, 10);
        recipe.AddIngredient(null, "CoreofEleum", 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LeadBar, 10);
        recipe.AddIngredient(null, "CoreofEleum", 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}